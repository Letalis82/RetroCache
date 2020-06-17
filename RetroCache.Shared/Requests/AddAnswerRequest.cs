namespace RetroCache.Shared.Requests
{
    public class AddAnswerRequest
    {
        public string Answer { get; }

        public AddAnswerRequest(string answer) => Answer = answer;

        public AddAnswerRequest()
        {

        }
    }
}
