using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TodoList
{
    public class DatabaseConnectionHandler
    {
        #region Private members

        private string dbName;

        private string tasksTableName;

        private SQLiteConnection dbConnection;

        #endregion

        #region Public Methods

        public TaskList GetAllTasksFromDatabase()
        {
            dbConnection =
                new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            string sql = "select * from tab1";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();


            TaskList list = new TaskList();

            while (reader.Read())
            {
                list.Tasks.Add(new Task() {Name = reader["name"].ToString(), Complete = (bool)reader["complete"]});                
            }

            dbConnection.Close();
            return list;
        }

        public void Connect()
        {
            
        }

        #endregion

        #region Public properties

        public bool Connected { get; private set; }

        #endregion


        public DatabaseConnectionHandler()
        {
            

        }

    }
}
