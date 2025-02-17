using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DemoEkzZachet.ViewModels;

namespace DemoEkzZachet;

public partial class ListProductView : UserControl
{
    public ListProductView()
    {
        InitializeComponent();
        DataContext = new ListProductVM();
    }
}