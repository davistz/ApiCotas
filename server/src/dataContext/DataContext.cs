using ApiCotas.Cotas;
using Microsoft.EntityFrameworkCore;

namespace dataContext;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CotaEntity>? Cotas { get; set; }

}