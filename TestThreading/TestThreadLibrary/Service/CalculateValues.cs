using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestThreadLibrary.Models;
using System.Threading;

namespace TestThreadLibrary.Service
{
    public class CalculateValues : ICalculateValues
    {
        public int CalculateAverage(Threads model, int value1 = 0, int value2 = 0, int value3 = 0)
        {
            if (model.FirstValue != 0 || model.SecondValue != 0 || model.ThirdValue != 0)
            {
                return model.Average = model.FirstValue + model.SecondValue + model.ThirdValue / 3;
            }

            else
            {
                Random rand = new Random();

                value1 = rand.Next(1, 101);
                value2 = rand.Next(1, 101);
                value3 = rand.Next(1, 101);

                return model.Average = value1 + value2 + value3 / 3;
            }
        }

        public int CalculateMin(Threads model, int value1 = 0, int value2 = 0, int value3 = 0)
        {
            if (model.FirstValue != 0 || model.SecondValue != 0 || model.ThirdValue != 0)
            {
                if (model.FirstValue < model.SecondValue && model.FirstValue < model.ThirdValue)
                {
                    return model.FirstValue;
                }

                else if (model.SecondValue < model.FirstValue && model.SecondValue < model.ThirdValue)
                {
                    return model.SecondValue;
                }

                else
                {
                    return model.ThirdValue;
                }
            }

            else
            {
                Random rand = new Random();

                value1 = rand.Next(1, 101);
                value2 = rand.Next(1, 101);
                value3 = rand.Next(1, 101);

                if (value1 < value2 && value1 < value3)
                {
                    return value1;
                }

                else if (value2 < value1 && value2 < value3)
                {
                    return value2;
                }

                else
                {
                    return value3;
                }
            }
        }

        public int CalculateMax(Threads model, int value1 = 0, int value2 = 0, int value3 = 0)
        {
            if (model.FirstValue != 0 || model.SecondValue != 0 || model.ThirdValue != 0)
            {
                if (model.FirstValue > model.SecondValue && model.FirstValue > model.ThirdValue)
                {
                    return model.FirstValue;
                }

                else if (model.SecondValue > model.FirstValue && model.SecondValue > model.ThirdValue)
                {
                    return model.SecondValue;
                }

                else
                {
                    return model.ThirdValue;
                }
            }
            else
            {
                Random rand = new Random();

                value1 = rand.Next(1, 101);
                value2 = rand.Next(1, 101);
                value3 = rand.Next(1, 101);

                if (value1 > value2 && value1 > value3)
                {
                    return value1;
                }

                else if (value2 > value1 && value2 > value3)
                {
                    return value2;
                }

                else
                {
                    return value3;
                }
            }
        }
    }
}
