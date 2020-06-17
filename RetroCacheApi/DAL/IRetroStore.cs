using RetroCache.Shared;
using System;
using System.Collections.Generic;

namespace RetroCache.DAL
{
    public interface IRetroStore<T>
    {
        List<T> Data { get; }
        BaseResult Add(T item);
        BaseResult Remove(Guid item);
    }
}
