using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace TodoList.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        public static string newName;


        #region Private members

        private string _name;
        private bool _complete;
        private string _description;
        private bool _editing;
        private string _newName;

        #endregion

        #region Public properties

        public int ID { get; set; }

        public string NewName
        {
            get { return _newName; }
            set
            {
                if (_newName == value) return;
                _newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }



        public bool Editing
        {
            get => _editing;
            set
            {
                if (_editing == value) return;
                _editing = value;
                OnPropertyChanged(nameof(Editing));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool Complete
        {
            get => _complete;
            set
            {
                if (_complete == value) return;
                _complete = value;
                OnPropertyChanged(nameof(Complete));
            }
        }

        #endregion

        #region PropertyChanged stuff

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public TaskViewModel()
        {
            StartEditingCommand = new RelayCommand(parameter =>
            {
                var task = (TaskViewModel)parameter;

                task.Editing = true;
            });

            EndEditingCommand = new RelayCommand(parameter =>
            {
                var task = (TaskViewModel)parameter;
                task.Name = newName;

                var dc = (TaskListViewModel)Application.Current.MainWindow.DataContext;

                dc.EditTaskCommand.Execute(task);

                task.Editing = false;
            });

            GetNewNameCommand = new RelayCommand(param =>
            {
                newName = param.ToString();
            });

        }
        /// <summary>
        /// Shows additional menu with editing tools under the task
        /// </summary>
        public ICommand StartEditingCommand { get; set; }

        public ICommand EndEditingCommand { get; set; }

        public ICommand GetNewNameCommand { get; set; }
    }
}
