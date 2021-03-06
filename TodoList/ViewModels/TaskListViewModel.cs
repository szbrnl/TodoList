﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TodoList.DatabaseStuff;

namespace TodoList.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        #region Constructor

        public TaskListViewModel()
        {
            _syncQueue = new DatabaseSyncQueue();

            Tasks = new ObservableCollection<TaskViewModel>(
                _syncQueue.GetAllTasks().Tasks.Select(task =>
                    new TaskViewModel {Complete = task.Complete, Name = task.Name, ID = task.ID}));

            CreateCommands();
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

        #region Public methods

        /// <summary>
        ///     Function called just before the application closes
        ///     Forces the synchronization with the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnWindowClose(object sender, CancelEventArgs e)
        {
            _syncQueue.ForceSync();
        }

        #endregion

        #region Private methods

        private void CreateCommands()
        {
            DeleteTaskCommand = new RelayCommand(parameter =>
            {
                var task = (TaskViewModel) parameter;
                Tasks.Remove(task);
                _syncQueue.AddItemToQueue(new Task {ID = task.ID}, SyncTaskType.Delete);
            });

            TaskDoneChangedCommand = new RelayCommand(parameter =>
            {
                var task = (TaskViewModel) parameter;

                //Prowizoryczne przesuwanie wykonanego zadania na koniec listy
                Tasks.Remove(task);
                Tasks.Add(task);
                //TODO jakims magicznym soposobem należy zapamiętywać kolejność zadań

                _syncQueue.AddItemToQueue(new Task {Complete = task.Complete, Name = task.Name, ID = task.ID},
                    SyncTaskType.Update);
            });

            AddTaskCommand = new RelayCommand(parameter =>
            {
                var textbox = (TextBox) parameter;
                if (textbox.Text == "")
                    return;

                _syncQueue.AddItemToQueue(new Task {Complete = false, Name = textbox.Text}, SyncTaskType.Add);
                textbox.Clear();
                _syncQueue.ForceSync();
                Tasks.Clear();
                Tasks = new ObservableCollection<TaskViewModel>(
                    _syncQueue.GetAllTasks().Tasks.Select(task =>
                        new TaskViewModel {Complete = task.Complete, Name = task.Name, ID = task.ID}));
            });

            EditTaskCommand = new RelayCommand(param =>
            {
                var task = (TaskViewModel) param;

                _syncQueue.AddItemToQueue(new Task {Name = task.Name, ID = task.ID, Complete = task.Complete},
                    SyncTaskType.Update);
            });
        }

        #endregion

        #region Private members

        private ObservableCollection<TaskViewModel> _tasks;

        private readonly DatabaseSyncQueue _syncQueue;

        #endregion

        #region Commands

        /// <summary>
        ///     Switches the task to done/undone
        /// </summary>
        public ICommand TaskDoneChangedCommand { get; set; }

        /// <summary>
        ///     Command to delete selected task from viewmodel
        /// </summary>
        public ICommand DeleteTaskCommand { get; set; }

        public ICommand AddTaskCommand { get; set; }

        public ICommand EditTaskCommand { get; set; }

        #endregion

        #region PropertyChanged stuff

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}