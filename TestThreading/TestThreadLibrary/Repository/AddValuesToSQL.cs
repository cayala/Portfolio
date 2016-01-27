using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestThreadLibrary.Models;
using TestThreadLibrary.Models.Entity;

namespace TestThreadLibrary.Repository
{
    public class AddValuesToSQL
    {
        public void SaveData(Threads model)
        {
            using (var db = new TrainingEntities())
            {
                var checkTaskExists = db.TaskLists.Where(t => t.TaskName.ToLower().Equals(model.Task.ToLower())).FirstOrDefault();

                var s = new MinMaxAvgThread
                {
                    TaskId = checkTaskExists.Id,
                    LastExecution = model.LastExecution,
                    Minimum = model.Min,
                    Maximum = model.Max,
                    Average = model.Average


                };

                db.MinMaxAvgThreads.Add(s);

                try
                {
                    db.SaveChanges();
                }

                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }

        }
    }
}
