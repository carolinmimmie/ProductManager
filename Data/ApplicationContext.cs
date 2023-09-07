using Microsoft.EntityFrameworkCore;
using ProductManager.Domain;

namespace ProductManager.Data;

public class ApplicationContext : DbContext
{


    public readonly string connectionString;

    public ApplicationContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }


    //Gör vårat DbSet public och koppla det till tabellen Student - glöm inte göra den public i klass
    public DbSet<Product> Product { get; set; }
}
