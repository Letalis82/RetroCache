using RetroCache.Shared;
using System;
using System.Collections.Generic;

namespace RetroCache.BLL
{
    public interface IRetroLogic
    {
        BaseResult AddCache(string description, string longitude, string latitude, List<string> hints);
        BaseResult RemoveCache(Guid cacheId);
        BaseResult<List<Cache>> GetCaches();

        BaseResult AddAnswer(string answer);
        BaseResult RemoveAnswer(Guid answerId);
        BaseResult<List<Answer>> GetAnswers();

        BaseResult AddQuestion(string question, int order);
        BaseResult RemoveQuestion(Guid questionId);
        BaseResult<List<Question>> GetQuestions();

        BaseResult AddQA(Guid answerId, Guid questionId, Guid cacheId);
        BaseResult RemoveQA(Guid qAId);
        BaseResult<List<QA>> GetQAs();

        BaseResult ChangeQuestionOrder(Guid questionId, int newOrder);
        bool ValidateAnswer(Guid questionId, string givenAnswer);
        
        BaseResult<Question> GetCurrentQuestion();
        BaseResult StartGame();
        BaseResult RestartGame();
    }
}
