namespace RetroCache.Shared
{
    public class Question : BaseItem
    {
        public int Order { get; set; }
        public string QuestionString { get; set; }
        public bool IsLastQuestion { get; set; }

        public Question(string question, int order, bool isLastQuestion = false) : base ()
        {
            QuestionString = question;
            Order = order;
            IsLastQuestion = isLastQuestion;
        }
    }
}
