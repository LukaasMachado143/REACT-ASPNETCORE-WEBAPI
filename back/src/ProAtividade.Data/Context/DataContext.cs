using Microsoft.EntityFrameworkCore;
using ProAtividade.Data.Mappings;
using ProAtividade.Domain.Entities;

namespace ProAtividade.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<AtividadeModel>? Atividades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AtividadeMap());
        }
    }
}