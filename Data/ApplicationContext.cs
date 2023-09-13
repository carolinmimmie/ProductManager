using Microsoft.EntityFrameworkCore;
using ProductManager.Domain;

namespace ProductManager.Data;

public class ApplicationContext : DbContext
{

  private string connectionString = "Server=.;Database=ProductManager;Integrated Security=false;Encrypt=False;User ID=SA;Password=Dinoaugust123456!;";

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(connectionString);
  }
  //Kunna prata med tabellen som skapades
  public DbSet<Product> Product { get; set; }
}
