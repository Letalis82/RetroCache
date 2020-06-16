namespace RetroCache.DTO
{
    public class Question : BaseItem
    {
        public int Order { get; set; }
        public string QuestionString { get; set; }

        public Question(string question, int order) : base ()
        {
            QuestionString = question;
            Order = order;
        }
    }
}
