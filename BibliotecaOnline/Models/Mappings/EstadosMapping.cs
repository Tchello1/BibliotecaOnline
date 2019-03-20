using System.Data.Entity.ModelConfiguration;

namespace BibliotecaOnline.Models.Mappings
{
    public class EstadosMapping : EntityTypeConfiguration<Estado>
    {
        public EstadosMapping()
        {
            ToTable("Estados");
            HasKey(x => x.CodigoUf);

            Property(x => x.CodigoUf)
           .HasMaxLength(200)
           .HasColumnType("varchar");

            Property(x => x.Nome)
           .HasMaxLength(200)
           .HasColumnType("varchar");

            Property(x => x.Uf)
            .HasMaxLength(2)
            .HasColumnType("varchar");

            Property(x => x.Regiao);
        }
    }
}