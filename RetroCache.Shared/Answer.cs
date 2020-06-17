using System;

namespace RetroCache.Shared
{
    public class Answer : BaseItem
    {
        public string AnswerString { get; set; }

        public Answer(string answer) : base()
        {
            AnswerString = answer;
        }
    }
}
