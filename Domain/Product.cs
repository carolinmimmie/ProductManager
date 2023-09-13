using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.Domain;

//Skapat en klasss som representerar strukturen för patient 

//required eller konstruktor gör det obligatoriskt att alla fält måste fyllas i 
public class Product
{
     // auto-implemented properties som har get och set metoder
    [MaxLength(50)]
    public required string Name { get; set; }

    [Key]
    [Column(TypeName = "nchar(10)")]//bestämd längd
    public required string Sku { get; set; }

    [MaxLength(100)]
    public required string Description { get; set; }

    [MaxLength(50)]
    public required string Image { get; set; }

    [MaxLength(20)]
    public required string Price { get; set; }

}

// using System.Globalization;
// using System.Text.RegularExpressions;
// using System.ComponentModel.DataAnnotations;

// namespace ProductManager.Domain;

// public class Product
// {
//     //Primärnyckel
//     public int Id { get; set; }
//     // auto-implemented properties som har get och set metoder

//     [MaxLength(50)]
//     public required string Name { get; set; }
//     [MaxLength(10)]
//     public required string Sku { get; set; }
//     [MaxLength(50)]
//     public required string Description { get; set; }
//     [MaxLength(50)]
//     public required string Image { get; set; }
//     [MaxLength(20)]
//     public required string Price { get; set; }
// }

