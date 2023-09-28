using ControleFacil.Api.Data.Mappings;
using ControleFacil.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<NaturezaDeLancamento> NaturezaDeLancamentos { get; set; }

        public DbSet<Apagar> Apagar { get; set; }
        public DbSet<Areceber> Areceber { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Usuarios = Set<Usuario>();
            NaturezaDeLancamentos = Set<NaturezaDeLancamento>();
            Apagar = Set<Apagar>();
            Areceber = Set<Areceber>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new NaturezaDeLancamentoMap());
            modelBuilder.ApplyConfiguration(new ApagarMap());
            modelBuilder.ApplyConfiguration(new AreceberMap());
        }

    }
}
