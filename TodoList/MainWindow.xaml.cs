//TODO
//[ ] !!! Styl dla textboxa żeby wyglądał jak label ale dało się go edytować
//[ ] animacja zanikania zadania przy jego wykonaniu
//[ ] ui dla textboxa do dodawania zadania
//[ ] zmiana bazy danych na nosql
//[ ] sortowanie i pozycjonowanie zadań (wraz z zapisywaniem porządku)
//[ ] drag and drop dla listy zadan

using System.Windows;
using System.Windows.Controls;
using TodoList.ViewModels;

namespace TodoList
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewmodel = new TaskListViewModel();
            this.DataContext = viewmodel;

            //Saving and syncing just before 
            this.Closing += viewmodel.OnWindowClose;
        }

        private void TaskEditTextBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            textBox.SelectionStart = textBox.Text.Length;
            textBox.Focus();
        }
        public string GetEditedTaskName()
        {
            return ((TextBox)this.FindName("TaskEditTextBox")).Text;
        }

    }
}
