using ElasticSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Repo
{
   public interface IElasticRepo
    {
       bool SaveToRepo(Restaurant r);
       bool DeleteFromRepo(string rName);
       bool UpdateRepo(string rName, string lName);
       List<Restaurant> ReadFromRepo(string search);
       List<Restaurant> GetAllFromIndex();
    }
}
