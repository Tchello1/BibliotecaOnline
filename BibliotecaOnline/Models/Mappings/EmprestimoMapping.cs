using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BibliotecaOnline.Models.Mappings
{
    public class EmprestimoMapping : EntityTypeConfiguration<Emprestimo>
    {
        public EmprestimoMapping()
        {
            ToTable("Emprestimos");
            HasKey(x => x.Id);

            Property(x => x.Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ColaboradorId);

            Property(x => x.UsuarioId);

            Property(x => x.DataEmprestimo);

            Property(x => x.Status);
        }
    }
}