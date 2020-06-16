using System;

namespace RetroCache.DTO
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
