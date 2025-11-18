namespace WpfApp.Domain.Entities
{
    public class BookEntity : BaseEntity
    {
        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string Editora { get; set; }

        public int NumPaginas { get; set; }

    }
}
