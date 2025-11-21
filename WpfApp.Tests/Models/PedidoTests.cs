using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using WpfApp.Models;

namespace WpfApp.Tests.Models
{
    [TestFixture]
    public class PedidoTests
    {
        [Test]
        public void Construtor_DeveCriarPedidoComValoresPadrao()
        {
            // Act
            var pedido = new Pedido();

            // Assert
            Assert.IsNotNull(pedido.Id);
            Assert.AreNotEqual(Guid.Empty, pedido.Id);
            Assert.IsNotNull(pedido.Itens);
            Assert.AreEqual(0, pedido.Itens.Count);
            Assert.AreEqual(StatusPedido.Pendente, pedido.Status);
            Assert.AreEqual(FormaPagamento.Dinheiro, pedido.FormaPagamento);
            Assert.AreEqual(0, pedido.ValorTotal);
        }

        [Test]
        public void CalcularTotal_SemItens_RetornaZero()
        {
            // Arrange
            var pedido = new Pedido();

            // Act
            pedido.CalcularTotal();

            // Assert
            Assert.AreEqual(0, pedido.ValorTotal);
        }

        [Test]
        public void CalcularTotal_ComUmItem_RetornaSubtotalDoItem()
        {
            // Arrange
            var pedido = new Pedido();
            pedido.Itens.Add(new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto A",
                Quantidade = 2,
                PrecoUnitario = 50.00m
            });

            // Act
            pedido.CalcularTotal();

            // Assert
            Assert.AreEqual(100.00m, pedido.ValorTotal);
        }

        [Test]
        public void CalcularTotal_ComMultiplosItens_RetornaSomaDosSubtotais()
        {
            // Arrange
            var pedido = new Pedido();
            pedido.Itens.Add(new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto A",
                Quantidade = 2,
                PrecoUnitario = 50.00m
            });
            pedido.Itens.Add(new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto B",
                Quantidade = 3,
                PrecoUnitario = 30.00m
            });
            pedido.Itens.Add(new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto C",
                Quantidade = 1,
                PrecoUnitario = 150.00m
            });

            // Act
            pedido.CalcularTotal();

            // Assert
            // 2*50 + 3*30 + 1*150 = 100 + 90 + 150 = 340
            Assert.AreEqual(340.00m, pedido.ValorTotal);
        }

        [Test]
        public void CalcularTotal_ComItensDecimais_CalculaCorretamente()
        {
            // Arrange
            var pedido = new Pedido();
            pedido.Itens.Add(new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto A",
                Quantidade = 5,
                PrecoUnitario = 12.50m
            });

            // Act
            pedido.CalcularTotal();

            // Assert
            Assert.AreEqual(62.50m, pedido.ValorTotal);
        }

        [Test]
        public void Itens_DeveSerObservableCollection()
        {
            // Arrange
            var pedido = new Pedido();

            // Assert
            Assert.IsInstanceOf<ObservableCollection<ItemPedido>>(pedido.Itens);
        }

        [Test]
        public void DataVenda_DeveSerInicializadaComDataAtual()
        {
            // Arrange
            var dataAntes = DateTime.Now;

            // Act
            var pedido = new Pedido();

            // Assert
            var dataDepois = DateTime.Now;
            Assert.IsTrue(pedido.DataVenda >= dataAntes);
            Assert.IsTrue(pedido.DataVenda <= dataDepois);
        }
    }
}

