using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetroCache.BLL;
using RetroCache.Shared;
using RetroCache.Shared.Requests;
using System;
using System.Collections.Generic;

namespace RetroCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IRetroLogic _retroLogic;

        public AdminController(ILogger<AdminController> logger, IRetroLogic retroLogic)
        {
            _logger = logger;
            _retroLogic = retroLogic;
        }

        [HttpPost("AddQuestion")]
        public BaseResult AddQuestion(AddQuestionRequest request)
        {
            return _retroLogic.AddQuestion(request.Question, request.Order);
        }

        [HttpDelete("RemoveQuestion")]
        public BaseResult RemoveQuestion(Guid questionId)
        {
            return _retroLogic.RemoveQuestion(questionId);
        }

        [HttpPost("AddQuestionAnswerCombi")]
        public BaseResult AddQuestionAnswerCombi(AddQARequest request)
        {
            return _retroLogic.AddQA(request.AnswerId, request.QuestionId, request.CacheId);
        }

        [HttpDelete("RemoveQuestionAnswerCombi")]
        public BaseResult RemoveQuestionAnswerCombi(Guid combiId)
        {
            return _retroLogic.RemoveQA(combiId);
        }

        [HttpPost("AddAnswer")]
        public BaseResult AddAnswer(AddAnswerRequest request)
        {
            return _retroLogic.AddAnswer(request.Answer);
        }

        [HttpDelete("RemoveAnswer")]
        public BaseResult RemoveAnswer(Guid answerId)
        {
            return _retroLogic.RemoveAnswer(answerId);
        }

        [HttpGet("GetAnswers")]
        public BaseResult<List<Answer>> GetAnswers()
        {
            return _retroLogic.GetAnswers();
        }

        [HttpGet("GetQuestions")]
        public BaseResult<List<Question>> GetQuestions()
        {
            return _retroLogic.GetQuestions();
        }

        [HttpGet("GetQAs")]
        public BaseResult<List<QA>> GetQAs()
        {
            return _retroLogic.GetQAs();
        }

        [HttpGet("GetCaches")]
        public BaseResult<List<Cache>> GetCaches()
        {
            return _retroLogic.GetCaches();
        }

        [HttpPost("AddCache")]
        public BaseResult AddCache(AddCacheRequest request)
        {
            return _retroLogic.AddCache(request.Description, request.Latitude, request.Latitude, request.Hints);
        }

        [HttpDelete("RemoveCache")]
        public BaseResult RemoveCache(Guid cacheId)
        {
            return _retroLogic.RemoveCache(cacheId);
        }

    }
}
