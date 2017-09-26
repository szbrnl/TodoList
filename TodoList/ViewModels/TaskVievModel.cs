using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace TodoList.ViewModels
{
    public class TaskVievModel : INotifyPropertyChanged
    {
        #region Private members

        private string _name;
        private bool _complete;
        private string _description;

        #endregion

        public TaskVievModel()
        {
           // DeleteTaskCommand = new RelayCommand(() => MessageBox.Show("as"));
        }

        #region Public properties

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


        public ICommand DeleteTaskCommand { get; set; }



        #region PropertyChanged stuff

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
