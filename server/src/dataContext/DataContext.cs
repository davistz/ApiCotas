using ApiCotas.Cotas;
using Microsoft.EntityFrameworkCore;

namespace dataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<CotaEntity> Cotas { get; set; }
        public DbSet<ConsorcioEntity> Consorcios { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UsuarioConsorcio> UserConsorcios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<UsuarioConsorcio>()
                .HasKey(uc => new { uc.UsuarioId, uc.ConsorcioId });

            modelBuilder.Entity<UsuarioConsorcio>()
                .HasOne(uc => uc.Usuario)
                .WithMany(u => u.UsuarioConsorcios) 
                .HasForeignKey(uc => uc.UsuarioId);

            modelBuilder.Entity<UsuarioConsorcio>()
                .HasOne(uc => uc.Consorcio)
                .WithMany(c => c.UsuarioConsorcios)
                .HasForeignKey(uc => uc.ConsorcioId);

          
            modelBuilder.Entity<CotaEntity>()
                .Property(c => c.Valor)
                .HasColumnType("decimal(18, 2)") 
                .IsRequired(); 

          
            modelBuilder.Entity<ConsorcioEntity>()
                .Property(c => c.ValorTotal)
                .HasColumnType("decimal(18, 2)")
                .IsRequired(); 
        }
    }
}