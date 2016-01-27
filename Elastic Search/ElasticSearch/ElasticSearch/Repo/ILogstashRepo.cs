using ElasticSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Repo
{
    public interface ILogstashRepo
    {
        List<LogstashLog> GetAllLogs();
    }
}
