using NUnit.Framework;
using WpfApp.Services;

namespace WpfApp.Tests
{
    [TestFixture]
    public class MascaraHelperTests
    {
        [Test]
        public void FormatarData_DataCompleta_RetornaFormatada()
        {
            // Arrange
            string dataSemFormatacao = "03121985";

            // Act
            string dataFormatada = MascaraHelper.FormatarData(dataSemFormatacao);

            // Assert
            Assert.AreEqual("03/12/1985", dataFormatada);
        }

        [Test]
        public void FormatarData_DataParcialDia_RetornaFormatacaoParcial()
        {
            // Arrange
            string dataParcial = "03";

            // Act
            string dataFormatada = MascaraHelper.FormatarData(dataParcial);

            // Assert
            Assert.AreEqual("03", dataFormatada);
        }

        [Test]
        public void FormatarData_DataParcialDiaMes_RetornaComBarra()
        {
            // Arrange
            string dataParcial = "0312";

            // Act
            string dataFormatada = MascaraHelper.FormatarData(dataParcial);

            // Assert
            Assert.AreEqual("03/12", dataFormatada);
        }

        [Test]
        public void FormatarData_DataComBarras_MantémFormatacao()
        {
            // Arrange
            string dataComBarras = "03/12/1985";

            // Act
            string dataFormatada = MascaraHelper.FormatarData(dataComBarras);

            // Assert
            Assert.AreEqual("03/12/1985", dataFormatada);
        }

        [Test]
        public void FormatarData_ApenasNumeros_AdicionaBarrasAutomaticamente()
        {
            // Arrange
            string dataNumeros = "031219";

            // Act
            string dataFormatada = MascaraHelper.FormatarData(dataNumeros);

            // Assert
            Assert.AreEqual("03/12/19", dataFormatada);
        }

        [Test]
        public void FormatarData_DataVazia_RetornaVazio()
        {
            // Arrange
            string dataVazia = "";

            // Act
            string dataFormatada = MascaraHelper.FormatarData(dataVazia);

            // Assert
            Assert.AreEqual("", dataFormatada);
        }

        [Test]
        public void FormatarData_DataMaisQue8Digitos_LimitaA8()
        {
            // Arrange
            string dataMuito = "031219851234";

            // Act
            string dataFormatada = MascaraHelper.FormatarData(dataMuito);

            // Assert
            Assert.AreEqual("03/12/1985", dataFormatada);
        }
    }
}

