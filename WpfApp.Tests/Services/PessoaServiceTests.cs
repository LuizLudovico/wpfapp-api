using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.Tests.Services
{
    [TestFixture]
    public class PessoaServiceTests
    {
        private PessoaService _service;
        private string _testDataPath;

        [SetUp]
        public void Setup()
        {
            // Criar caminho temporário para testes
            _testDataPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "WpfAppTests");
            if (!System.IO.Directory.Exists(_testDataPath))
            {
                System.IO.Directory.CreateDirectory(_testDataPath);
            }

            _service = new PessoaService();
            
            // Limpar dados antes de cada teste
            var todasPessoas = _service.ObterTodas();
            foreach (var pessoa in todasPessoas)
            {
                _service.Excluir(pessoa.Id);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Limpar dados após teste
            if (_service != null)
            {
                var todasPessoas = _service.ObterTodas();
                foreach (var pessoa in todasPessoas)
                {
                    _service.Excluir(pessoa.Id);
                }
            }
        }

        [Test]
        public void Adicionar_NovaPessoa_DeveIncrementarContador()
        {
            // Arrange
            var pessoa = new Pessoa
            {
                Nome = "João Silva",
                CPF = "123.456.789-09",
                Email = "joao@teste.com",
                Telefone = "(11)98765-4321",
                DataNascimento = new DateTime(1990, 1, 1),
                Endereco = "Rua Teste, 123"
            };

            var countAntes = _service.ObterTodas().Count;

            // Act
            _service.Adicionar(pessoa);
            var countDepois = _service.ObterTodas().Count;

            // Assert
            Assert.AreEqual(countAntes + 1, countDepois);
        }

        [Test]
        public void Adicionar_PessoaComId_DeveManterId()
        {
            // Arrange
            var idEsperado = Guid.NewGuid();
            var pessoa = new Pessoa
            {
                Id = idEsperado,
                Nome = "Maria Santos",
                CPF = "987.654.321-00",
                Email = "maria@teste.com"
            };

            // Act
            _service.Adicionar(pessoa);
            var pessoaRecuperada = _service.ObterPorId(idEsperado);

            // Assert
            Assert.IsNotNull(pessoaRecuperada);
            Assert.AreEqual(idEsperado, pessoaRecuperada.Id);
        }

        [Test]
        public void BuscarPorNome_NomeExistente_RetornaListaComPessoa()
        {
            // Arrange
            var pessoa1 = new Pessoa { Nome = "João Silva", CPF = "111.111.111-11", Email = "joao@teste.com" };
            var pessoa2 = new Pessoa { Nome = "Maria João", CPF = "222.222.222-22", Email = "maria@teste.com" };
            var pessoa3 = new Pessoa { Nome = "Carlos Santos", CPF = "333.333.333-33", Email = "carlos@teste.com" };

            _service.Adicionar(pessoa1);
            _service.Adicionar(pessoa2);
            _service.Adicionar(pessoa3);

            // Act
            var resultado = _service.BuscarPorNome("João");

            // Assert
            Assert.AreEqual(2, resultado.Count);
            Assert.IsTrue(resultado.Any(p => p.Nome == "João Silva"));
            Assert.IsTrue(resultado.Any(p => p.Nome == "Maria João"));
        }

        [Test]
        public void BuscarPorNome_NomeInexistente_RetornaListaVazia()
        {
            // Arrange
            var pessoa = new Pessoa { Nome = "João Silva", CPF = "111.111.111-11", Email = "joao@teste.com" };
            _service.Adicionar(pessoa);

            // Act
            var resultado = _service.BuscarPorNome("Pedro");

            // Assert
            Assert.AreEqual(0, resultado.Count);
        }

        [Test]
        public void BuscarPorNome_BuscaCaseInsensitive()
        {
            // Arrange
            var pessoa = new Pessoa { Nome = "João Silva", CPF = "111.111.111-11", Email = "joao@teste.com" };
            _service.Adicionar(pessoa);

            // Act
            var resultadoMinusculo = _service.BuscarPorNome("joão");
            var resultadoMaiusculo = _service.BuscarPorNome("JOÃO");

            // Assert
            Assert.AreEqual(1, resultadoMinusculo.Count);
            Assert.AreEqual(1, resultadoMaiusculo.Count);
        }

        [Test]
        public void Atualizar_PessoaExistente_DeveAlterarDados()
        {
            // Arrange
            var pessoa = new Pessoa
            {
                Nome = "João Silva",
                CPF = "123.456.789-09",
                Email = "joao@teste.com",
                Telefone = "(11)98765-4321"
            };

            _service.Adicionar(pessoa);
            var idPessoa = pessoa.Id;

            // Act
            pessoa.Nome = "João Silva Santos";
            pessoa.Email = "joao.santos@teste.com";
            _service.Atualizar(pessoa);

            var pessoaAtualizada = _service.ObterPorId(idPessoa);

            // Assert
            Assert.AreEqual("João Silva Santos", pessoaAtualizada.Nome);
            Assert.AreEqual("joao.santos@teste.com", pessoaAtualizada.Email);
        }

        [Test]
        public void Excluir_PessoaExistente_DeveRemoverDoSistema()
        {
            // Arrange
            var pessoa = new Pessoa
            {
                Nome = "João Silva",
                CPF = "123.456.789-09",
                Email = "joao@teste.com"
            };

            _service.Adicionar(pessoa);
            var idPessoa = pessoa.Id;
            var countAntes = _service.ObterTodas().Count;

            // Act
            _service.Excluir(idPessoa);
            var countDepois = _service.ObterTodas().Count;
            var pessoaExcluida = _service.ObterPorId(idPessoa);

            // Assert
            Assert.AreEqual(countAntes - 1, countDepois);
            Assert.IsNull(pessoaExcluida);
        }

        [Test]
        public void ObterPorId_IdInexistente_RetornaNull()
        {
            // Arrange
            var idInexistente = Guid.NewGuid();

            // Act
            var pessoa = _service.ObterPorId(idInexistente);

            // Assert
            Assert.IsNull(pessoa);
        }

        [Test]
        public void ObterTodas_RetornaListaOrdenadaPorNome()
        {
            // Arrange
            var pessoa1 = new Pessoa { Nome = "Carlos", CPF = "111.111.111-11", Email = "carlos@teste.com" };
            var pessoa2 = new Pessoa { Nome = "Ana", CPF = "222.222.222-22", Email = "ana@teste.com" };
            var pessoa3 = new Pessoa { Nome = "Bruno", CPF = "333.333.333-33", Email = "bruno@teste.com" };

            _service.Adicionar(pessoa1);
            _service.Adicionar(pessoa2);
            _service.Adicionar(pessoa3);

            // Act
            var todas = _service.ObterTodas();

            // Assert
            Assert.AreEqual("Ana", todas[0].Nome);
            Assert.AreEqual("Bruno", todas[1].Nome);
            Assert.AreEqual("Carlos", todas[2].Nome);
        }
    }
}

