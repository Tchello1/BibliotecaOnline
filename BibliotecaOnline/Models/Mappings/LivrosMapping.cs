using System.Data.Entity.ModelConfiguration;

namespace BibliotecaOnline.Models.Mappings
{
    public class LivrosMapping : EntityTypeConfiguration<Livro>
    {
        public LivrosMapping()
        {
            ToTable("Livros");
            HasKey(x => x.Id);

            Property(x => x.Autor)
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(x => x.Editora)
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(x => x.Edicao)
           .HasMaxLength(200)
           .HasColumnType("varchar");

            Property(x => x.Ano);
            Property(x => x.Idioma);

            Property(x => x.ISBN)
            .HasMaxLength(200)
            .HasColumnType("varchar");
        }
    }
}
