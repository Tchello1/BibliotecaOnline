using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BibliotecaOnline.Models.Mappings
{
    public class EmprestimoItensMapping : EntityTypeConfiguration<EmprestimoItens>
    {
        public EmprestimoItensMapping()
        {
            ToTable("EmprestimoItens");
            HasKey(x => x.Id);

            Property(x => x.Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ColaboradorId);

            Property(x => x.UsuarioId);

            Property(x => x.DataEmprestimo);

            Property(x => x.ColaboradorIdRenovacao)
            .IsOptional();

            Property(x => x.DataRenovacao)
            .IsOptional();

            Property(x => x.ColaboradorIdDevolucao)
            .IsOptional();

            Property(x => x.DataDevolucao)
            .IsOptional();

            Property(x => x.DataLimite)
            .IsOptional();

            Property(x => x.Status);

            Property(x => x.EmprestimoId)
                .IsRequired();

            HasRequired(x => x.Emprestimos)
            .WithMany(x => x.EmprestimoItens)
            .HasForeignKey(c => c.EmprestimoId);

            Property(x => x.ExemplarId)
            .IsRequired();

            HasRequired(x => x.Exemplares)
            .WithMany(x => x.EmprestimoItens)
            .HasForeignKey(c => c.ExemplarId);
        }
    }
}