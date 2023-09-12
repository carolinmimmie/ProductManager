using System.Globalization;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace ProductManager.Domain;

public class Product
{
      //Lägger till Id om vi använder oss av det i databasen
    public int Id { get; set; }
    // auto-implemented properties som har get och set metoder

    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(10)]
    public string Sku { get; set; }
    [MaxLength(50)]
    public string Description { get; set; }
    [MaxLength(50)]
    public string Image { get; set; }
    [MaxLength(20)]
    public string Price { get; set; }

   

    //konstruktor som vill ha strängar som kommer in
    // public Product(string name, string sku, string description, string image, string price)
    // {

    //     Name = name;
    //     Sku = sku;
    //     Description = description;
    //     Image = image;
    //     Price = price;
  
    // }


    //Privata Fält kan placeras här 
    //private string socialSecurityNumber;
}

