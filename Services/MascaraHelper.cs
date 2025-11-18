using System;

namespace WpfApp.Services
{
    public static class MascaraHelper
    {
        /// <summary>
        /// Aplica máscara de Data: DD/MM/AAAA
        /// </summary>
        public static string FormatarData(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            // Remove tudo que não é número
            texto = new string(Array.FindAll(texto.ToCharArray(), c => char.IsDigit(c)));

            if (texto.Length == 0)
                return string.Empty;

            // Limita a 8 dígitos (DDMMAAAA)
            if (texto.Length > 8)
                texto = texto.Substring(0, 8);

            // Aplica a máscara DD/MM/AAAA
            if (texto.Length <= 2)
                return texto;
            if (texto.Length <= 4)
                return texto.Insert(2, "/");
            
            return texto.Insert(2, "/").Insert(5, "/");
        }
    }
}

