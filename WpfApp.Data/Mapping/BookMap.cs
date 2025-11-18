using WpfApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Data.Mapping
{
    public class BookMap : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable("LIVROS");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Titulo)
                .IsUnique();

            builder.Property(x => x.Titulo)
              .IsRequired()
              .HasMaxLength(60);

            builder.Property(x => x.Autor)
             .IsRequired()
             .HasMaxLength(30);

            builder.Property(x => x.Editora)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Autor)
            .IsRequired();

            builder.Property(x => x.Enabled)
              .HasDefaultValueSql("1");
        }
    }
}
