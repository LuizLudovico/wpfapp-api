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

            // Salva a posição do cursor
            int cursorPosition = textBox.SelectionStart;
            string textoAnterior = textBox.Text;
            
            // Remove formatação
            string textoSemFormatacao = ValidationHelper.RemoverFormatacao(textBox.Text);
            
            // Limita a 11 dígitos
            if (textoSemFormatacao.Length > 11)
                textoSemFormatacao = textoSemFormatacao.Substring(0, 11);

            // Aplica formatação
            string textoFormatado = ValidationHelper.FormatarCPF(textoSemFormatacao);
            
            // Só atualiza se mudou
            if (textBox.Text != textoFormatado)
            {
                // Remove o handler temporariamente para evitar loop
                textBox.TextChanged -= TxtCPF_TextChanged;
                
                textBox.Text = textoFormatado;
                
                // Ajusta cursor: se estava no final, vai pro final, senão mantém posição relativa
                if (cursorPosition >= textoAnterior.Length)
                {
                    textBox.SelectionStart = textoFormatado.Length;
                }
                else
                {
                    // Calcula nova posição baseada no número de dígitos antes do cursor
                    int digitosAntes = ValidationHelper.RemoverFormatacao(textoAnterior.Substring(0, cursorPosition)).Length;
                    int novaPosicao = 0;
                    int digitosContados = 0;
                    
                    for (int i = 0; i < textoFormatado.Length && digitosContados < digitosAntes; i++)
                    {
                        if (char.IsDigit(textoFormatado[i]))
                            digitosContados++;
                        novaPosicao = i + 1;
                    }
                    
                    textBox.SelectionStart = System.Math.Min(novaPosicao, textoFormatado.Length);
                }
                
                // Reativa o handler
                textBox.TextChanged += TxtCPF_TextChanged;
            }

            // Validação visual
            if (textoSemFormatacao.Length > 0)
            {
                bool cpfValido = textoSemFormatacao.Length == 11 && ValidationHelper.ValidarCPF(textoSemFormatacao);
                textBox.Background = cpfValido ? System.Windows.Media.Brushes.White : 
                                    textoSemFormatacao.Length == 11 ? System.Windows.Media.Brushes.LightPink : 
                                    System.Windows.Media.Brushes.White;
                textBox.ToolTip = textoSemFormatacao.Length == 11 ? 
                                 (cpfValido ? "CPF válido" : "CPF inválido") : 
                                 "Digite o CPF completo";
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

