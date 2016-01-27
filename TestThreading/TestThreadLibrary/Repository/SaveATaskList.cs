using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestThreadLibrary.Models;
using TestThreadLibrary.Models.Entity;

namespace TestThreadLibrary.Repository
{
    public class SaveATaskList
    {
        public void SaveTaskList(Threads model)
        {
            using (var db = new TrainingEntities())
            {
                int taskId;
                var checkTaskExists = db.TaskLists.Where(t => t.TaskName.ToLower().Equals(model.Task.ToLower())).FirstOrDefault();

                if (checkTaskExists != null)
                {
                    taskId = checkTaskExists.Id;
                    db.SaveChanges();
                }

                else
                {
                    var taskList = new TaskList
                    {   
                        TaskName = model.Task,
                        NextExecution = model.NextExecution
                    };
                    db.TaskLists.Add(taskList);
                    db.SaveChanges();
                }

            }
        }
    }
}
