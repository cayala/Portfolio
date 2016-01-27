using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElasticSearch.Models
{
    public class RestaurantResponse
    {
        public string Id { get; set; }
        public string RestaurantName { get; set; }
        public string LandName { get; set; }
    }
}