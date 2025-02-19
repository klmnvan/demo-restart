using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DemoEkzZachet.Models;
using DemoEkzZachet.ViewModels;

namespace DemoEkzZachet;

public partial class AddTourView : UserControl
{
    public AddTourView()
    {
        InitializeComponent();
        DataContext = new AddTourVM();
    }

    public AddTourView(Tour tour)
    {
        InitializeComponent();
        DataContext = new AddTourVM(tour);
    }
}