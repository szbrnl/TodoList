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
      

        #endregion

        #region Public properties

        public int ID { get; set; }

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
    }
}
