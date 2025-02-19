using Avalonia.Controls;
using DemoEkzZachet.Models;
using DemoEkzZachet.ViewModels;

namespace DemoEkzZachet.Views
{
    public partial class ProductEditorView : UserControl
    {
        public ProductEditorView()
        {

            DataContext = new ProductEditorVM();
            InitializeComponent();
        }

        public ProductEditorView(Tour Tour)
        {

            DataContext = new ProductEditorVM(Tour);
            InitializeComponent();
        }
    }
}
