using System.Windows;
using System.Windows.Controls;
using WpfApp.Services;

namespace WpfApp.Views
{
    public partial class PessoaView : UserControl
    {
        public PessoaView()
        {
            InitializeComponent();
        }

        private void TxtCPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            int cursorPosition = textBox.SelectionStart;
            string textoSemFormatacao = ValidationHelper.RemoverFormatacao(textBox.Text);
            
            // Limita a 11 dígitos
            if (textoSemFormatacao.Length > 11)
                textoSemFormatacao = textoSemFormatacao.Substring(0, 11);

            string textoFormatado = ValidationHelper.FormatarCPF(textoSemFormatacao);
            
            if (textBox.Text != textoFormatado)
            {
                textBox.Text = textoFormatado;
                textBox.SelectionStart = System.Math.Min(cursorPosition + (textoFormatado.Length - textBox.Text.Length), textoFormatado.Length);
            }

            // Validação visual
            if (!string.IsNullOrWhiteSpace(textoSemFormatacao))
            {
                bool cpfValido = ValidationHelper.ValidarCPF(textoSemFormatacao);
                textBox.Background = cpfValido ? System.Windows.Media.Brushes.White : System.Windows.Media.Brushes.LightPink;
                textBox.ToolTip = cpfValido ? "CPF válido" : "CPF inválido";
            }
            else
            {
                textBox.Background = System.Windows.Media.Brushes.White;
                textBox.ToolTip = "Digite o CPF";
            }
        }

        private void TxtTelefone_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            int cursorPosition = textBox.SelectionStart;
            string textoSemFormatacao = ValidationHelper.RemoverFormatacao(textBox.Text);
            
            // Limita a 11 dígitos (celular)
            if (textoSemFormatacao.Length > 11)
                textoSemFormatacao = textoSemFormatacao.Substring(0, 11);

            string textoFormatado = ValidationHelper.FormatarTelefone(textoSemFormatacao);
            
            if (textBox.Text != textoFormatado)
            {
                textBox.Text = textoFormatado;
                textBox.SelectionStart = System.Math.Min(cursorPosition + 1, textoFormatado.Length);
            }
        }

        private void TxtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                bool emailValido = ValidationHelper.ValidarEmail(textBox.Text);
                textBox.Background = emailValido ? System.Windows.Media.Brushes.White : System.Windows.Media.Brushes.LightPink;
                textBox.ToolTip = emailValido ? "Email válido" : "Email inválido";
            }
            else
            {
                textBox.Background = System.Windows.Media.Brushes.White;
                textBox.ToolTip = "Digite o email";
            }
        }
    }
}

