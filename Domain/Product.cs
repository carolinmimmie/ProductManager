using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductManager.Domain;

//Skapat en klasss som representerar strukturen för patient 

//required eller konstruktor gör det obligatoriskt att alla fält måste fyllas i 
[Index(nameof(Sku), IsUnique = true)]//unik
public class Product
{
    public int Id { get; set; }
    // auto-implemented properties som har get och set metoder
    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(20)]//bestämd längd
    public required string Sku { get; set; }

    [MaxLength(50)]
    public required string Description { get; set; }

    [MaxLength(50)]
    public required string Image { get; set; }

    [MaxLength(20)]
    public required string Price { get; set; }

}



