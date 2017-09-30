using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TodoList.DatabaseStuff
{
    public class DatabaseSyncQueue
    {
        #region Private members

        private List<DatabaseSyncQueueItem> _syncQueue;

        private DatabaseConnectionHandler _dbConnectionHandler;

        #endregion

        #region Constructor

        public DatabaseSyncQueue()
        {
            _syncQueue = new List<DatabaseSyncQueueItem>();
            _dbConnectionHandler = new DatabaseConnectionHandler();
        }

        #endregion

        #region Public methods

        public TaskList GetAllTasks()
        {
            return _dbConnectionHandler.GetAllTasksFromDatabase();
        }


        public void AddItemToQueue(Task task, SyncTaskType jobType)
        {
            _syncQueue.Add(new DatabaseSyncQueueItem() { TaskToSync = task, TaskType = jobType });

            //TODO sprawdzanie czy wymagana jest synchronizacja
        }


        public void ForceSync()
        {
            //TODO ma być async jakoś

            var queue = _syncQueue;
            _syncQueue = new List<DatabaseSyncQueueItem>();

            foreach (var item in queue)
            {
                switch (item.TaskType)
                {
                    case SyncTaskType.Add:
                        _dbConnectionHandler.AddNewTask(item.TaskToSync);
                        break;

                    case SyncTaskType.Delete:
                        _dbConnectionHandler.RemoveTaskById(item.TaskToSync.ID);
                        break;
                    case SyncTaskType.Update:
                        _dbConnectionHandler.UpdateTask(item.TaskToSync);
                        break;
                }
            }

            queue.Clear();
        }

        #endregion
    }
}
