//TODO refaktoryzacja konstruktora
//TODO możliwość usuwania zadań

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TodoList.DatabaseStuff;

namespace TodoList.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        #region Public methods

        private void AddNewTask()
        {
            var task = new TaskVievModel() { Complete = false, Name = "qwe" };
            Tasks.Add(task);
            _dbConnectionHandler.AddNewTask(new Task() { Name = task.Name, Complete = task.Complete });
        }

        #endregion

        #region Public properties
        public ObservableCollection<TaskVievModel> Tasks
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
            Tasks = new ObservableCollection<TaskVievModel>(
                _dbConnectionHandler.GetAllTasksFromDatabase().Tasks.Select(task =>
                    new TaskVievModel() { Complete = task.Complete, Name = task.Name }));



            //Przykładowe komendy 
            #region ClickedCommand definition

            ClickedCommand = new RelayCommand(() =>
            {
                //Tasks[0].Name = "123";
                //Tasks.Insert(1, new TaskVievModel() { Complete = false, Name = "nowy" });
                //Tasks.RemoveAt(2);
                //Tasks.RemoveAt(3);

                AddNewTask();
            });

            #endregion

            #region CCommand definition 

            CCommand = new RelayCommand(() =>
            {
                _dbConnectionHandler = new DatabaseConnectionHandler();
                Tasks = new ObservableCollection<TaskVievModel>(
                    _dbConnectionHandler.GetAllTasksFromDatabase().Tasks.Select(task =>
                        new TaskVievModel() { Complete = task.Complete, Name = task.Name }));
            });

            #endregion


        }

        #endregion

        #region Private members

        private ObservableCollection<TaskVievModel> _tasks;

        private DatabaseConnectionHandler _dbConnectionHandler = null;

        #endregion

        #region Commands

        public ICommand ClickedCommand { get; set; }
        public ICommand CCommand { get; set; }
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
