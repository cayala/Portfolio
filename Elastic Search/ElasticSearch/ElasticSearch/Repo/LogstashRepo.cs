using ElasticSearch.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElasticSearch.Repo
{
    public class LogstashRepo : ILogstashRepo
    {
        private static ElasticClient _client;

        public LogstashRepo()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node, defaultIndex: "logstash-2015.08.18");
            var client = new ElasticClient(settings);
            _client = client;
        }

        public List<LogstashLog> GetAllLogs()
        {
            try
            {
                var result = _client.Search<LogstashLog>(s => s.Index("logstash-2015.08.18").Type("logs"));

                return result.Documents.ToList<LogstashLog>();
            }

            catch
            {
                return new List<LogstashLog>();
            }
        }
    }
}