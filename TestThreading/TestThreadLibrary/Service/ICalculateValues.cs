using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestThreadLibrary.Models;
using TestThreadLibrary.Service;

namespace TestThreadLibrary.Service
{
   public interface ICalculateValues
    {
       int CalculateAverage(Threads model, int value1 = 0, int value2 = 0, int value3 = 0);

       int CalculateMin(Threads model, int value1 = 0, int value2 = 0, int value3 = 0);

       int CalculateMax(Threads model, int value1 = 0, int value2 = 0, int value3 = 0);
    }
}
