using ElasticSearch.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ElasticSearch.Repo
{
    public class ElasticRepo : IElasticRepo
    {
        private static ElasticClient _Client;

        public ElasticRepo()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node, defaultIndex: "restaurant");
            var client = new ElasticClient(settings);
            _Client = client;
        }

        public bool SaveToRepo(Restaurant r)
        {
            try
            {
                _Client.Index(r);
                return true;
            }

            catch
            {
                return false;
            }
        }

        public List<Restaurant> GetAllFromIndex()
        {

            try
            {
                var results = _Client.Search<Restaurant>(s => s
                    .Type("restaurant")
                    );

                //var results = _Client.Get<Restaurant>(g => g
                //    .Type("restaurant")
                //    .Fields(f => f.Id, f => f.RestaurantName, f => f.LandName)
                //    );

                //List<Restaurant> rList = new List<Restaurant>();

                //foreach (var r in results)
                //{
                //    rList.Add(new Restaurant
                //    {
                //        Id = r.Source.Id,
                //        RestaurantName = r.Source.RestaurantName,
                //        LandName = r.Source.LandName
                //    });
                //}

                return results.Documents.ToList<Restaurant>();
            }
            catch
            {
                return null;
            }
        }

        public List<Restaurant> ReadFromRepo(string search)
        {
            try
            {
                var sResult = _Client.Search<Restaurant>(s => s
                    .Size(10)
                    .From(0)
                    .Query(q => q
                        .QueryString(qs => qs
                            .Query(search)
                            .OnFields(new List<string> { "landName", "restaurantName" })
                            )));

                return sResult.Documents.ToList<Restaurant>();

            }

            catch
            {
                return null;
            }
        }

        public bool UpdateRepo(string rName, string lName)
        {
            try
            {
                //_Client.Update<Restaurant>();

                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool DeleteFromRepo(string rName)
        {
            try
            {
                var dResult = _Client.DeleteByQuery<Restaurant>(x => x
                    .Query(q => q.QueryString(qs => qs
                        .Query(rName)
                        .OnFields(new List<string> { "restaurantName" })
                        )));
                return true;
            }

            catch
            {
                return false;
            }
        }

    }
}