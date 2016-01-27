using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TestThreadLibrary.Models;
using TestThreadLibrary.Service;
using TestThreadLibrary.Repository;

namespace TaskScheduler.Services
{
    public class TaskSchedule
    {
       static Timer _timer;

        public static void TaskSchedules()
        {
            _timer = new Timer(60000);

            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
        }

        static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Threads model = new Threads();
            ThreadHandler thread = new ThreadHandler();

            thread.testTimer();
        }
    }
}
