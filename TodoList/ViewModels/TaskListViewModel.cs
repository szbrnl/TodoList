using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TodoList.DatabaseStuff;

namespace TodoList.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        #region Public methods

        private void AddNewTask()
        {
            var task = new TaskViewModel() { Complete = false, Name = "qwe" };
            Tasks.Add(task);
            _dbConnectionHandler.AddNewTask(new Task() { Name = task.Name, Complete = task.Complete, ID = task.ID});
        }

        #endregion

        #region Public properties
        public ObservableCollection<TaskViewModel> Tasks
        {
            get => _tasks;
            set
            {
                if (_tasks == value)
                    return;
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }
        #endregion

        #region Constructor
        public TaskListViewModel()
        {
            _dbConnectionHandler = new DatabaseConnectionHandler();

            Tasks = new ObservableCollection<TaskViewModel>(
                _dbConnectionHandler.GetAllTasksFromDatabase().Tasks.Select(task =>
                    new TaskViewModel() { Complete = task.Complete, Name = task.Name, ID = task.ID}));


            DeleteTaskCommand = new RelayCommand(parameter =>
            {
                var task = (TaskViewModel) parameter;
                Tasks.Remove(task);
                _dbConnectionHandler.RemoveTaskById(task.ID);
            });

            TaskDoneChangedCommand = new RelayCommand(parameter =>
            {
                var task = (TaskViewModel) parameter;
                _dbConnectionHandler.UpdateTask(new Task() {Complete = task.Complete, Name = task.Name, ID = task.ID});
            });
        }

        #endregion

        #region Private members

        private ObservableCollection<TaskViewModel> _tasks;

        private DatabaseConnectionHandler _dbConnectionHandler = null;

        #endregion

        #region Commands
        
        /// <summary>
        /// Switches the task to done/undone
        /// </summary>
        public ICommand TaskDoneChangedCommand { get; set; }

        /// <summary>
        /// Command to delete selected task from viewmodel
        /// </summary>
        public ICommand DeleteTaskCommand { get; set; }

        #endregion

        #region PropertyChanged stuff
        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
