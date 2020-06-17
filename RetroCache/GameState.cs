using Microsoft.Extensions.Options;
using RetroCache.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetroCache
{
    public class GameState
    {
        private readonly IHttpClientFactory _http;
        private readonly RetroCacheConfiguration _config;

        public string Title { get; private set; }
        public string Question { get; private set; }

        public bool AnswerCorrect { get; private set; }
        public string AnswerHeader { get; private set; }
        public string Answer { get; private set; }

        public bool ShowAnswerBox { get; private set; }

        public GameState(IHttpClientFactory http, IOptions<RetroCache.Configuration.RetroCacheConfiguration> config)
        {
            _config = config.Value;
            Title = "Welkom Team Haai";
            Question = "<h1>Hallo team Haai</h1>Vandaag wordt het een spannende dag, namelijk: wij van de BLD dienst servies en andere rommel hebben besloten jullie te helpen met jullie zoektoch naar de toekomst, namelijk het hosten van oude software in een nieuw jasje.<p>Daarom hebben wij voor jullie een aantal nuttige zaken laten slingeren, aan jullie de taak om deze te verzamelen</p>";
            ShowAnswerBox = false;
            _http = http;
            
        }

        //public bool StartGame()
        //{
        //    return false;
        //}

        public void Next()
        {
            if (true)
            {
                Title = "Vraag 1";
                Question = "<p>asdfasdf ??<p/>";
                ShowAnswerBox = true;
            }
        }


        public bool ValidateAnswer(string answer)
        {
            //zoek en match met vraag
            return false;
        }

        private void ResetQuestion()
        {
            AnswerCorrect = false;
            AnswerHeader = string.Empty;
            Answer = string.Empty;
        }

        public async Task Restart()
        {
            ResetQuestion();

            //restart
            var ding = _http.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.ApiEndpoint}/RetroCache/RestartGame");
            await ding.SendAsync(request);

        }
    }
}
