using System;

namespace WpfApp.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string Categoria { get; set; }
        public string CodigoBarras { get; set; }
        public DateTime DataCadastro { get; set; }

        public Produto()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }
    }
}

