using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetroCache.BLL;
using RetroCache.Shared;
using RetroCache.Shared.Requests;
using RetroCache.Shared.Responses;

namespace RetroCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RetroCacheController : ControllerBase
    {
        private readonly ILogger<RetroCacheController> _logger;
        private readonly IRetroLogic _retroLogic;

        public RetroCacheController(ILogger<RetroCacheController> logger, IRetroLogic retroLogic)
        {
            _logger = logger;
            _retroLogic = retroLogic;
        }

        [HttpPost("ValidateAnswer")]
        public ValidateAnswerResponse ValidateAnswer([FromBody]ValidateAnswerRequest validateAnswerRequest)
        {
            var res = _retroLogic.ValidateAnswer(validateAnswerRequest.QuestionId, validateAnswerRequest.GivenAnswer);

            if (res)
            {
                var cache = _retroLogic.GetCacheCorrespondingToQuestion(validateAnswerRequest.QuestionId);

                if (!cache.HasError)
                { return new ValidateAnswerResponse(cache.Data); }

                return new ValidateAnswerResponse(cache.ErrorMessage);
            }

            return new ValidateAnswerResponse(res);
        }

        [HttpGet("GetCurrentQuestion")]
        public CurrentQuestionResponse GetCurrentQuestion()
        {
            var res = _retroLogic.GetCurrentQuestion();

            if (res.HasError)
            { return new CurrentQuestionResponse(res.ErrorMessage); }


            return new CurrentQuestionResponse(res.Data);
        }

        [HttpGet("StartGame")]
        public BaseResult<bool> StartGame()
        {
            return _retroLogic.StartGame();
        }

        [HttpGet("RestartGame")]
        public BaseResult<bool> RestartGame()
        {
            return _retroLogic.RestartGame();
        }

        [HttpGet("ValidateGameStart")]
        public BaseResult<bool> ValidateGameStart()
        {
            return _retroLogic.ValidateGameStart();
        }
    }
}
