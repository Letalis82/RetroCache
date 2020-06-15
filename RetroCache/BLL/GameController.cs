using RetroCache.BLL.DTO;
using RetroCache.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RetroCache.BLL
{
    public class GameController : IGameController
    {
        private List<Question> _questions;
        private List<Answer> _answers;
        private List<QA> _matchList;
        private List<Cache> _caches;

        private List<QuestionState> _questionState;

        public bool Started { get; private set; }

        public Question CurrentQuestion()
        {
            foreach (var q in _questionState)
            {
                if (!q.Answered)
                { return _questions.First(x => x.Id == q.QuestionId); }
            }

            return new Question("Complete!!!!", 9999);
        }

        public void RestartGame()
        {
            Init();
        }

        public void StartGame(List<Question> questions, List<Answer> answers, List<QA> matchList, List<Cache> caches)
        {
            _questions = questions;
            _answers = answers;
            _matchList = matchList;
            _caches = caches;

            Init();
        }

        private void Init()
        {
            _questionState = _questions.OrderBy(a => a.Order).Select(q => new QuestionState(q.Id, q.Order)).ToList();
            Started = true;
        }

        public void UpdateQuestionState(Guid questionId, bool state)
        {
            var q = _questionState.FirstOrDefault(c => c.QuestionId == questionId);

            q.Answered = state;
        }
    }
}
