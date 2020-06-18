using RetroCache.DAL;
using RetroCache.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RetroCache.BLL
{
    public class RetroLogic : IRetroLogic
    {
        private IRetroStore<Cache> _cacheStore;
        private IRetroStore<Answer> _answerStore;
        private IRetroStore<Question> _questionStore;
        private IRetroStore<QA> _qaStore;

        private const string _answerLocation = "/Storage/Answers.dat";
        private const string _questionLocation = "/Storage/Questions.dat";
        private const string _cacheLocation = "/Storage/Caches.dat";
        private const string _qaLocation = "/Storage/QA.dat";
        private readonly IGameController _gameController;

        public RetroLogic(IGameController gameController)
        {
            _cacheStore = new RetroStore<Cache>(_cacheLocation);
            _answerStore = new RetroStore<Answer>(_answerLocation);
            _questionStore = new RetroStore<Question>(_questionLocation);
            _qaStore = new RetroStore<QA>(_qaLocation);
            _gameController = gameController;
        }

        public BaseResult AddCache(string description, string latitude, string longitude, List<string> hints)
        {
            var existing = _cacheStore.Data.FirstOrDefault(c => c.Latitude.Equals(latitude, StringComparison.InvariantCultureIgnoreCase) && c.Longitude.Equals(longitude, StringComparison.InvariantCultureIgnoreCase));

            if (existing != null)
            {
                return new BaseResult("Items exists");
            }

            return _cacheStore.Add(new Cache(description, latitude, longitude, hints));
        }

        public BaseResult AddQuestion(string question, int order)
        {
            var existing = _questionStore.Data.FirstOrDefault(q => q.QuestionString.Equals(question, StringComparison.InvariantCultureIgnoreCase));

            if (existing != null)
            {
                return new BaseResult("Question exists");
            }

            return _questionStore.Add(new Question(question, order));
        }

        public BaseResult AddQA(Guid answerId, Guid questionId, Guid cacheId)
        {
            var existing = _qaStore.Data.FirstOrDefault(q => q.AnswerId == answerId && q.QuestionId == questionId && q.CacheId == cacheId);

            if (existing != null)
            {
                return new BaseResult("QA exists");
            }

            return _qaStore.Add(new QA(answerId, questionId, cacheId));
        }

        public BaseResult RemoveQuestion(Guid questionId)
        {
            return _questionStore.Remove(questionId);
        }

        public BaseResult RemoveCache(Guid cacheId)
        {
            return _cacheStore.Remove(cacheId);
        }

        public BaseResult RemoveAnswer(Guid answerId)
        {
            return _answerStore.Remove(answerId);
        }

        public BaseResult RemoveQA(Guid qAId)
        {
            return _qaStore.Remove(qAId);
        }

        public bool ValidateAnswer(Guid questionId, string givenAnswer)
        {
            var foundQ = _questionStore.Data.FirstOrDefault(q => q.Id == questionId);

            if (foundQ != null)
            {
                return foundQ.QuestionString.Equals(givenAnswer, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public BaseResult AddAnswer(string answer)
        {
            var existing = _answerStore.Data.FirstOrDefault(q => q.AnswerString.Equals(answer, StringComparison.InvariantCultureIgnoreCase));

            if (existing != null)
            {
                return new BaseResult("Answer exists");
            }

            return _answerStore.Add(new Answer(answer));
        }

        public BaseResult ChangeQuestionOrder(Guid questionId, int newOrder)
        {
            var a = _questionStore.Data.FirstOrDefault(a => a.Id == questionId);
            a.Order = newOrder;

            _questionStore.Remove(questionId);
            return _questionStore.Add(a);
        }

        public BaseResult<Question> GetCurrentQuestion()
        {
            if (!_gameController.Started)
            {
                return new BaseResult<Question>("Game is not started");
            }

            return new BaseResult<Question>(_gameController.CurrentQuestion());
        }

        public BaseResult StartGame()
        {
            if (!_questionStore.Data.Any() || !_answerStore.Data.Any() || !_qaStore.Data.Any() || !_cacheStore.Data.Any())
            {
                return new BaseResult("Cannot start, invalid questions / answers collection");
            }

            if (_questionStore.Data.Count != _qaStore.Data.Count || _questionStore.Data.Count != _answerStore.Data.Count || _questionStore.Data.Count != _cacheStore.Data.Count || _questionStore.Data.Count != _qaStore.Data.Count)
            {
                return new BaseResult("Cannot start questions/answers/caches are nog equal size");
            }

            for (int i = 1; i < _questionStore.Data.Count; i++)
            {
                var c = _questionStore.Data.Select(c => c.Order == i).FirstOrDefault();

                if (!c)
                { return new BaseResult("Incorrect question order"); }
            }

            foreach (var item in _questionStore.Data)
            {
                var c = _qaStore.Data.Select(c => c.QuestionId == item.Id).FirstOrDefault();

                if (!c)
                { return new BaseResult("Question is not in the question list"); }
            }

            foreach (var item in _answerStore.Data)
            {
                var c = _qaStore.Data.Select(c => c.AnswerId == item.Id).FirstOrDefault();

                if (!c)
                { return new BaseResult("Answer is not in the question list"); }
            }

            foreach (var item in _cacheStore.Data)
            {
                var c = _qaStore.Data.Select(c => c.CacheId == item.Id).FirstOrDefault();

                if (!c)
                { return new BaseResult("Cache is not in the question list"); }
            }

            _gameController.StartGame(_questionStore.Data, _answerStore.Data, _qaStore.Data, _cacheStore.Data);

            return new BaseResult();
        }

        public BaseResult RestartGame()
        {
            _gameController.RestartGame();

            return new BaseResult();
        }

        public BaseResult<List<Cache>> GetCaches()
        {
            var res = _cacheStore.Data.Any() ? _cacheStore.Data : null;
            return new BaseResult<List<Cache>>(res);
        }

        public BaseResult<List<Answer>> GetAnswers()
        {
            var res = _answerStore.Data.Any() ? _answerStore.Data : null;
            return new BaseResult<List<Answer>>(res);
        }

        public BaseResult<List<QA>> GetQAs()
        {
            var res = _qaStore.Data.Any() ? _qaStore.Data : null;
            return new BaseResult<List<QA>>(res);
        }

        BaseResult<List<Question>> IRetroLogic.GetQuestions()
        {
            var res = _questionStore.Data.Any() ? _questionStore.Data : null;
            return new BaseResult<List<Question>>(res);
        }
    }
}
