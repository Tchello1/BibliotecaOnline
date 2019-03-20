﻿using BibliotecaOnline.Models.Mappings;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BibliotecaOnline.Models
{
    public class Context : DbContext 
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<LivroExemplar> Exemplares { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new PessoaMapping());
            modelBuilder.Configurations.Add(new EstadosMapping());
            modelBuilder.Configurations.Add(new CidadesMapping());
            modelBuilder.Configurations.Add(new LivrosMapping());
            modelBuilder.Configurations.Add(new LivroExemplaresMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}