using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using TestThreadLibrary.Service;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = ConfigureDependencies();
            
            TaskSchedule.TaskSchedules();

            Console.Write("Done");
            Console.ReadLine();
        }

        private static IContainer ConfigureDependencies()
        {
            return new Container(x =>
            {
                x.Scan(
                    scan =>
                    {
                        scan.Assembly("TestThreadLibrary");
                        scan.Assembly("ConsoleApplication1");
                        scan.WithDefaultConventions();
                    }
                    );
            });
        }
    }
}
