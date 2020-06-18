using RetroCache.Shared;
using System.Collections.Generic;

namespace RetroCache.DTO
{
    public class Inventory
    {
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Cache> Caches { get; set; }
        public List<QA> QAs { get; set; }

        public Inventory()
        {
            Questions = new List<Question>();
            Answers = new List<Answer>();
            Caches = new List<Cache>();
            QAs = new List<QA>();
        }
    }
}
