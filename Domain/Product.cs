using System.Globalization;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace ProductManager.Domain;

public class Product
{
      //Lägger till Id om vi använder oss av det i databasen
    public int Id { get; set; }
    // auto-implemented properties som har get och set metoder
       private string name;

    [MaxLength(50)]
   public required string Name { 
        get => name; 
        set
        {
            // Guard clause - säkerställer att vi inte få in "dålig" 
            // data för förnamn
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }

            name = value;
        }   
    }
      private string sku;

    [MaxLength(10)]
       public required string Sku { 
        get => sku; 
        set
        {
            // Guard clause - säkerställer att vi inte få in "dålig" 
            // data för förnamn
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Sku cannot be null or empty");
            }

            sku = value;
        }   
    }
           private string description;

    [MaxLength(50)]
   public required string Description { 
        get => description; 
        set
        {
            // Guard clause - säkerställer att vi inte få in "dålig" 
            // data för förnamn
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Description cannot be null or empty");
            }

            description = value;
        }   
    }
         private string image;

    [MaxLength(50)]
   public required string Image { 
        get => image; 
        set
        {
            // Guard clause - säkerställer att vi inte få in "dålig" 
            // data för förnamn
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Image cannot be null or empty");
            }

            image = value;
        }   
    }

    private string price;

    [MaxLength(20)]
      public required string Price { 
        get => price; 
        set
        {
            // Guard clause - säkerställer att vi inte få in "dålig" 
            // data för förnamn
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Price cannot be null or empty");
            }

            image = price;
        }   
    }
}

