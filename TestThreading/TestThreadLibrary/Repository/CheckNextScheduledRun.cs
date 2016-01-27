using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestThreadLibrary.Models;
using TestThreadLibrary.Models.Entity;

namespace TestThreadLibrary.Repository
{
    public class CheckNextScheduledRun
    {
        public static int CheckForNextExecution()
        {
            using (var db = new TrainingEntities())
            {
                var lastMinTaskRun = db.MinMaxAvgThreads.Where(x => x.Minimum.HasValue && x.Minimum > 0).Select(x => x.LastExecution).OrderByDescending(x => x).FirstOrDefault(); //make sure that does a Top 1
                var lastMaxTaskRun = db.MinMaxAvgThreads.Where(x => x.Maximum.HasValue && x.Maximum > 0).Select(x => x.LastExecution).OrderByDescending(x => x).FirstOrDefault(); //Test to see if Take (1 just gets one) 
                var lastAvgTaskRun = db.MinMaxAvgThreads.Where(x => x.Average.HasValue && x.Average > 0).Select(x => x.LastExecution).OrderByDescending(x => x).FirstOrDefault();

                //var nextMinTaskRun = db.TaskLists.Where(x => x.Id.Equals(1)).Select(x => x.Schedule.Ticks).LastOrDefault();
                //var nextMaxTaskRun = db.TaskLists.Where(x => x.Id.Equals(2)).Select(x => x.Schedule.Ticks).LastOrDefault();
                //var nextAvgTaskRun = db.TaskLists.Where(x => x.Id.Equals(3)).Select(x => x.Schedule.Ticks).LastOrDefault();

                if (lastMinTaskRun.CompareTo(lastMaxTaskRun) <= 0 && lastMinTaskRun.CompareTo(lastAvgTaskRun) < 0)
                {
                    return 1;
                }

                else if (lastMaxTaskRun.CompareTo(lastMinTaskRun) <= 0 && lastMaxTaskRun.CompareTo(lastAvgTaskRun) < 0)
                {
                    return 2;
                }

                else
                {
                    return 3;
                }
            }
        }
    }
}
