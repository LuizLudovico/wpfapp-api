using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class PedidoService
    {
        private readonly IDataService<Pedido> _dataService;
        private readonly ProdutoService _produtoService;

        public PedidoService()
        {
            _dataService = new JsonDataService<Pedido>("pedidos.json");
            _produtoService = new ProdutoService();
        }

        public List<Pedido> ObterTodos()
        {
            return _dataService.GetAll();
        }

        public Pedido ObterPorId(Guid id)
        {
            return _dataService.GetById(id);
        }

        public List<Pedido> ObterPorCliente(Guid pessoaId)
        {
            return _dataService.GetAll()
                .Where(p => p.PessoaId == pessoaId)
                .OrderByDescending(p => p.DataVenda)
                .ToList();
        }

        public List<Pedido> ObterPorStatus(StatusPedido status)
        {
            return _dataService.GetAll()
                .Where(p => p.Status == status)
                .OrderByDescending(p => p.DataVenda)
                .ToList();
        }

        public List<Pedido> ObterPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return _dataService.GetAll()
                .Where(p => p.DataVenda >= dataInicio && p.DataVenda <= dataFim)
                .OrderByDescending(p => p.DataVenda)
                .ToList();
        }

        public decimal ObterValorTotalVendas()
        {
            return _dataService.GetAll()
                .Where(p => p.Status == StatusPedido.Recebido)
                .Sum(p => p.ValorTotal);
        }

        public void Adicionar(Pedido pedido)
        {
            pedido.CalcularTotal();
            _dataService.Add(pedido);
            _dataService.SaveChanges();

            // Atualiza estoque dos produtos
            foreach (var item in pedido.Itens)
            {
                _produtoService.AtualizarEstoque(item.ProdutoId, -item.Quantidade);
            }
        }

        public void Atualizar(Pedido pedido)
        {
            pedido.CalcularTotal();
            _dataService.Update(pedido);
            _dataService.SaveChanges();
        }

        public void Excluir(Guid id)
        {
            var pedido = ObterPorId(id);
            if (pedido != null && pedido.Status == StatusPedido.Pendente)
            {
                // Devolve produtos ao estoque
                foreach (var item in pedido.Itens)
                {
                    _produtoService.AtualizarEstoque(item.ProdutoId, item.Quantidade);
                }

                _dataService.Delete(id);
                _dataService.SaveChanges();
            }
        }

        public void AlterarStatus(Guid id, StatusPedido novoStatus)
        {
            var pedido = ObterPorId(id);
            if (pedido != null)
            {
                pedido.Status = novoStatus;
                Atualizar(pedido);
            }
        }
    }
}

