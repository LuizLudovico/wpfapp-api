using NUnit.Framework;
using System;
using WpfApp.Models;

namespace WpfApp.Tests.Models
{
    [TestFixture]
    public class ItemPedidoTests
    {
        [Test]
        public void Subtotal_DeveCalcularCorretamente()
        {
            // Arrange
            var item = new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto Teste",
                Quantidade = 5,
                PrecoUnitario = 25.50m
            };

            // Act
            var subtotal = item.Subtotal;

            // Assert
            Assert.AreEqual(127.50m, subtotal);
        }

        [Test]
        public void Subtotal_ComQuantidadeZero_RetornaZero()
        {
            // Arrange
            var item = new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto Teste",
                Quantidade = 0,
                PrecoUnitario = 100.00m
            };

            // Act
            var subtotal = item.Subtotal;

            // Assert
            Assert.AreEqual(0, subtotal);
        }

        [Test]
        public void Subtotal_ComPrecoZero_RetornaZero()
        {
            // Arrange
            var item = new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto Teste",
                Quantidade = 10,
                PrecoUnitario = 0
            };

            // Act
            var subtotal = item.Subtotal;

            // Assert
            Assert.AreEqual(0, subtotal);
        }

        [Test]
        public void Subtotal_ComValoresDecimais_CalculaCorretamente()
        {
            // Arrange
            var item = new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto Teste",
                Quantidade = 3,
                PrecoUnitario = 12.50m
            };

            // Act
            var subtotal = item.Subtotal;

            // Assert
            Assert.AreEqual(37.50m, subtotal);
        }

        [Test]
        public void Subtotal_AtualizaQuandoQuantidadeMuda()
        {
            // Arrange
            var item = new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto Teste",
                Quantidade = 2,
                PrecoUnitario = 50.00m
            };

            var subtotalInicial = item.Subtotal;

            // Act
            item.Quantidade = 5;
            var subtotalFinal = item.Subtotal;

            // Assert
            Assert.AreEqual(100.00m, subtotalInicial);
            Assert.AreEqual(250.00m, subtotalFinal);
        }

        [Test]
        public void Subtotal_AtualizaQuandoPrecoMuda()
        {
            // Arrange
            var item = new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto Teste",
                Quantidade = 4,
                PrecoUnitario = 10.00m
            };

            var subtotalInicial = item.Subtotal;

            // Act
            item.PrecoUnitario = 20.00m;
            var subtotalFinal = item.Subtotal;

            // Assert
            Assert.AreEqual(40.00m, subtotalInicial);
            Assert.AreEqual(80.00m, subtotalFinal);
        }

        [Test]
        public void Subtotal_ComValoresAltos_CalculaCorretamente()
        {
            // Arrange
            var item = new ItemPedido
            {
                ProdutoId = Guid.NewGuid(),
                NomeProduto = "Produto Caro",
                Quantidade = 10,
                PrecoUnitario = 1999.99m
            };

            // Act
            var subtotal = item.Subtotal;

            // Assert
            Assert.AreEqual(19999.90m, subtotal);
        }
    }
}

