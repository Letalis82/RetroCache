using System;

namespace RetroCache.DTO
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
