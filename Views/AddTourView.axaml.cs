using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DemoEkzZachet.ViewModels;

namespace DemoEkzZachet;

public partial class AddTourView : UserControl
{
    public AddTourView()
    {
        InitializeComponent();
        DataContext = new AddTourVM();
    }
}