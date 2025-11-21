using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class PedidoEditViewModel : ViewModelBase
    {
        private readonly ProdutoService _produtoService;
        private Pedido _pedido;
        private ObservableCollection<Produto> _produtosDisponiveis;
        private Produto _produtoSelecionado;
        private int _quantidade;
        private ItemPedido _itemSelecionado;

        public Pedido Pedido
        {
            get => _pedido;
            set => SetProperty(ref _pedido, value);
        }

        public ObservableCollection<Produto> ProdutosDisponiveis
        {
            get => _produtosDisponiveis;
            set => SetProperty(ref _produtosDisponiveis, value);
        }

        public Produto ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set => SetProperty(ref _produtoSelecionado, value);
        }

        public int Quantidade
        {
            get => _quantidade;
            set => SetProperty(ref _quantidade, value);
        }

        public ItemPedido ItemSelecionado
        {
            get => _itemSelecionado;
            set => SetProperty(ref _itemSelecionado, value);
        }

        public ICommand AdicionarProdutoCommand { get; }
        public ICommand RemoverProdutoCommand { get; }
        public ICommand FinalizarPedidoCommand { get; }
        public ICommand CancelarCommand { get; }

        public bool PedidoFinalizado { get; private set; }

        public PedidoEditViewModel(Pedido pedido)
        {
            _produtoService = new ProdutoService();
            Pedido = pedido;
            Quantidade = 1;

            AdicionarProdutoCommand = new RelayCommand(param => AdicionarProduto(), param => ProdutoSelecionado != null && Quantidade > 0);
            RemoverProdutoCommand = new RelayCommand(param => RemoverProduto(), param => ItemSelecionado != null);
            FinalizarPedidoCommand = new RelayCommand(param => FinalizarPedido(), param => Pedido.Itens.Any());
            CancelarCommand = new RelayCommand(param => Cancelar());

            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            var produtos = _produtoService.ObterTodos();
            ProdutosDisponiveis = new ObservableCollection<Produto>(produtos);
        }

        private void AdicionarProduto()
        {
            if (ProdutoSelecionado != null && Quantidade > 0)
            {
                // Verificar se há estoque suficiente
                if (ProdutoSelecionado.QuantidadeEstoque < Quantidade)
                {
                    MessageBox.Show($"Estoque insuficiente! Disponível: {ProdutoSelecionado.QuantidadeEstoque}",
                        "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Verificar se o produto já está no pedido
                var itemExistente = Pedido.Itens.FirstOrDefault(i => i.ProdutoId == ProdutoSelecionado.Id);
                if (itemExistente != null)
                {
                    itemExistente.Quantidade += Quantidade;
                    // Subtotal é calculado automaticamente pela propriedade
                }
                else
                {
                    var novoItem = new ItemPedido
                    {
                        ProdutoId = ProdutoSelecionado.Id,
                        NomeProduto = ProdutoSelecionado.Nome,
                        Quantidade = Quantidade,
                        PrecoUnitario = ProdutoSelecionado.Preco
                        // Subtotal é calculado automaticamente: Quantidade * PrecoUnitario
                    };
                    Pedido.Itens.Add(novoItem);
                }

                // Atualizar valor total do pedido
                Pedido.CalcularTotal();
                OnPropertyChanged(nameof(Pedido));

                // Reset
                ProdutoSelecionado = null;
                Quantidade = 1;
            }
        }

        private void RemoverProduto()
        {
            if (ItemSelecionado != null)
            {
                Pedido.Itens.Remove(ItemSelecionado);
                Pedido.CalcularTotal();
                OnPropertyChanged(nameof(Pedido));
                ItemSelecionado = null;
            }
        }

        private void FinalizarPedido()
        {
            if (!Pedido.Itens.Any())
            {
                MessageBox.Show("Adicione pelo menos um produto ao pedido!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var resultado = MessageBox.Show(
                $"Finalizar pedido no valor de {Pedido.ValorTotal:C}?\n\nApós finalizado, o pedido não poderá ser alterado.",
                "Finalizar Pedido",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                PedidoFinalizado = true;
                
                // Fechar a janela
                Application.Current.Windows.OfType<Window>()
                    .FirstOrDefault(w => w.DataContext == this)?.Close();
            }
        }

        private void Cancelar()
        {
            var resultado = MessageBox.Show(
                "Tem certeza que deseja cancelar? O pedido será descartado.",
                "Cancelar",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                PedidoFinalizado = false;
                
                // Fechar a janela
                Application.Current.Windows.OfType<Window>()
                    .FirstOrDefault(w => w.DataContext == this)?.Close();
            }
        }
    }
}

