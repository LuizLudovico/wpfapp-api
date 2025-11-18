using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public Guid PessoaId { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataPedido { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public decimal ValorTotal { get; set; }
        public StatusPedido Status { get; set; }
        public string Observacoes { get; set; }

        public Pedido()
        {
            Id = Guid.NewGuid();
            DataPedido = DateTime.Now;
            Itens = new List<ItemPedido>();
            Status = StatusPedido.Pendente;
        }

        public void CalcularTotal()
        {
            ValorTotal = Itens?.Sum(i => i.Subtotal) ?? 0;
        }
    }

    public class ItemPedido
    {
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }

    public enum StatusPedido
    {
        Pendente,
        EmAndamento,
        Concluido,
        Cancelado
    }
}

