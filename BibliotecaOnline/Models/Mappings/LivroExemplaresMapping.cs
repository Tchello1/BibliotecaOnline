using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BibliotecaOnline.Models.Mappings
{
    public class LivroExemplaresMapping : EntityTypeConfiguration<LivroExemplar>
    {
        public LivroExemplaresMapping()
        {
            ToTable("Exemplares");
            HasKey(x => x.Id);

            Property(x => x.Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.CodigoDeBarras)
            .HasMaxLength(200)
            .HasColumnType("varchar")
            .IsOptional();

            Property(x => x.Estante)
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(x => x.Setor)
           .HasMaxLength(200)
           .HasColumnType("varchar");

            Property(x => x.Campos)
           .HasMaxLength(200)
           .HasColumnType("varchar")
           .IsRequired();

            Property(x => x.Quantidade).IsOptional();

            Property(x => x.Status)
                .IsRequired();

            Property(x => x.LivroId)
                .IsRequired();

            HasRequired(x => x.Livros)
                .WithMany(x => x.Exemplares)
                .HasForeignKey(c => c.LivroId);

        }
    }
}