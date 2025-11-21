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
    public class PessoaViewModel : ViewModelBase
    {
        private readonly PessoaService _pessoaService;
        private readonly PedidoService _pedidoService;
        private ObservableCollection<Pessoa> _pessoas;
        private ObservableCollection<Pedido> _pedidosDaPessoa;
        private Pessoa _pessoaSelecionada;
        private Pedido _pedidoDaPessoaSelecionado;
        private string _tipoFiltroSelecionado;
        private string _valorFiltro;
        private string _filtroPedidosDaPessoa;

        public ObservableCollection<Pessoa> Pessoas
        {
            get => _pessoas;
            set => SetProperty(ref _pessoas, value);
        }

        public Pessoa PessoaSelecionada
        {
            get => _pessoaSelecionada;
            set
            {
                SetProperty(ref _pessoaSelecionada, value);
                CarregarPedidosDaPessoa();
            }
        }

        public ObservableCollection<Pedido> PedidosDaPessoa
        {
            get => _pedidosDaPessoa;
            set => SetProperty(ref _pedidosDaPessoa, value);
        }

        public Pedido PedidoDaPessoaSelecionado
        {
            get => _pedidoDaPessoaSelecionado;
            set => SetProperty(ref _pedidoDaPessoaSelecionado, value);
        }

        public string FiltroPedidosDaPessoa
        {
            get => _filtroPedidosDaPessoa;
            set
            {
                SetProperty(ref _filtroPedidosDaPessoa, value);
                CarregarPedidosDaPessoa();
            }
        }

        public string TipoFiltroSelecionado
        {
            get => _tipoFiltroSelecionado;
            set
            {
                SetProperty(ref _tipoFiltroSelecionado, value);
                AplicarFiltro();
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

        public ICommand AdicionarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand SalvarCommand { get; }
        public ICommand ExcluirCommand { get; }
        public ICommand IncluirPedidoCommand { get; }
        public ICommand MarcarPedidoComoPagoCommand { get; }
        public ICommand MarcarPedidoComoEnviadoCommand { get; }
        public ICommand MarcarPedidoComoRecebidoCommand { get; }

        public PessoaViewModel()
        {
            _pessoaService = new PessoaService();
            _pedidoService = new PedidoService();
            
            AdicionarCommand = new RelayCommand(param => Adicionar());
            EditarCommand = new RelayCommand(param => Editar(), param => PessoaSelecionada != null);
            SalvarCommand = new RelayCommand(param => Salvar(), param => PessoaSelecionada != null);
            ExcluirCommand = new RelayCommand(param => Excluir(), param => PessoaSelecionada != null);
            IncluirPedidoCommand = new RelayCommand(param => IncluirPedido(), param => PessoaSelecionada != null);
            MarcarPedidoComoPagoCommand = new RelayCommand(param => MarcarPedidoComoPago(), param => PedidoDaPessoaSelecionado != null);
            MarcarPedidoComoEnviadoCommand = new RelayCommand(param => MarcarPedidoComoEnviado(), param => PedidoDaPessoaSelecionado != null);
            MarcarPedidoComoRecebidoCommand = new RelayCommand(param => MarcarPedidoComoRecebido(), param => PedidoDaPessoaSelecionado != null);

            TipoFiltroSelecionado = "Nome";
            FiltroPedidosDaPessoa = "Todos";
            PedidosDaPessoa = new ObservableCollection<Pedido>();
            CarregarPessoas();
        }

        private void CarregarPessoas()
        {
            var pessoas = _pessoaService.ObterTodas();
            Pessoas = new ObservableCollection<Pessoa>(pessoas);
        }

        private void AplicarFiltro()
        {
            if (string.IsNullOrWhiteSpace(ValorFiltro))
            {
                CarregarPessoas();
            }
            else
            {
                List<Pessoa> pessoasFiltradas;

                if (TipoFiltroSelecionado == "Nome")
                {
                    pessoasFiltradas = _pessoaService.BuscarPorNome(ValorFiltro);
                }
                else if (TipoFiltroSelecionado == "CPF")
                {
                    pessoasFiltradas = _pessoaService.ObterTodas()
                        .Where(p => p.CPF != null && p.CPF.Contains(ValorFiltro))
                        .OrderBy(p => p.Nome)
                        .ToList();
                }
                else
                {
                    pessoasFiltradas = _pessoaService.ObterTodas();
                }

                Pessoas = new ObservableCollection<Pessoa>(pessoasFiltradas);
            }
        }

        private void Adicionar()
        {
            var novaPessoa = new Pessoa
            {
                Nome = "Nova Pessoa",
                CPF = "",
                Email = "",
                Telefone = "",
                DataNascimento = DateTime.Now.AddYears(-18),
                Endereco = ""
            };

            _pessoaService.Adicionar(novaPessoa);
            CarregarPessoas();
            PessoaSelecionada = Pessoas.FirstOrDefault(p => p.Id == novaPessoa.Id);
        }

        private void Editar()
        {
            // Botão Editar habilita a edição da pessoa selecionada
            // No WPF com DataBinding, a edição já é automática ao selecionar
            if (PessoaSelecionada != null)
            {
                MessageBox.Show(
                    $"Editando: {PessoaSelecionada.Nome}\n\nModifique os campos desejados e clique em 💾 Salvar.",
                    "Modo de Edição",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void Salvar()
        {
            if (PessoaSelecionada != null)
            {
                _pessoaService.Atualizar(PessoaSelecionada);
                MessageBox.Show("Pessoa salva com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Limpa o formulário após salvar
                PessoaSelecionada = null;
                CarregarPessoas();
            }
        }

        private void Excluir()
        {
            if (PessoaSelecionada != null)
            {
                var resultado = MessageBox.Show(
                    $"Deseja realmente excluir {PessoaSelecionada.Nome}?",
                    "Confirmar Exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    _pessoaService.Excluir(PessoaSelecionada.Id);
                    CarregarPessoas();
                    PessoaSelecionada = null;
                }
            }
        }

        private void CarregarPedidosDaPessoa()
        {
            if (PessoaSelecionada != null)
            {
                var pedidos = _pedidoService.ObterPorCliente(PessoaSelecionada.Id);
                
                // Aplicar filtro
                if (FiltroPedidosDaPessoa == "Pagos")
                {
                    pedidos = pedidos.Where(p => p.Status == StatusPedido.Pago).ToList();
                }
                else if (FiltroPedidosDaPessoa == "Enviados")
                {
                    pedidos = pedidos.Where(p => p.Status == StatusPedido.Enviado).ToList();
                }
                else if (FiltroPedidosDaPessoa == "Recebidos")
                {
                    pedidos = pedidos.Where(p => p.Status == StatusPedido.Recebido).ToList();
                }
                else if (FiltroPedidosDaPessoa == "Pendentes")
                {
                    pedidos = pedidos.Where(p => p.Status == StatusPedido.Pendente).ToList();
                }
                // "Todos" - não aplica filtro
                
                PedidosDaPessoa = new ObservableCollection<Pedido>(pedidos);
            }
            else
            {
                PedidosDaPessoa = new ObservableCollection<Pedido>();
            }
        }

        private void IncluirPedido()
        {
            if (PessoaSelecionada != null)
            {
                var novoPedido = new Pedido
                {
                    PessoaId = PessoaSelecionada.Id,
                    NomeCliente = PessoaSelecionada.Nome,
                    Status = StatusPedido.Pendente,
                    FormaPagamento = FormaPagamento.Dinheiro
                };

                _pedidoService.Adicionar(novoPedido);
                CarregarPedidosDaPessoa();
                MessageBox.Show($"Pedido criado para {PessoaSelecionada.Nome}!\n\nAdicione produtos ao pedido na tela de Pedidos.", 
                    "Pedido Criado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MarcarPedidoComoPago()
        {
            if (PedidoDaPessoaSelecionado != null)
            {
                _pedidoService.AlterarStatus(PedidoDaPessoaSelecionado.Id, StatusPedido.Pago);
                CarregarPedidosDaPessoa();
                MessageBox.Show("Pedido marcado como Pago!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MarcarPedidoComoEnviado()
        {
            if (PedidoDaPessoaSelecionado != null)
            {
                _pedidoService.AlterarStatus(PedidoDaPessoaSelecionado.Id, StatusPedido.Enviado);
                CarregarPedidosDaPessoa();
                MessageBox.Show("Pedido marcado como Enviado!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MarcarPedidoComoRecebido()
        {
            if (PedidoDaPessoaSelecionado != null)
            {
                _pedidoService.AlterarStatus(PedidoDaPessoaSelecionado.Id, StatusPedido.Recebido);
                CarregarPedidosDaPessoa();
                MessageBox.Show("Pedido marcado como Recebido!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

