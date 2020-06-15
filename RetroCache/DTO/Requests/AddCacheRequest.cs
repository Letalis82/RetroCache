﻿using System.Collections.Generic;

namespace RetroCache.DTO.Requests
{
    public class AddCacheRequest
    {
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<string> Hints { get; }

        public AddCacheRequest(string description, string latitude, string longitude, List<string> hints)
        {
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            Hints = hints;
        }
    }
}
