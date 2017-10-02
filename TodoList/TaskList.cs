using System.Collections.Generic;

namespace TodoList
{
    public class TaskList
    {
        #region Private members

        private List<Task> _taskList;

        #endregion

        #region Public Properties 

        public List<Task> Tasks
        {
            get => _taskList;
            set => _taskList = value;
        }

        #endregion

        #region Constructors

        public TaskList()
        {
            _taskList = new List<Task>();
        }

        #endregion

        public void AddTask(Task task)
        {
            _taskList.Add(task);
        }
    }
}
