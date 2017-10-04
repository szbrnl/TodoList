using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace TodoList.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {

        #region Private members

        private string _name;

        private bool _complete;

        private string _description;

        private bool _editing;

        /// <summary>
        /// Name given after user's editing
        /// </summary>
        private string _newName;

        #endregion

        #region Public properties

        public int ID { get; set; }

        /// <summary>
        /// Name given after user's editing
        /// </summary>
        public string NewName
        {
            get => _newName;
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

        #region Commands

        /// <summary>
        /// Shows TextBox instead of label of edited task, allows user to edit the name of task
        /// </summary>
        public ICommand StartEditingCommand { get; set; }

        /// <summary>
        /// Hides TextBox for task editing and shows label, saves the changes and asks TaskListViewModel to sync them
        /// </summary>
        public ICommand EndEditingCommand { get; set; }

        #endregion

        #region Private Methods

        private void CreateCommands()
        {
            StartEditingCommand = new RelayCommand(parameter =>
            {
                var task = (TaskViewModel)parameter;

                NewName = Name;

                task.Editing = true;
            });

            EndEditingCommand = new RelayCommand(parameter =>
            {
                var task = (TaskViewModel)parameter;

                //If nothing has changed - no syncing needed
                if (task.Name == NewName)
                {
                    task.Editing = false;
                    return;
                }

                task.Name = NewName;

                var dc = (TaskListViewModel)Application.Current.MainWindow.DataContext;

                //Asks viewModel to save changes and sync
                dc.EditTaskCommand.Execute(task);

                task.Editing = false;
            });

        }

        #endregion

        #region Default Constructor

        public TaskViewModel()
        {
            CreateCommands();
        }

        #endregion

        #region PropertyChanged stuff

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
