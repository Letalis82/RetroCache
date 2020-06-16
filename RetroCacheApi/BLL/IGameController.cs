using RetroCache.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RetroCache.BLL
{
    public interface IGameController
    {
        bool Started { get; }
        void StartGame(List<Question> questions, List<Answer> answers, List<QA> matchList, List<Cache> caches);
        void RestartGame();

        Question CurrentQuestion();
        void UpdateQuestionState(Guid questionId, bool state);
    }
}
