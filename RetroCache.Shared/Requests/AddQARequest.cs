using System;

namespace RetroCache.Shared.Requests
{
    public class AddQARequest
    {
        public Guid AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid CacheId { get; set; }

        public AddQARequest(Guid answerId, Guid questionId, Guid cacheId)
        {
            AnswerId = answerId;
            QuestionId = questionId;
            CacheId = cacheId;
        }

        public AddQARequest()
        {

        }
    }
}
