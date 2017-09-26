using System.Data.SQLite;

namespace TodoList.DatabaseStuff
{
    public class DatabaseConnectionHandler
    {
        #region Static strings

        private static string _selectAllTasksCommand;

        #endregion

        #region Private members

        private string _dbName;

        private string _tasksTableName;

        private SQLiteConnection _dbConnection;

        #endregion

        #region Public properties

        public bool Connected { get; private set; } = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns all tasks from the selected database
        /// </summary>
        /// <returns><see cref="TaskList"/> with tasks</returns>
        public TaskList GetAllTasksFromDatabase()
        {
            Connect();
            var command = new SQLiteCommand(_selectAllTasksCommand, _dbConnection);
            var reader = command.ExecuteReader();


            var list = new TaskList();

            while (reader.Read())
            {
                list.Tasks.Add(new Task() { Name = reader["name"].ToString(), Complete = (bool)reader["complete"] });
            }

            Disconnect();
            return list;
        }

        /// <summary>
        /// Adds a new <see cref="Task"/> to the database
        /// </summary>
        /// <param name="task">Task to be added</param>
        public void AddNewTask(Task task)
        {
            Connect();

            var sql = $"insert into {_tasksTableName} (name, complete) values ('{task.Name}', 0)";
            var command = new SQLiteCommand(sql, _dbConnection);
            command.ExecuteNonQuery();

            Disconnect();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Connects to the database if necessary
        /// </summary>
        private void Connect()
        {
            if (Connected) return;

            _dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            _dbConnection.Open();
            Connected = true;
        }

        /// <summary>
        /// Closes the connection with the database
        /// </summary>
        private void Disconnect()
        {
            if (!Connected) return;

            _dbConnection.Close();
            Connected = false;
        }

        #endregion

        #region Default constructor

        public DatabaseConnectionHandler()
        {
            _tasksTableName = "tab1";
            _selectAllTasksCommand = $"select * from {_tasksTableName}";

        }

        #endregion
    }
}
