using System;

namespace RetroCache.Shared.Requests
{
    public class AddQARequest
    {
        public Guid AnswerId { get; }
        public Guid QuestionId { get; }
        public Guid CacheId { get; }

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
