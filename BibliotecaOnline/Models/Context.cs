using BibliotecaOnline.Models.Mappings;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BibliotecaOnline.Models
{
    public class Context : DbContext
    {
        public Context()
             : base("name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<Context>(null);
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<LivroExemplar> Exemplares { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<EmprestimoItens> EmprestimoItens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ForeignKeyNavigationPropertyAttributeConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new PessoaMapping());
            modelBuilder.Configurations.Add(new EstadosMapping());
            modelBuilder.Configurations.Add(new CidadesMapping());
            modelBuilder.Configurations.Add(new LivrosMapping());
            modelBuilder.Configurations.Add(new LivroExemplaresMapping());
            modelBuilder.Configurations.Add(new EmprestimoMapping());
            modelBuilder.Configurations.Add(new EmprestimoItensMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}