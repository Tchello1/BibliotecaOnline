using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BibliotecaOnline.Models.Mappings
{
    public class LivrosMapping : EntityTypeConfiguration<Livro>
    {
        public LivrosMapping()
        {
            ToTable("Livros");

            HasKey(x => x.Id);

            Property(x => x.Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Titulo)
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(x => x.Autor)
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(x => x.Editora)
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(x => x.Edicao)
           .HasMaxLength(200)
           .HasColumnType("varchar");

            Property(x => x.Ano)
            .HasColumnType("varchar");

            Property(x => x.Idioma)
           .HasColumnType("varchar");

            Property(x => x.ISBN)
            .HasMaxLength(200)
            .HasColumnType("varchar");
        }
    }
}
