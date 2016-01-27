using ElasticSearch.Models;
using ElasticSearch.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElasticSearch.Services
{
    public class SearchService : ISearchService
    {
        private IElasticRepo _repo;
        private ILogstashRepo _LogRepo;

        public SearchService(IElasticRepo r, ILogstashRepo lr)
        {
            _repo = r;
            _LogRepo = lr;
        }

        public bool AddToIndex (Restaurant r)
        {
            return _repo.SaveToRepo(r);
        }

        public List<Restaurant> ReadRepo(string search)
        {
            return _repo.ReadFromRepo(search);
        }

        public List<Restaurant> GetAllRestaurants() {
            return _repo.GetAllFromIndex();
        }

        public bool DeleteFromIndex(string rName)
        {
            return _repo.DeleteFromRepo(rName);
        }

        public bool UpdateIndex(string rName, string lName)
        {
            return _repo.UpdateRepo(rName, lName);
        }

        public List<LogstashLog> GetAllLogstashLogs() 
        {
            return _LogRepo.GetAllLogs();
        }
    }
}