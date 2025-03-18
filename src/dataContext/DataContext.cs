using ApiCotas.Cotas;
using Microsoft.EntityFrameworkCore;

namespace dataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<CotaEntity> Cotas { get; set; }
        public DbSet<GrupoEntity> Consorcios { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CotaEntity>()
                .Property(c => c.Valor)
                .HasColumnType("decimal(18, 2)") 
                .IsRequired(); 
          
            modelBuilder.Entity<GrupoEntity>()
                .Property(c => c.ValorTotal)
                .HasColumnType("decimal(18, 2)")
                .IsRequired(); 
        }
    }
}