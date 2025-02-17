using Avalonia.Controls;
using DemoEkzZachet.Models;
using ReactiveUI;

namespace DemoEkzZachet.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public static MainWindowViewModel Instance;
        public static ToursContext Context = new ToursContext();

        UserControl _currentPage;

        public UserControl CurrentPage { get => _currentPage; set => this.RaiseAndSetIfChanged(ref _currentPage, value); }

        public MainWindowViewModel()
        {
            Instance = this;
            CurrentPage = new ListProductView();
        }
    }
}
