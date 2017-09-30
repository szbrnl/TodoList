using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.DatabaseStuff
{
    public class DatabaseSyncQueueItem
    {
        public SyncTaskType TaskType { get; set; }

        public Task TaskToSync { get; set; }
    }
}
