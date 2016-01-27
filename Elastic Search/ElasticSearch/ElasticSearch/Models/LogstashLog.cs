using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElasticSearch.Models
{
    public class LogstashLog
    {
        public string Message { get; set; }
        public string @TimeStamp { get; set; }
        public string Path { get; set; }
        public string Host { get; set; }
    }
}