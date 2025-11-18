using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class ProdutoService
    {
        private readonly IDataService<Produto> _dataService;

        public ProdutoService()
        {
            _dataService = new JsonDataService<Produto>("produtos.json");
        }

        public List<Produto> ObterTodos()
        {
            return _dataService.GetAll();
        }

        public Produto ObterPorId(Guid id)
        {
            return _dataService.GetById(id);
        }

        public List<Produto> BuscarPorNome(string nome)
        {
            return _dataService.GetAll()
                .Where(p => p.Nome.IndexOf(nome, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(p => p.Nome)
                .ToList();
        }

        public List<Produto> BuscarPorCodigo(string codigo)
        {
            return _dataService.GetAll()
                .Where(p => p.Codigo.IndexOf(codigo, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(p => p.Codigo)
                .ToList();
        }

        public List<Produto> BuscarPorCategoria(string categoria)
        {
            return _dataService.GetAll()
                .Where(p => p.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.Nome)
                .ToList();
        }

        public List<Produto> BuscarPorFaixaDeValor(decimal? valorInicial = null, decimal? valorFinal = null)
        {
            var query = _dataService.GetAll().AsQueryable();

            if (valorInicial.HasValue)
                query = query.Where(p => p.Preco >= valorInicial.Value);

            if (valorFinal.HasValue)
                query = query.Where(p => p.Preco <= valorFinal.Value);

            return query.OrderBy(p => p.Preco).ToList();
        }

        public List<Produto> ObterProdutosComEstoqueBaixo(int quantidadeMinima = 10)
        {
            return _dataService.GetAll()
                .Where(p => p.QuantidadeEstoque <= quantidadeMinima)
                .OrderBy(p => p.QuantidadeEstoque)
                .ToList();
        }

        public void Adicionar(Produto produto)
        {
            _dataService.Add(produto);
            _dataService.SaveChanges();
        }

        public void Atualizar(Produto produto)
        {
            _dataService.Update(produto);
            _dataService.SaveChanges();
        }

        public void Excluir(Guid id)
        {
            _dataService.Delete(id);
            _dataService.SaveChanges();
        }

        public void AtualizarEstoque(Guid produtoId, int quantidade)
        {
            var produto = ObterPorId(produtoId);
            if (produto != null)
            {
                produto.QuantidadeEstoque += quantidade;
                Atualizar(produto);
            }
        }
    }
}

