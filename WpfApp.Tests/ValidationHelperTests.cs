using NUnit.Framework;
using WpfApp.Services;

namespace WpfApp.Tests
{
    [TestFixture]
    public class ValidationHelperTests
    {
        #region CPF Validation Tests

        [Test]
        public void ValidarCPF_CPFValido_RetornaTrue()
        {
            // Arrange
            string cpfValido = "123.456.789-09";

            // Act
            bool resultado = ValidationHelper.ValidarCPF(cpfValido);

            // Assert
            Assert.IsTrue(resultado);
        }

        [Test]
        [TestCase("111.111.111-11")]
        [TestCase("000.000.000-00")]
        [TestCase("222.222.222-22")]
        [TestCase("333.333.333-33")]
        public void ValidarCPF_CPFComDigitosRepetidos_RetornaFalse(string cpf)
        {
            // Act
            bool resultado = ValidationHelper.ValidarCPF(cpf);

            // Assert
            Assert.IsFalse(resultado);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void ValidarCPF_CPFVazioOuNulo_RetornaFalse(string cpf)
        {
            // Act
            bool resultado = ValidationHelper.ValidarCPF(cpf);

            // Assert
            Assert.IsFalse(resultado);
        }

        [Test]
        [TestCase("12345678")]
        [TestCase("123.456.789")]
        public void ValidarCPF_CPFComMenosDe11Digitos_RetornaFalse(string cpf)
        {
            // Act
            bool resultado = ValidationHelper.ValidarCPF(cpf);

            // Assert
            Assert.IsFalse(resultado);
        }

        [Test]
        public void ValidarCPF_CPFComDigitoVerificadorInvalido_RetornaFalse()
        {
            // Arrange
            string cpfInvalido = "123.456.789-00"; // Dígitos verificadores incorretos

            // Act
            bool resultado = ValidationHelper.ValidarCPF(cpfInvalido);

            // Assert
            Assert.IsFalse(resultado);
        }

        [Test]
        public void ValidarCPF_CPFSemFormatacao_RetornaTrue()
        {
            // Arrange
            string cpfSemFormatacao = "12345678909";

            // Act
            bool resultado = ValidationHelper.ValidarCPF(cpfSemFormatacao);

            // Assert
            Assert.IsTrue(resultado);
        }

        #endregion

        #region Email Validation Tests

        [Test]
        [TestCase("teste@exemplo.com")]
        [TestCase("usuario.nome@dominio.com.br")]
        [TestCase("email+tag@teste.co")]
        [TestCase("nome_sobrenome@empresa.com")]
        public void ValidarEmail_EmailValido_RetornaTrue(string email)
        {
            // Act
            bool resultado = ValidationHelper.ValidarEmail(email);

            // Assert
            Assert.IsTrue(resultado);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void ValidarEmail_EmailVazioOuNulo_RetornaFalse(string email)
        {
            // Act
            bool resultado = ValidationHelper.ValidarEmail(email);

            // Assert
            Assert.IsFalse(resultado);
        }

        [Test]
        [TestCase("emailsemarroba.com")]
        [TestCase("@semlocal.com")]
        [TestCase("email@")]
        [TestCase("email @dominio.com")]
        [TestCase("email@dominio")]
        public void ValidarEmail_EmailInvalido_RetornaFalse(string email)
        {
            // Act
            bool resultado = ValidationHelper.ValidarEmail(email);

            // Assert
            Assert.IsFalse(resultado);
        }

        #endregion

        #region CPF Formatting Tests

        [Test]
        public void FormatarCPF_CPFSemFormatacao_RetornaCPFFormatado()
        {
            // Arrange
            string cpfSemFormatacao = "12345678909";

            // Act
            string cpfFormatado = ValidationHelper.FormatarCPF(cpfSemFormatacao);

            // Assert
            Assert.AreEqual("123.456.789-09", cpfFormatado);
        }

        [Test]
        public void FormatarCPF_CPFParcial_RetornaFormatacaoParcial()
        {
            // Arrange
            string cpfParcial = "12345";

            // Act
            string cpfFormatado = ValidationHelper.FormatarCPF(cpfParcial);

            // Assert
            Assert.AreEqual("123.45", cpfFormatado);
        }

        [Test]
        public void FormatarCPF_CPFVazio_RetornaVazio()
        {
            // Arrange
            string cpfVazio = "";

            // Act
            string cpfFormatado = ValidationHelper.FormatarCPF(cpfVazio);

            // Assert
            Assert.AreEqual("", cpfFormatado);
        }

        #endregion

        #region Telefone Formatting Tests

        [Test]
        public void FormatarTelefone_Celular_RetornaFormatado()
        {
            // Arrange
            string telefoneSemFormatacao = "18997316821";

            // Act
            string telefoneFormatado = ValidationHelper.FormatarTelefone(telefoneSemFormatacao);

            // Assert
            Assert.AreEqual("(18)99731-6821", telefoneFormatado);
        }

        [Test]
        public void FormatarTelefone_Fixo_RetornaFormatado()
        {
            // Arrange
            string telefoneSemFormatacao = "1833412500";

            // Act
            string telefoneFormatado = ValidationHelper.FormatarTelefone(telefoneSemFormatacao);

            // Assert
            Assert.AreEqual("(18)3341-2500", telefoneFormatado);
        }

        [Test]
        public void FormatarTelefone_TelefoneParcial_RetornaFormatacaoParcial()
        {
            // Arrange
            string telefoneParcial = "1899";

            // Act
            string telefoneFormatado = ValidationHelper.FormatarTelefone(telefoneParcial);

            // Assert
            Assert.AreEqual("(18)99", telefoneFormatado);
        }

        #endregion

        #region RemoverFormatacao Tests

        [Test]
        public void RemoverFormatacao_CPFFormatado_RetornaApenasNumeros()
        {
            // Arrange
            string cpfFormatado = "123.456.789-09";

            // Act
            string apenasNumeros = ValidationHelper.RemoverFormatacao(cpfFormatado);

            // Assert
            Assert.AreEqual("12345678909", apenasNumeros);
        }

        [Test]
        public void RemoverFormatacao_TelefoneFormatado_RetornaApenasNumeros()
        {
            // Arrange
            string telefoneFormatado = "(18)99731-6821";

            // Act
            string apenasNumeros = ValidationHelper.RemoverFormatacao(telefoneFormatado);

            // Assert
            Assert.AreEqual("18997316821", apenasNumeros);
        }

        [Test]
        public void RemoverFormatacao_TextoSemNumeros_RetornaVazio()
        {
            // Arrange
            string textoSemNumeros = "ABC-DEF.GHI";

            // Act
            string resultado = ValidationHelper.RemoverFormatacao(textoSemNumeros);

            // Assert
            Assert.AreEqual("", resultado);
        }

        #endregion
    }
}

