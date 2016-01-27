using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestThreadLibrary.Models;

namespace TestThreadLibrary.Service
{
   public interface IThreadHandler
    {
        Threads MinMaxAvgThread(Threads model);
        // void testTimer(int threadSelected);
    }
}
