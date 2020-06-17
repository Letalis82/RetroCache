namespace RetroCache.Shared.Responses
{
    public class CurrentQuestionResponse : BaseResult
    {
        public Question Question { get; set; }

        public CurrentQuestionResponse(Question question) => Question = question;

        public CurrentQuestionResponse(string errorMessage) : base(errorMessage)
        {

        }
    }
}
