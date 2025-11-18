using System;
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
        private string _filtroNome;

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
        public ICommand IncluirPedidoCommand { get; }

        public PessoaViewModel()
        {
            _pessoaService = new PessoaService();
            _pedidoService = new PedidoService();
            
            AdicionarCommand = new RelayCommand(param => Adicionar());
            EditarCommand = new RelayCommand(param => Editar(), param => PessoaSelecionada != null);
            ExcluirCommand = new RelayCommand(param => Excluir(), param => PessoaSelecionada != null);
            IncluirPedidoCommand = new RelayCommand(param => IncluirPedido(), param => PessoaSelecionada != null);

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
            if (string.IsNullOrWhiteSpace(FiltroNome))
            {
                CarregarPessoas();
            }
            else
            {
                var pessoasFiltradas = _pessoaService.BuscarPorNome(FiltroNome);
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
    }
}

