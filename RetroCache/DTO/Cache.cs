using System;
using System.Collections.Generic;

namespace RetroCache.DTO
{
    public class Cache : BaseItem
    {
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<string> Hints { get; }

        public Cache(string description, string latitude, string longitude, List<string> hints) : base()
        {
            Id = Guid.NewGuid();
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            Hints = hints;
        }
    }
}
