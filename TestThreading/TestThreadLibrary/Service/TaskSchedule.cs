using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TestThreadLibrary.Models;
using TestThreadLibrary.Repository;

namespace TestThreadLibrary.Service
{
   public class TaskSchedule
    {
        static Timer _timer;
        private IThreadHandler _ThreadHandler;

        public static void TaskSchedules()
        {
            _timer = new Timer(5000);

            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
            _timer.AutoReset = true;
        }

        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int nextThreadExe = CheckNextScheduledRun.CheckForNextExecution();

            ThreadHandler.testTimer(nextThreadExe);
        }
    }
}
