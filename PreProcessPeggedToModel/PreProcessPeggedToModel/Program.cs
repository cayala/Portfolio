using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreProcessPeggedToModel.Model;
using PreProcessPeggedToModel.Repo;
using System.Diagnostics;

namespace PreProcessPeggedToModel
{
    class Program
    {
        static void Main(string[] args)
        {
            //First get list of databases
            //THen loop through and see if they have part to model table
            //If they do, skip it. If they do not have the table, then create the table
            //You will need a list of databases
            //The SQL queries to run
            //A list of AssemblyIds and PeggedIds
            //query to create table
            //query to insert all values into said table
            SqlHelper repo = new SqlHelper();
            
            List<Database> dbs = repo.GetDataBases();
            
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach(Database db in dbs)
            {
                if (repo.CheckIfTableExists(db))
                {
                    Console.WriteLine(String.Format("Skipped Catalog: {0}", db.Catalog));
                    continue;
                }

                else 
                {
                    Stopwatch sw2 = new Stopwatch();
                    sw2.Start();

                    repo.CreateTable(db);

                    sw2.Stop();
                    Console.WriteLine(String.Format("Catalog {0} took {1} milliseconds", db.Catalog, sw2.ElapsedMilliseconds.ToString()));
                }
            }
            sw.Stop();

            Console.WriteLine(String.Format("Total time to execute job is {0}"), sw.ElapsedMilliseconds.ToString());
        }
    }
}
