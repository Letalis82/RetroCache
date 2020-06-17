using System;

namespace RetroCache.Shared
{
    public abstract class BaseItem
    {
        public Guid Id { get; set; }

        public BaseItem()
        {
            Id = Guid.NewGuid();
        }
    }
}
