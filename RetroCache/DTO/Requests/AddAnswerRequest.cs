namespace RetroCache.DTO.Requests
{
    public class AddAnswerRequest
    {
        public string Answer { get; }

        public AddAnswerRequest(string answer) => Answer = answer;
    }
}
