using System;

namespace RetroCache.Shared
{
    public class QA : BaseItem
    {
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid CacheId { get; set; }

        public QA(Guid answerId, Guid questionId, Guid cacheId) : base()
        {
            AnswerId = answerId;
            QuestionId = questionId;
            CacheId = cacheId;
        }
    }
}
