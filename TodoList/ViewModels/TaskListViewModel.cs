using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace TodoList.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {

        #region Private members

        private ObservableCollection<TaskVievModel> _tasks;

        private DatabaseConnectionHandler dbConnectionHandler;

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

        #region Commands

        public ICommand ClickedCommand { get; set; }
        public ICommand CCommand { get; set; }

        #endregion

        #region Constructor
        public TaskListViewModel()
        {
            dbConnectionHandler = new DatabaseConnectionHandler();
            Tasks = new ObservableCollection<TaskVievModel>(
                dbConnectionHandler.GetAllTasksFromDatabase().Tasks.Select(task =>
                    new TaskVievModel() { Complete = task.Complete, Name = task.Name }));


            #region ClickedCommand definition

            ClickedCommand = new RelayCommand(() =>
            {
                Tasks[0].Name = "123";
                Tasks.Insert(1, new TaskVievModel() { Complete = false, Name = "nowy" });
                Tasks.RemoveAt(2);
                Tasks.RemoveAt(3);
            });

            #endregion

            #region CCommand definition 

            CCommand = new RelayCommand(() =>
            {
                dbConnectionHandler = new DatabaseConnectionHandler();
                Tasks = new ObservableCollection<TaskVievModel>(
                    dbConnectionHandler.GetAllTasksFromDatabase().Tasks.Select(task =>
                        new TaskVievModel() { Complete = task.Complete, Name = task.Name }));
            });

            #endregion

        }

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
