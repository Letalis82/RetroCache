using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RetroCache.Configuration;
using RetroCache.DTO;
using RetroCache.Shared;
using RetroCache.Shared.Requests;
using RetroCache.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RetroCache
{
    public class GameState
    {
        private const string ADMIN = "/Admin";
        private const string DEFAULT = "/RetroCache";
        private const string RESTART = "/RestartGame";
        private const string START = "/StartGame";
        private const string CURRENTQUESTION = "/GetCurrentQuestion";
        private const string VALIDATEANSWER = "/ValidateAnswer";

        //admin stuff
        private const string GETANSWERS = "/GetAnswers";
        private const string GETQUESTIONS = "/GetQuestions";
        private const string GETQA = "/GetQAs";
        private const string GETCACHES = "/GetCaches";

        private const string REMOVEQUESTION = "/RemoveQuestion";
        private const string REMOVEANSWER = "/RemoveAnswer";
        private const string REMOVEQA = "/RemoveQuestionAnswerCombi";
        private const string REMOVECACHE = "/RemoveCache";

        private const string ADDQUESTION = "/AddQuestion";
        private const string ADDANSWER = "/AddAnswer";
        private const string ADDQA = "/AddQuestionAnswerCombi";
        private const string ADDCACHE = "/AddCache";

        private readonly IHttpClientFactory _http;
        private readonly RetroCacheConfiguration _config;

        public string Title { get; private set; }
        public string Question { get; private set; }
        private Question _currentQuestion;
        public bool GameIsActive = false;

        public Cache CurrentCache;

        public bool AnswerCorrect { get; private set; }
        public string AnswerHeader { get; private set; }
        public string Answer { get; private set; }

        public bool ShowAnswerBox { get; private set; }

        public bool IsGameStarted = false;

        public GameState(IHttpClientFactory http, IOptions<RetroCache.Configuration.RetroCacheConfiguration> config)
        {
            _config = config.Value;
            _http = http;
            SetWelcome();
        }

        private void SetWelcome()
        {
            Title = "Welkom Team Haai";
            Question = "<h1>Hallo team Haai</h1>Vandaag wordt het een spannende dag, namelijk: wij van de BLD dienst servies en andere rommel hebben besloten jullie te helpen met jullie zoektoch naar de toekomst, namelijk het hosten van oude software in een nieuw jasje.<p>Daarom hebben wij voor jullie een aantal nuttige zaken laten slingeren, aan jullie de taak om deze te verzamelen</p>";
            ShowAnswerBox = false;
        }

        public async Task Next()
        {
            if (!IsGameStarted)
            {
                IsGameStarted = await StartGame();
            }
            await GetCurrentQuestion();
            ShowAnswerBox = true;
        }

        private void SetQuestion(Question q)
        {
            _currentQuestion = q;
            Question = q.QuestionString;

            if (q.Order >= 9999)
            { Title = "-"; }
            else
            { Title = $"Vraag: {q.Order}"; }
        }

        public async Task<bool> ValidateAnswer(string givenAnswer)
        {
            //zoek en match met vraag
            var ding = _http.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config.ApiEndpoint}{DEFAULT}{VALIDATEANSWER}");
            request.Content = JsonContent.Create(new ValidateAnswerRequest(_currentQuestion.Id, givenAnswer));
            var res = await ding.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                var aRes = JsonConvert.DeserializeObject<ValidateAnswerResponse>(await res.Content.ReadAsStringAsync());
                AnswerCorrect = aRes.IsCorrect;
                if (aRes.IsCorrect)
                {
                    CurrentCache = aRes.Cache;
                    return true;
                }
            }

            return false;
        }

        private void ResetQuestion()
        {
            AnswerCorrect = false;
            AnswerHeader = string.Empty;
            Answer = string.Empty;
        }

        private async Task<bool> StartGame()
        {
            ResetQuestion();

            var ding = _http.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.ApiEndpoint}{DEFAULT}{START}");
            var r = await ding.SendAsync(request);

            if (r.IsSuccessStatusCode)
            {
                var d = JsonConvert.DeserializeObject<BaseResult<bool>>(await r.Content.ReadAsStringAsync());
                return !d.HasError;
            }

            return false;
        }

        public async Task<bool> Restart()
        {
            ResetQuestion();
            CurrentCache = null;
            GameIsActive = false;
            IsGameStarted = false;
            SetWelcome();

            //restart
            var ding = _http.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.ApiEndpoint}{DEFAULT}{RESTART}");
            var r = await ding.SendAsync(request);

            if (r.IsSuccessStatusCode)
            {
                var d = JsonConvert.DeserializeObject<BaseResult<bool>>(await r.Content.ReadAsStringAsync());
                return !d.HasError;
            }

            return false;

        }

        public async Task<Inventory> GetInventory()
        {
            Inventory result = new Inventory();

            var qData = await GetListingData<List<Question>>($"{ADMIN}{GETQUESTIONS}");
            result.Questions = qData;

            var aData = await GetListingData<List<Answer>>($"{ADMIN}{GETANSWERS}");
            result.Answers = aData;

            var qaData = await GetListingData<List<QA>>($"{ADMIN}{GETQA}");
            result.QAs = qaData;

            var cData = await GetListingData<List<Cache>>($"{ADMIN}{GETCACHES}");
            result.Caches = cData;

            return result;
        }

        private async Task<T> GetListingData<T>(string endpoint) where T : class
        {
            var ding = _http.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.ApiEndpoint}{endpoint}");
            var res = await ding.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                string data = await res.Content.ReadAsStringAsync();
                var r = JsonConvert.DeserializeObject<BaseResult<T>>(data);

                if (!r.HasError)
                {
                    return r.Data;
                }
            }

            return default(T);
        }

        public async Task RemoveQuestion(Guid id)
        {
            await Remove($"{ADMIN}{REMOVEQUESTION}?questionId={id}");
        }

        public async Task RemoveQA(Guid id)
        {
            await Remove($"{ADMIN}{REMOVEQA}?combiId={id}");
        }

        public async Task RemoveAnswer(Guid id)
        {
            await Remove($"{ADMIN}{REMOVEANSWER}?answerId={id}");
        }

        public async Task RemoveCache(Guid id)
        {
            await Remove($"{ADMIN}{REMOVECACHE}?cacheId={id}");
        }

        private async Task Remove(string endpoint)
        {
            var ding = _http.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_config.ApiEndpoint}{endpoint}");

            var res = await ding.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
            }
        }

        public async Task AddQuestion(int order, string questionText)
        {
            var ding = _http.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config.ApiEndpoint}{ADMIN}{ADDQUESTION}");
            request.Content = JsonContent.Create(new AddQuestionRequest { Order = order, Question = questionText });
            var res = await ding.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
            }
        }

        public async Task AddAnswer(string answerText)
        {
            var ding = _http.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config.ApiEndpoint}{ADMIN}{ADDANSWER}");
            request.Content = JsonContent.Create(new AddAnswerRequest { Answer = answerText });
            var res = await ding.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
            }
        }

        public async Task AddCache(string description, string latitude, string longitude)
        {
            var ding = _http.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config.ApiEndpoint}{ADMIN}{ADDCACHE}");
            request.Content = JsonContent.Create(new AddCacheRequest { Description = description, Latitude = latitude, Longitude = longitude, Hints = new List<string>() });
            var res = await ding.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
            }
        }

        public async Task AddQA(Guid questionId, Guid answerId, Guid cacheId)
        {
            var ding = _http.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config.ApiEndpoint}{ADMIN}{ADDQA}");
            request.Content = JsonContent.Create(new AddQARequest(answerId, questionId, cacheId));
            var res = await ding.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
            }
        }

        private async Task GetCurrentQuestion()
        {
            var ding = _http.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.ApiEndpoint}{DEFAULT}{CURRENTQUESTION}");
            var res = await ding.SendAsync(request);

            if (res.IsSuccessStatusCode)
            {
                var currentQ = JsonConvert.DeserializeObject<CurrentQuestionResponse>(await res.Content.ReadAsStringAsync());

                if (currentQ.HasError)
                {
                    Question = $"Oh ow.....{currentQ.ErrorMessage}";
                }
                else
                {
                    SetQuestion(currentQ.Question);
                }
            }
            else
            {
                Question = "Oh ow.... problemen";
            }
        }

    }
}
