using System.Collections.Generic;

namespace TodoList
{
    public class TaskList
    {
        #region Constructors

        public TaskList()
        {
            Tasks = new List<Task>();
        }

        #endregion

        #region Public Properties 

        public List<Task> Tasks { get; set; }

        #endregion

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }
    }
}