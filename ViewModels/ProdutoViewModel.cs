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
        private string _tipoFiltroSelecionado;
        private string _valorFiltro;
        private string _valorMinimo;
        private string _valorMaximo;

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

        public string TipoFiltroSelecionado
        {
            get => _tipoFiltroSelecionado;
            set
            {
                SetProperty(ref _tipoFiltroSelecionado, value);
                // Limpar valores ao trocar de filtro
                ValorFiltro = string.Empty;
                ValorMinimo = string.Empty;
                ValorMaximo = string.Empty;
                OnPropertyChanged(nameof(MostrarFiltroTexto));
                OnPropertyChanged(nameof(MostrarFiltroFaixa));
            }
        }

        public string ValorFiltro
        {
            get => _valorFiltro;
            set
            {
                SetProperty(ref _valorFiltro, value);
                AplicarFiltro();
            }
        }

        public string ValorMinimo
        {
            get => _valorMinimo;
            set
            {
                SetProperty(ref _valorMinimo, value);
                AplicarFiltro();
            }
        }

        public string ValorMaximo
        {
            get => _valorMaximo;
            set
            {
                SetProperty(ref _valorMaximo, value);
                AplicarFiltro();
            }
        }

        public bool MostrarFiltroTexto => TipoFiltroSelecionado == "Nome" || TipoFiltroSelecionado == "Código";
        public bool MostrarFiltroFaixa => TipoFiltroSelecionado == "Faixa de Valor";

        public ICommand AdicionarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand SalvarCommand { get; }
        public ICommand ExcluirCommand { get; }

        public ProdutoViewModel()
        {
            _produtoService = new ProdutoService();
            
            AdicionarCommand = new RelayCommand(param => Adicionar());
            EditarCommand = new RelayCommand(param => Editar(), param => ProdutoSelecionado != null);
            SalvarCommand = new RelayCommand(param => Salvar(), param => ProdutoSelecionado != null);
            ExcluirCommand = new RelayCommand(param => Excluir(), param => ProdutoSelecionado != null);

            TipoFiltroSelecionado = "Nome";
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            var produtos = _produtoService.ObterTodos();
            Produtos = new ObservableCollection<Produto>(produtos);
        }

        private void AplicarFiltro()
        {
            if (TipoFiltroSelecionado == "Nome")
            {
                if (string.IsNullOrWhiteSpace(ValorFiltro))
                {
                    CarregarProdutos();
                }
                else
                {
                    var produtosFiltrados = _produtoService.BuscarPorNome(ValorFiltro);
                    Produtos = new ObservableCollection<Produto>(produtosFiltrados);
                }
            }
            else if (TipoFiltroSelecionado == "Código")
            {
                if (string.IsNullOrWhiteSpace(ValorFiltro))
                {
                    CarregarProdutos();
                }
                else
                {
                    var produtosFiltrados = _produtoService.BuscarPorCodigo(ValorFiltro);
                    Produtos = new ObservableCollection<Produto>(produtosFiltrados);
                }
            }
            else if (TipoFiltroSelecionado == "Faixa de Valor")
            {
                decimal? min = null;
                decimal? max = null;

                if (!string.IsNullOrWhiteSpace(ValorMinimo) && decimal.TryParse(ValorMinimo, out decimal minValue))
                {
                    min = minValue;
                }

                if (!string.IsNullOrWhiteSpace(ValorMaximo) && decimal.TryParse(ValorMaximo, out decimal maxValue))
                {
                    max = maxValue;
                }

                var produtosFiltrados = _produtoService.BuscarPorFaixaDeValor(min, max);
                Produtos = new ObservableCollection<Produto>(produtosFiltrados);
            }
            else
            {
                CarregarProdutos();
            }
        }

        private void Adicionar()
        {
            var novoProduto = new Produto
            {
                Nome = "Novo Produto",
                Codigo = "",
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
            // Botão Editar habilita a edição do produto selecionado
            // No WPF com DataBinding, a edição já é automática ao selecionar
            if (ProdutoSelecionado != null)
            {
                MessageBox.Show(
                    $"Editando: {ProdutoSelecionado.Nome}\n\nModifique os campos desejados e clique em 💾 Salvar.",
                    "Modo de Edição",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void Salvar()
        {
            if (ProdutoSelecionado != null)
            {
                _produtoService.Atualizar(ProdutoSelecionado);
                MessageBox.Show("Produto salvo com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Limpa o formulário após salvar
                ProdutoSelecionado = null;
                CarregarProdutos();
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
    }
}

