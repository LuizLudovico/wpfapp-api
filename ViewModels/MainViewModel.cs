using System.Windows.Input;

namespace WpfApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public ICommand NavigateToPessoasCommand { get; }
        public ICommand NavigateToProdutosCommand { get; }
        public ICommand NavigateToPedidosCommand { get; }

        public MainViewModel()
        {
            NavigateToPessoasCommand = new RelayCommand(param => NavigateToPessoas());
            NavigateToProdutosCommand = new RelayCommand(param => NavigateToProdutos());
            NavigateToPedidosCommand = new RelayCommand(param => NavigateToPedidos());

            // Inicia na tela de Pessoas
            NavigateToPessoas();
        }

        private void NavigateToPessoas()
        {
            CurrentViewModel = new PessoaViewModel();
        }

        private void NavigateToProdutos()
        {
            CurrentViewModel = new ProdutoViewModel();
        }

        private void NavigateToPedidos()
        {
            CurrentViewModel = new PedidoViewModel();
        }
    }
}

