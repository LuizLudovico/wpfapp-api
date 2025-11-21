using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Views;

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
        private string _filtroSelecionado;

        public ObservableCollection<Pedido> Pedidos
        {
            get => _pedidos;
            set => SetProperty(ref _pedidos, value);
        }

        public Pedido PedidoSelecionado
        {
            get => _pedidoSelecionado;
            set
            {
                SetProperty(ref _pedidoSelecionado, value);
                OnPropertyChanged(nameof(PedidoPodeSerEditado));
            }
        }

        public bool PedidoPodeSerEditado => PedidoSelecionado != null && PedidoSelecionado.Status == StatusPedido.Pendente;

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

        public string FiltroSelecionado
        {
            get => _filtroSelecionado;
            set
            {
                SetProperty(ref _filtroSelecionado, value);
                AplicarFiltro();
            }
        }

        public ICommand NovoPedidoCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }
        public ICommand FinalizarCommand { get; }
        public ICommand MarcarComoPagoCommand { get; }
        public ICommand MarcarComoEnviadoCommand { get; }
        public ICommand MarcarComoRecebidoCommand { get; }

        public PedidoViewModel()
        {
            _pedidoService = new PedidoService();
            _pessoaService = new PessoaService();
            _produtoService = new ProdutoService();
            
            NovoPedidoCommand = new RelayCommand(param => NovoPedido());
            EditarCommand = new RelayCommand(param => Editar(), param => PedidoSelecionado != null);
            ExcluirCommand = new RelayCommand(param => Excluir(), param => PedidoSelecionado != null);
            FinalizarCommand = new RelayCommand(param => Finalizar(), param => PedidoSelecionado != null);
            MarcarComoPagoCommand = new RelayCommand(param => MarcarComoPago(), param => PedidoSelecionado != null);
            MarcarComoEnviadoCommand = new RelayCommand(param => MarcarComoEnviado(), param => PedidoSelecionado != null);
            MarcarComoRecebidoCommand = new RelayCommand(param => MarcarComoRecebido(), param => PedidoSelecionado != null);

            FiltroSelecionado = "Todos";
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

            // Abrir janela de seleção de pessoa
            var selecaoWindow = new SelecionarPessoaWindow(Clientes);
            selecaoWindow.ShowDialog();

            if (!selecaoWindow.Confirmado || selecaoWindow.PessoaSelecionada == null)
                return;

            var clienteSelecionado = selecaoWindow.PessoaSelecionada;

            // Criar pedido temporário
            var novoPedido = new Pedido
            {
                PessoaId = clienteSelecionado.Id,
                NomeCliente = clienteSelecionado.Nome,
                Status = StatusPedido.Pendente,
                FormaPagamento = FormaPagamento.Dinheiro
            };

            // Abrir janela de edição
            var viewModel = new PedidoEditViewModel(novoPedido);
            var window = new PedidoEditWindow(viewModel);
            window.ShowDialog();

            // Se o pedido foi finalizado, salvar
            if (viewModel.PedidoFinalizado)
            {
                _pedidoService.Adicionar(novoPedido);
                CarregarDados();
                MessageBox.Show($"Pedido finalizado com sucesso!\n\nValor Total: {novoPedido.ValorTotal:C}", 
                    "Pedido Criado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // Se não foi finalizado, o pedido é descartado (não faz nada)
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
                AplicarFiltro();
            }
        }

        private void MarcarComoPago()
        {
            if (PedidoSelecionado != null)
            {
                _pedidoService.AlterarStatus(PedidoSelecionado.Id, StatusPedido.Pago);
                MessageBox.Show("Pedido marcado como Pago!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                AplicarFiltro();
            }
        }

        private void MarcarComoEnviado()
        {
            if (PedidoSelecionado != null)
            {
                _pedidoService.AlterarStatus(PedidoSelecionado.Id, StatusPedido.Enviado);
                MessageBox.Show("Pedido marcado como Enviado!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                AplicarFiltro();
            }
        }

        private void MarcarComoRecebido()
        {
            if (PedidoSelecionado != null)
            {
                _pedidoService.AlterarStatus(PedidoSelecionado.Id, StatusPedido.Recebido);
                MessageBox.Show("Pedido marcado como Recebido!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                AplicarFiltro();
            }
        }

        private void AplicarFiltro()
        {
            var todosPedidos = _pedidoService.ObterTodos();

            switch (FiltroSelecionado)
            {
                case "Pendentes":
                    todosPedidos = _pedidoService.ObterPorStatus(StatusPedido.Pendente);
                    break;
                case "Pagos":
                    todosPedidos = _pedidoService.ObterPorStatus(StatusPedido.Pago);
                    break;
                case "Enviados":
                    todosPedidos = _pedidoService.ObterPorStatus(StatusPedido.Enviado);
                    break;
                case "Recebidos":
                    todosPedidos = _pedidoService.ObterPorStatus(StatusPedido.Recebido);
                    break;
                default: // "Todos"
                    break;
            }

            Pedidos = new ObservableCollection<Pedido>(todosPedidos);
        }
    }
}

