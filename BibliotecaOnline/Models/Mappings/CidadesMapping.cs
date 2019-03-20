using System.Data.Entity.ModelConfiguration;

namespace BibliotecaOnline.Models.Mappings
{
    public class CidadesMapping : EntityTypeConfiguration<Cidade>
    {
        public CidadesMapping()
        {
            ToTable("Cidades");
            HasKey(x => x.Codigo);

            Property(x => x.Codigo)
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(x => x.Nome)
            .HasMaxLength(200)
            .HasColumnType("varchar");

            Property(x => x.UF)
            .HasMaxLength(2)
            .HasColumnType("varchar");
        }
    }
}