using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class ProdutoViewModel : ViewModelBase
    {
        private readonly ProdutoService _produtoService;
        private ObservableCollection<Produto> _produtos;
        private Produto _produtoSelecionado;
        private string _filtroNome;

        public ObservableCollection<Produto> Produtos
        {
            get => _produtos;
            set => SetProperty(ref _produtos, value);
        }

        public Produto ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set => SetProperty(ref _produtoSelecionado, value);
        }

        public string FiltroNome
        {
            get => _filtroNome;
            set
            {
                SetProperty(ref _filtroNome, value);
                AplicarFiltro();
            }
        }

        public ICommand AdicionarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }
        public ICommand LimparCommand { get; }

        public ProdutoViewModel()
        {
            _produtoService = new ProdutoService();
            
            AdicionarCommand = new RelayCommand(param => Adicionar());
            EditarCommand = new RelayCommand(param => Editar(), param => ProdutoSelecionado != null);
            ExcluirCommand = new RelayCommand(param => Excluir(), param => ProdutoSelecionado != null);
            LimparCommand = new RelayCommand(param => Limpar());

            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            var produtos = _produtoService.ObterTodos();
            Produtos = new ObservableCollection<Produto>(produtos);
        }

        private void AplicarFiltro()
        {
            if (string.IsNullOrWhiteSpace(FiltroNome))
            {
                CarregarProdutos();
            }
            else
            {
                var produtosFiltrados = _produtoService.BuscarPorNome(FiltroNome);
                Produtos = new ObservableCollection<Produto>(produtosFiltrados);
            }
        }

        private void Adicionar()
        {
            var novoProduto = new Produto
            {
                Nome = "Novo Produto",
                Descricao = "",
                Preco = 0,
                QuantidadeEstoque = 0,
                Categoria = "",
                CodigoBarras = ""
            };

            _produtoService.Adicionar(novoProduto);
            CarregarProdutos();
            ProdutoSelecionado = Produtos.FirstOrDefault(p => p.Id == novoProduto.Id);
        }

        private void Editar()
        {
            if (ProdutoSelecionado != null)
            {
                _produtoService.Atualizar(ProdutoSelecionado);
                MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Excluir()
        {
            if (ProdutoSelecionado != null)
            {
                var resultado = MessageBox.Show(
                    $"Deseja realmente excluir {ProdutoSelecionado.Nome}?",
                    "Confirmar Exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    _produtoService.Excluir(ProdutoSelecionado.Id);
                    CarregarProdutos();
                    ProdutoSelecionado = null;
                }
            }
        }

        private void Limpar()
        {
            ProdutoSelecionado = null;
            FiltroNome = string.Empty;
        }
    }
}

