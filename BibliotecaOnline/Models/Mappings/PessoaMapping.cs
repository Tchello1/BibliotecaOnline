using System.Data.Entity.ModelConfiguration;

namespace BibliotecaOnline.Models.Mappings
{
    public class PessoaMapping : EntityTypeConfiguration<Pessoa>
    {
        public PessoaMapping()
        {
            ToTable("Pessoas");
            HasKey(x => x.Id);

            Property(x => x.Tipo)
            .IsRequired();

            Property(x => x.Nome)
           .HasMaxLength(200)
           .HasColumnType("varchar")
           .IsRequired();

            Property(x => x.Email)
           .HasMaxLength(200)
           .HasColumnType("varchar")
           .IsRequired();

            Property(x => x.Senha)
           .HasMaxLength(200)
           .HasColumnType("varchar")
           .IsRequired();

            Property(x => x.Matricula)
           .HasMaxLength(200)
           .HasColumnType("varchar")
           .IsRequired();

            Property(x => x.Cidade)
           .HasMaxLength(200)
           .HasColumnType("varchar")
           .IsRequired();

            Property(x => x.UF)
           .HasMaxLength(2)
           .HasColumnType("varchar")
           .IsRequired();

            Property(x => x.Status);

        }
    }
}