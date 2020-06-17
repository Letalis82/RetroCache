using Newtonsoft.Json;
using RetroCache.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RetroCache.DAL
{
    public class RetroStore<T> : IRetroStore<T> where T : BaseItem
    {
        private readonly string _location;
        public List<T> Data { get; private set; }

        public RetroStore(string location)
        {
            Data = Read(location);
            _location = location;
        }

        public BaseResult Add(T item)
        {
            if (Data.FirstOrDefault(d => d.Id == item.Id) == null)
            {
                Data.Add(item);
                Store(Data, _location);
                return new BaseResult();
            }

            return new BaseResult("Item already exists");
        }

        public BaseResult Remove(Guid item)
        {
            var foundItem = Data.FirstOrDefault(d => d.Id == item);
            if (foundItem != null)
            {
                Data.Remove(foundItem);
                Store(Data, _location);
                return new BaseResult();
            }

            return new BaseResult("Item not found");
        }

        private List<T> Read(string location)
        {
            if (File.Exists(location))
            {
                string rawContent = File.ReadAllText(location);
                return JsonConvert.DeserializeObject<List<T>>(rawContent);
            }

            return new List<T>();
        }

        private void Store(List<T> storageObject, string location)
        {
            if (!Directory.Exists(Path.GetDirectoryName(location)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(location));
            }

            string rawContent = JsonConvert.SerializeObject(storageObject);

            File.WriteAllText(location, rawContent);
        }
    }
}
