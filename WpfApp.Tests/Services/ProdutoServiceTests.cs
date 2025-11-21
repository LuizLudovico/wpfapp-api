using NUnit.Framework;
using System;
using System.Linq;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.Tests.Services
{
    [TestFixture]
    public class ProdutoServiceTests
    {
        private ProdutoService _service;

        [SetUp]
        public void Setup()
        {
            _service = new ProdutoService();

            // Limpar dados antes de cada teste
            var todosProdutos = _service.ObterTodos();
            foreach (var produto in todosProdutos)
            {
                _service.Excluir(produto.Id);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (_service != null)
            {
                var todosProdutos = _service.ObterTodos();
                foreach (var produto in todosProdutos)
                {
                    _service.Excluir(produto.Id);
                }
            }
        }

        [Test]
        public void Adicionar_NovoProduto_DeveIncrementarContador()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Notebook",
                Codigo = "NB-001",
                Preco = 2500.00m,
                QuantidadeEstoque = 10
            };

            var countAntes = _service.ObterTodos().Count;

            // Act
            _service.Adicionar(produto);
            var countDepois = _service.ObterTodos().Count;

            // Assert
            Assert.AreEqual(countAntes + 1, countDepois);
        }

        [Test]
        public void BuscarPorCodigo_CodigoExistente_RetornaProduto()
        {
            // Arrange
            var produto1 = new Produto { Nome = "Notebook", Codigo = "NB-001", Preco = 2500m, QuantidadeEstoque = 10 };
            var produto2 = new Produto { Nome = "Mouse", Codigo = "MS-001", Preco = 50m, QuantidadeEstoque = 50 };

            _service.Adicionar(produto1);
            _service.Adicionar(produto2);

            // Act
            var resultado = _service.BuscarPorCodigo("NB-001");

            // Assert
            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual("Notebook", resultado[0].Nome);
        }

        [Test]
        public void BuscarPorCodigo_CodigoParcial_RetornaProdutos()
        {
            // Arrange
            var produto1 = new Produto { Nome = "Notebook HP", Codigo = "NB-001", Preco = 2500m, QuantidadeEstoque = 10 };
            var produto2 = new Produto { Nome = "Notebook Dell", Codigo = "NB-002", Preco = 3000m, QuantidadeEstoque = 5 };
            var produto3 = new Produto { Nome = "Mouse", Codigo = "MS-001", Preco = 50m, QuantidadeEstoque = 50 };

            _service.Adicionar(produto1);
            _service.Adicionar(produto2);
            _service.Adicionar(produto3);

            // Act
            var resultado = _service.BuscarPorCodigo("NB");

            // Assert
            Assert.AreEqual(2, resultado.Count);
            Assert.IsTrue(resultado.All(p => p.Codigo.StartsWith("NB")));
        }

        [Test]
        public void BuscarPorFaixaDeValor_ComValoresMinMax_RetornaProdutosDentroDaFaixa()
        {
            // Arrange
            var produto1 = new Produto { Nome = "Produto A", Codigo = "A", Preco = 10m, QuantidadeEstoque = 10 };
            var produto2 = new Produto { Nome = "Produto B", Codigo = "B", Preco = 50m, QuantidadeEstoque = 10 };
            var produto3 = new Produto { Nome = "Produto C", Codigo = "C", Preco = 100m, QuantidadeEstoque = 10 };
            var produto4 = new Produto { Nome = "Produto D", Codigo = "D", Preco = 150m, QuantidadeEstoque = 10 };

            _service.Adicionar(produto1);
            _service.Adicionar(produto2);
            _service.Adicionar(produto3);
            _service.Adicionar(produto4);

            // Act
            var resultado = _service.BuscarPorFaixaDeValor(40m, 120m);

            // Assert
            Assert.AreEqual(2, resultado.Count);
            Assert.IsTrue(resultado.Any(p => p.Nome == "Produto B"));
            Assert.IsTrue(resultado.Any(p => p.Nome == "Produto C"));
        }

        [Test]
        public void BuscarPorFaixaDeValor_ApenasValorMinimo_RetornaProdutosAcima()
        {
            // Arrange
            var produto1 = new Produto { Nome = "Produto A", Codigo = "A", Preco = 10m, QuantidadeEstoque = 10 };
            var produto2 = new Produto { Nome = "Produto B", Codigo = "B", Preco = 50m, QuantidadeEstoque = 10 };
            var produto3 = new Produto { Nome = "Produto C", Codigo = "C", Preco = 100m, QuantidadeEstoque = 10 };

            _service.Adicionar(produto1);
            _service.Adicionar(produto2);
            _service.Adicionar(produto3);

            // Act
            var resultado = _service.BuscarPorFaixaDeValor(50m, null);

            // Assert
            Assert.AreEqual(2, resultado.Count);
            Assert.IsTrue(resultado.All(p => p.Preco >= 50m));
        }

        [Test]
        public void BuscarPorFaixaDeValor_ApenasValorMaximo_RetornaProdutosAbaixo()
        {
            // Arrange
            var produto1 = new Produto { Nome = "Produto A", Codigo = "A", Preco = 10m, QuantidadeEstoque = 10 };
            var produto2 = new Produto { Nome = "Produto B", Codigo = "B", Preco = 50m, QuantidadeEstoque = 10 };
            var produto3 = new Produto { Nome = "Produto C", Codigo = "C", Preco = 100m, QuantidadeEstoque = 10 };

            _service.Adicionar(produto1);
            _service.Adicionar(produto2);
            _service.Adicionar(produto3);

            // Act
            var resultado = _service.BuscarPorFaixaDeValor(null, 50m);

            // Assert
            Assert.AreEqual(2, resultado.Count);
            Assert.IsTrue(resultado.All(p => p.Preco <= 50m));
        }

        [Test]
        public void ObterProdutosComEstoqueBaixo_RetornaProdutosComEstoqueMenorOuIgual()
        {
            // Arrange
            var produto1 = new Produto { Nome = "Produto A", Codigo = "A", Preco = 10m, QuantidadeEstoque = 5 };
            var produto2 = new Produto { Nome = "Produto B", Codigo = "B", Preco = 50m, QuantidadeEstoque = 10 };
            var produto3 = new Produto { Nome = "Produto C", Codigo = "C", Preco = 100m, QuantidadeEstoque = 15 };

            _service.Adicionar(produto1);
            _service.Adicionar(produto2);
            _service.Adicionar(produto3);

            // Act
            var resultado = _service.ObterProdutosComEstoqueBaixo(10);

            // Assert
            Assert.AreEqual(2, resultado.Count);
            Assert.IsTrue(resultado.Any(p => p.Nome == "Produto A"));
            Assert.IsTrue(resultado.Any(p => p.Nome == "Produto B"));
        }

        [Test]
        public void AtualizarEstoque_Aumentar_DeveIncrementarEstoque()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Mouse",
                Codigo = "MS-001",
                Preco = 50m,
                QuantidadeEstoque = 20
            };

            _service.Adicionar(produto);
            var idProduto = produto.Id;

            // Act
            _service.AtualizarEstoque(idProduto, 10);
            var produtoAtualizado = _service.ObterPorId(idProduto);

            // Assert
            Assert.AreEqual(30, produtoAtualizado.QuantidadeEstoque);
        }

        [Test]
        public void AtualizarEstoque_Diminuir_DeveDecrementarEstoque()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Mouse",
                Codigo = "MS-001",
                Preco = 50m,
                QuantidadeEstoque = 20
            };

            _service.Adicionar(produto);
            var idProduto = produto.Id;

            // Act
            _service.AtualizarEstoque(idProduto, -5);
            var produtoAtualizado = _service.ObterPorId(idProduto);

            // Assert
            Assert.AreEqual(15, produtoAtualizado.QuantidadeEstoque);
        }

        [Test]
        public void BuscarPorNome_CaseInsensitive_RetornaProdutos()
        {
            // Arrange
            var produto = new Produto { Nome = "Notebook", Codigo = "NB-001", Preco = 2500m, QuantidadeEstoque = 10 };
            _service.Adicionar(produto);

            // Act
            var resultadoMinusculo = _service.BuscarPorNome("notebook");
            var resultadoMaiusculo = _service.BuscarPorNome("NOTEBOOK");

            // Assert
            Assert.AreEqual(1, resultadoMinusculo.Count);
            Assert.AreEqual(1, resultadoMaiusculo.Count);
        }

        [Test]
        public void Atualizar_ProdutoExistente_DeveAlterarDados()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Mouse",
                Codigo = "MS-001",
                Preco = 50m,
                QuantidadeEstoque = 20
            };

            _service.Adicionar(produto);
            var idProduto = produto.Id;

            // Act
            produto.Preco = 60m;
            produto.QuantidadeEstoque = 25;
            _service.Atualizar(produto);

            var produtoAtualizado = _service.ObterPorId(idProduto);

            // Assert
            Assert.AreEqual(60m, produtoAtualizado.Preco);
            Assert.AreEqual(25, produtoAtualizado.QuantidadeEstoque);
        }

        [Test]
        public void Excluir_ProdutoExistente_DeveRemoverDoSistema()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Mouse",
                Codigo = "MS-001",
                Preco = 50m,
                QuantidadeEstoque = 20
            };

            _service.Adicionar(produto);
            var idProduto = produto.Id;
            var countAntes = _service.ObterTodos().Count;

            // Act
            _service.Excluir(idProduto);
            var countDepois = _service.ObterTodos().Count;
            var produtoExcluido = _service.ObterPorId(idProduto);

            // Assert
            Assert.AreEqual(countAntes - 1, countDepois);
            Assert.IsNull(produtoExcluido);
        }
    }
}

