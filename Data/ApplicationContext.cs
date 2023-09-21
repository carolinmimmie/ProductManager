using Microsoft.EntityFrameworkCore;
using ProductManager.Domain;

namespace ProductManager.Data;

public class ApplicationContext : DbContext//Möjligör kommikationen med modellen och databasen
{

  private string connectionString = "Server=.;Database=ProductManager;Integrated Security=false;Encrypt=False;User ID=SA;Password=Dinoaugust123456!;";

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(connectionString);
  }
  //Kommunicera med  Modellen Product 
  public DbSet<Product> Product { get; set; }
}
