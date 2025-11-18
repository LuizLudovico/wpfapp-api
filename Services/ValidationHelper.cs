using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace WpfApp.Services
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Valida CPF brasileiro
        /// </summary>
        public static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove formatação
            cpf = RemoverFormatacao(cpf);

            // CPF deve ter 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.All(c => c == cpf[0]))
                return false;

            // Validação do primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
                return false;

            // Validação do segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cpf[10].ToString()) == digitoVerificador2;
        }

        /// <summary>
        /// Valida Email
        /// </summary>
        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Aplica máscara de CPF: 000.000.000-00
        /// </summary>
        public static string FormatarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return string.Empty;

            cpf = RemoverFormatacao(cpf);

            if (cpf.Length <= 3)
                return cpf;
            if (cpf.Length <= 6)
                return cpf.Insert(3, ".");
            if (cpf.Length <= 9)
                return cpf.Insert(3, ".").Insert(7, ".");
            
            return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
        }

        /// <summary>
        /// Aplica máscara de Telefone: (00)00000-0000 ou (00)0000-0000
        /// </summary>
        public static string FormatarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return string.Empty;

            telefone = RemoverFormatacao(telefone);

            if (telefone.Length <= 2)
                return telefone;
            if (telefone.Length <= 6)
                return $"({telefone.Substring(0, 2)}){telefone.Substring(2)}";
            
            // Celular (11 dígitos): (00)00000-0000
            if (telefone.Length == 11)
            {
                return $"({telefone.Substring(0, 2)}){telefone.Substring(2, 5)}-{telefone.Substring(7)}";
            }
            
            // Fixo (10 dígitos): (00)0000-0000
            if (telefone.Length == 10)
            {
                return $"({telefone.Substring(0, 2)}){telefone.Substring(2, 4)}-{telefone.Substring(6)}";
            }

            // Em digitação
            if (telefone.Length > 6)
            {
                int tamanhoMeio = telefone.Length > 10 ? 5 : 4;
                return $"({telefone.Substring(0, 2)}){telefone.Substring(2, tamanhoMeio)}-{telefone.Substring(2 + tamanhoMeio)}";
            }

            return telefone;
        }

        /// <summary>
        /// Remove formatação de CPF, Telefone, etc
        /// </summary>
        public static string RemoverFormatacao(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return string.Empty;

            return Regex.Replace(valor, @"[^\d]", "");
        }
    }
}

