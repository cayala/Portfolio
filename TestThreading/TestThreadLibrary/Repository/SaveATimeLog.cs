//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TestThreadLibrary.Models;
//using TestThreadLibrary.Models.Entity;

//namespace TestThreadLibrary.Repository
//{
//    public class SaveATimeLog
//    {
//        public void SaveTimeLog(Threads model)
//        {
//            using (var db = new TrainingEntities())
//            {
//                int lastExeId;
//                var checkLastExe = db.TaskListTimeLogs.Where(e => e.LastRun.Equals(model.LastExecution)).FirstOrDefault();

//                if (checkLastExe != null)
//                {
//                    lastExeId = checkLastExe.Id;
//                    db.SaveChanges();
//                }

//                else
//                {
//                    var taskList = new TaskListTimeLog
//                    {
//                        LastRun = model.LastExecution,
//                        NextRun = model.NextExecution
//                    };
//                db.TaskListTimeLogs.Add(taskList);
//                db.SaveChanges();
//                }
                
//            }
//        }
//    }
//}
