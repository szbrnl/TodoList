//TODO
//[ ] animacja zanikania zadania przy jego wykonaniu
//[ ] ui dla textboxa do dodawania zadania
//[ ] zapis do pliku binarnego zamiast do bazy danych (?)
//[ ] sortowanie i pozycjonowanie zadań (wraz z zapisywaniem porządku)
//[ ] drag and drop dla listy zadan

using System.Windows;
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
            this.Closing += viewmodel.OnWindowClose;
        }

    }
}
