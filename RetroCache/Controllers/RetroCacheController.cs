using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetroCache.BLL;
using RetroCache.DTO;
using RetroCache.DTO.Requests;
using RetroCache.DTO.Responses;

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

            return new ValidateAnswerResponse(res);
        }

        [HttpGet("GetQuestions")]
        public CurrentQuestionResponse GetQuestions()
        {
            var res = _retroLogic.GetCurrentQuestion();

            if (res.HasError)
            { return new CurrentQuestionResponse(res.ErrorMessage); }


            return new CurrentQuestionResponse(res.Data);
        }

        [HttpGet("StartGame")]
        public BaseResult StartGame()
        {
            return _retroLogic.StartGame();
        }

        [HttpGet("RestartGame")]
        public BaseResult RestartGame()
        {
            return _retroLogic.RestartGame();
        }
    }
}
