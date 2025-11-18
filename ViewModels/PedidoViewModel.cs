using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class PedidoViewModel : ViewModelBase
    {
        private readonly PedidoService _pedidoService;
        private readonly PessoaService _pessoaService;
        private readonly ProdutoService _produtoService;
        
        private ObservableCollection<Pedido> _pedidos;
        private Pedido _pedidoSelecionado;
        private ObservableCollection<Pessoa> _clientes;
        private ObservableCollection<Produto> _produtos;

        public ObservableCollection<Pedido> Pedidos
        {
            get => _pedidos;
            set => SetProperty(ref _pedidos, value);
        }

        public Pedido PedidoSelecionado
        {
            get => _pedidoSelecionado;
            set => SetProperty(ref _pedidoSelecionado, value);
        }

        public ObservableCollection<Pessoa> Clientes
        {
            get => _clientes;
            set => SetProperty(ref _clientes, value);
        }

        public ObservableCollection<Produto> Produtos
        {
            get => _produtos;
            set => SetProperty(ref _produtos, value);
        }

        public ICommand NovoPedidoCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }
        public ICommand FinalizarCommand { get; }

        public PedidoViewModel()
        {
            _pedidoService = new PedidoService();
            _pessoaService = new PessoaService();
            _produtoService = new ProdutoService();
            
            NovoPedidoCommand = new RelayCommand(param => NovoPedido());
            EditarCommand = new RelayCommand(param => Editar(), param => PedidoSelecionado != null);
            ExcluirCommand = new RelayCommand(param => Excluir(), param => PedidoSelecionado != null);
            FinalizarCommand = new RelayCommand(param => Finalizar(), param => PedidoSelecionado != null);

            CarregarDados();
        }

        private void CarregarDados()
        {
            var pedidos = _pedidoService.ObterTodos();
            Pedidos = new ObservableCollection<Pedido>(pedidos);

            var clientes = _pessoaService.ObterTodas();
            Clientes = new ObservableCollection<Pessoa>(clientes);

            var produtos = _produtoService.ObterTodos();
            Produtos = new ObservableCollection<Produto>(produtos);
        }

        private void NovoPedido()
        {
            if (!Clientes.Any())
            {
                MessageBox.Show("Cadastre pelo menos um cliente antes de criar um pedido.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Produtos.Any())
            {
                MessageBox.Show("Cadastre pelo menos um produto antes de criar um pedido.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var novoPedido = new Pedido
            {
                PessoaId = Clientes.First().Id,
                NomeCliente = Clientes.First().Nome,
                Status = StatusPedido.Pendente,
                Observacoes = ""
            };

            _pedidoService.Adicionar(novoPedido);
            CarregarDados();
            PedidoSelecionado = Pedidos.FirstOrDefault(p => p.Id == novoPedido.Id);
        }

        private void Editar()
        {
            if (PedidoSelecionado != null)
            {
                _pedidoService.Atualizar(PedidoSelecionado);
                MessageBox.Show("Pedido atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                CarregarDados();
            }
        }

        private void Excluir()
        {
            if (PedidoSelecionado != null)
            {
                var resultado = MessageBox.Show(
                    $"Deseja realmente excluir o pedido #{PedidoSelecionado.Id}?",
                    "Confirmar Exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    _pedidoService.Excluir(PedidoSelecionado.Id);
                    CarregarDados();
                    PedidoSelecionado = null;
                }
            }
        }

        private void Finalizar()
        {
            if (PedidoSelecionado != null && PedidoSelecionado.Status == StatusPedido.Pendente)
            {
                _pedidoService.AlterarStatus(PedidoSelecionado.Id, StatusPedido.Recebido);
                MessageBox.Show("Pedido finalizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                CarregarDados();
            }
        }
    }
}

