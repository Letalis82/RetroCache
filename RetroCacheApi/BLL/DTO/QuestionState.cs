using System;

namespace RetroCache.BLL.DTO
{
    public class QuestionState
    {
        public Guid QuestionId { get; set; }
        public int Order { get; set; }
        public bool Answered { get; set; }

        public QuestionState(Guid questionId, int order)
        {
            QuestionId = questionId;
            Order = order;
            Answered = false;
        }
    }
}
