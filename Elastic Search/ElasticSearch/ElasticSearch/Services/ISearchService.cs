using ElasticSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Services
{
    public interface ISearchService
    {
        bool UpdateIndex(string rName, string lName);
        bool DeleteFromIndex(string rName);
        List<Restaurant> ReadRepo(string search);
        bool AddToIndex(Restaurant r);
        List<Restaurant> GetAllRestaurants();
        List<LogstashLog> GetAllLogstashLogs();
    }
}
