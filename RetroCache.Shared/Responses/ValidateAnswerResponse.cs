namespace RetroCache.Shared.Responses
{
    public class ValidateAnswerResponse : BaseResult
    {
        public bool IsCorrect { get; set; }
        public Cache Cache { get; set; }

        public ValidateAnswerResponse(Cache cache)
        {
            IsCorrect = true;
            Cache = cache;
        }

        public ValidateAnswerResponse(bool isCorrect)
        {
            IsCorrect = isCorrect;
        }

        public ValidateAnswerResponse()
        {
            IsCorrect = false;
        }

        public ValidateAnswerResponse(string errorMessage) : base(errorMessage)
        {
            IsCorrect = false;
        }

    }
}
