using System;
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

            // Salva a posição do cursor
            int cursorPosition = textBox.SelectionStart;
            string textoAnterior = textBox.Text;
            
            // Remove formatação
            string textoSemFormatacao = ValidationHelper.RemoverFormatacao(textBox.Text);
            
            // Limita a 11 dígitos (celular)
            if (textoSemFormatacao.Length > 11)
                textoSemFormatacao = textoSemFormatacao.Substring(0, 11);

            // Aplica formatação
            string textoFormatado = ValidationHelper.FormatarTelefone(textoSemFormatacao);
            
            // Só atualiza se mudou
            if (textBox.Text != textoFormatado)
            {
                // Remove o handler temporariamente para evitar loop
                textBox.TextChanged -= TxtTelefone_TextChanged;
                
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
                textBox.TextChanged += TxtTelefone_TextChanged;
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

        private void TxtDataNascimento_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            // Salva a posição do cursor
            int cursorPosition = textBox.SelectionStart;
            string textoAnterior = textBox.Text;

            // Aplica máscara
            string textoFormatado = MascaraHelper.FormatarData(textBox.Text);

            // Só atualiza se mudou
            if (textBox.Text != textoFormatado)
            {
                // Remove o handler temporariamente para evitar loop
                textBox.TextChanged -= TxtDataNascimento_TextChanged;

                textBox.Text = textoFormatado;

                // Ajusta cursor
                if (cursorPosition >= textoAnterior.Length)
                {
                    textBox.SelectionStart = textoFormatado.Length;
                }
                else
                {
                    // Remove formatação para contar dígitos
                    string apenasDigitos = new string(Array.FindAll(textoAnterior.Substring(0, cursorPosition).ToCharArray(), c => char.IsDigit(c)));
                    int digitosAntes = apenasDigitos.Length;
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
                textBox.TextChanged += TxtDataNascimento_TextChanged;
            }
        }

        private void TxtDataNascimento_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                System.DateTime data;
                bool dataValida = DateTime.TryParseExact(textBox.Text, "dd/MM/yyyy", 
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, out data);

                if (dataValida)
                {
                    // Verifica se a data não é futura
                    if (data > DateTime.Now)
                    {
                        textBox.Background = System.Windows.Media.Brushes.LightPink;
                        textBox.ToolTip = "Data de nascimento não pode ser futura";
                        return;
                    }

                    // Verifica se a pessoa tem pelo menos 18 anos
                    int idade = DateTime.Now.Year - data.Year;
                    if (DateTime.Now < data.AddYears(idade)) idade--;

                    if (idade < 18)
                    {
                        textBox.Background = System.Windows.Media.Brushes.LightYellow;
                        textBox.ToolTip = $"Pessoa tem {idade} anos (menor de idade)";
                    }
                    else
                    {
                        textBox.Background = System.Windows.Media.Brushes.White;
                        textBox.ToolTip = $"Data válida - {idade} anos";
                    }
                }
                else
                {
                    textBox.Background = System.Windows.Media.Brushes.LightPink;
                    textBox.ToolTip = "Data inválida. Use o formato: DD/MM/AAAA (ex: 01/01/1999)";
                }
            }
            else
            {
                textBox.Background = System.Windows.Media.Brushes.White;
                textBox.ToolTip = "Digite a data de nascimento";
            }
        }
    }
}

