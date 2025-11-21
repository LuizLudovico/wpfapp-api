using System.Collections.ObjectModel;
using System.Windows;
using WpfApp.Models;

namespace WpfApp.Views
{
    public partial class SelecionarPessoaWindow : Window
    {
        public ObservableCollection<Pessoa> Pessoas { get; set; }
        public Pessoa PessoaSelecionada { get; set; }
        public bool Confirmado { get; private set; }

        public SelecionarPessoaWindow(ObservableCollection<Pessoa> pessoas)
        {
            InitializeComponent();
            Pessoas = pessoas;
            DataContext = this;
            Confirmado = false;
        }

        private void Selecionar_Click(object sender, RoutedEventArgs e)
        {
            if (PessoaSelecionada == null)
            {
                MessageBox.Show("Selecione um cliente da lista.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Confirmado = true;
            Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Confirmado = false;
            Close();
        }
    }
}

