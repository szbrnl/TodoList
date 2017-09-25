using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
