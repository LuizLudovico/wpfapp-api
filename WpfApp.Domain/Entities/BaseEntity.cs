using System.ComponentModel.DataAnnotations;

namespace WpfApp.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? Enabled { get; set; }

    }
}
