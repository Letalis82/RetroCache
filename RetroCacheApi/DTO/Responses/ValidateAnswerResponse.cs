namespace RetroCache.DTO.Responses
{
    public class ValidateAnswerResponse : BaseResult
    {
        public bool IsCorrect { get; set; }
        
        public ValidateAnswerResponse(string errorMessage) : base(errorMessage)
        {
            IsCorrect = false;
        }

        public ValidateAnswerResponse(bool isCorrect) : base()
        {
            IsCorrect = isCorrect;
        }
    }
}
