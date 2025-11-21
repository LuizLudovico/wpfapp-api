using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    public partial class PedidoEditWindow : Window
    {
        public PedidoEditWindow(PedidoEditViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

