using System.Data.Entity.ModelConfiguration;

namespace BibliotecaOnline.Models.Mappings
{
    public class LivroExemplaresMapping : EntityTypeConfiguration<LivroExemplar>
    {
        public LivroExemplaresMapping()
        {
            ToTable("Exemplares");
            HasKey(x => x.Id);

            Property(x => x.CodigoDeBarras)
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(x => x.Estante)
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(x => x.Setor)
           .HasMaxLength(200)
           .HasColumnType("varchar");

            Property(x => x.Campos)
           .HasMaxLength(200)
           .HasColumnType("varchar");

            Property(x => x.Status);
        }
    }
}