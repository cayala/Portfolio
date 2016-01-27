using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestThreadLibrary.Models;
using System.Threading;
using TestThreadLibrary.Service;
using TestThreadLibrary.Repository;

namespace TestThreadLibrary.Service
{
    public class ThreadHandler : IThreadHandler
    {
        ICalculateValues _CalculateValues;

        public ThreadHandler(ICalculateValues cv)
        {
            _CalculateValues = cv;
        }

        public ThreadHandler()
        {

        }

        public Threads MinMaxAvgThread(Threads model)
        {
            CalculateValues calculate = new CalculateValues();

            int minValue = 0;
            int maxValue = 0;
            int avgValue = 0;

            Thread minThread = new Thread(() => minValue = calculate.CalculateMin(model));
            Thread maxThread = new Thread(() => maxValue = calculate.CalculateMax(model));
            Thread avgThread = new Thread(() => avgValue = calculate.CalculateAverage(model));

            string name = "TestThread";

            minThread.Start();

            maxThread.Start();

            avgThread.Start();

            minThread.Join();
            maxThread.Join();
            avgThread.Join();


            model.Min = minValue;
            model.Max = maxValue;
            model.Average = avgValue;
            model.Task = name;
            model.LastExecution = DateTime.UtcNow;

            if (!minThread.IsAlive && !maxThread.IsAlive && !avgThread.IsAlive)
            {
                AddValuesToSQL add = new AddValuesToSQL();
                SaveATaskList addTask = new SaveATaskList();
                
                addTask.SaveTaskList(model);
                add.SaveData(model);
            }

            return model;
        }

        public static void testTimer(int threadSelected)
        {
            Threads model = new Threads();
            CalculateValues calculate = new CalculateValues();

            int minValue = 0;
            int maxValue = 0;
            int avgValue = 0;

            if (threadSelected == 1)
            {
                Thread minThread = new Thread(() => minValue = calculate.CalculateMin(model));

                minThread.Start();
                minThread.Join();

                model.Min = minValue;
                model.Task = "Minimum";
                model.LastExecution = DateTime.UtcNow;

                AddValuesToSQL add = new AddValuesToSQL();
                SaveATaskList addTask = new SaveATaskList();

                addTask.SaveTaskList(model);
                add.SaveData(model);

            }

            else if (threadSelected == 2)
            {
                Thread maxThread = new Thread(() => maxValue = calculate.CalculateMax(model));

                maxThread.Start();
                maxThread.Join();

                model.Max = maxValue;
                model.Task = "Maximum";
                model.LastExecution = DateTime.UtcNow;

                AddValuesToSQL add = new AddValuesToSQL();
                SaveATaskList addTask = new SaveATaskList();

                addTask.SaveTaskList(model);
                add.SaveData(model);
            }

            else
            {
                Thread avgThread = new Thread(() => avgValue = calculate.CalculateAverage(model));

                avgThread.Start();
                avgThread.Join();

                model.Average = avgValue;
                model.Task = "Average";
                model.LastExecution = DateTime.UtcNow;

                AddValuesToSQL add = new AddValuesToSQL();
                SaveATaskList addTask = new SaveATaskList();

                addTask.SaveTaskList(model);
                add.SaveData(model);
            }
        }
    }
}
