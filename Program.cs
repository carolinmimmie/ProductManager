using ProductManager.Data;
using ProductManager.Domain;
using static System.Console;

namespace ProductManager;

class Program
{
   // static string connectionString = "ProductManager;User=SA;Password=Dinoaugust123456!;ConnectRetryCount=0;MultipleActiveResultSets=true;Encrypt=False";
    //Global Context
    static string connectionString = "Server=.;Database=ProductManager;Integrated Security=false;Encrypt=False;User ID=SA;Password=Dinoaugust123456!;";
    static ApplicationContext context = new ApplicationContext(connectionString);

    static void Main()
    {
        CursorVisible = false;
        Title = "Product Manager";

        while (true)
        {
            WriteLine("1. Ny produkt");
            WriteLine("2. Sök produkt");
            WriteLine("3. Avsluta");

            var keyPressed = ReadKey(intercept: true);

            Clear();

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    ShowRegisterStudentView();

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    ShowSearchProductView();

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    Environment.Exit(0);

                    return;
            }

            Clear();
        }
    }

    //Metoderna
    private static Product GetProductBySku(string sku){
        //return a single Product or null if no product is found.

        return context.Product.FirstOrDefault(x => x.Sku == sku);
    }
private static void ShowSearchProductView()
{
    Write("Sku: ");

    string sku = ReadLine();

    Clear();

    var product = GetProductBySku(sku);

    if (product is not null)
    {
        WriteLine($"Namn: {product.Name}");
        WriteLine($"Sku: {product.Sku}");
        WriteLine($"Beskrivning: {product.Description}");
        WriteLine($"Bild: {product.Image}");
        WriteLine($"Pris: {product.Price}");
        WriteLine("(R)adera");

        char key = Console.ReadKey().KeyChar;

        if (key == 'R' || key == 'r')
        {
            // Implement the logic to delete the product here
            DeleteProduct(product);
        }
    }
    else
    {
        WriteLine("Produkten finns ej");
        Thread.Sleep(2000);
    }
}

    private static void ShowRegisterStudentView()
    {
        Write("Namn: ");

        string name = ReadLine();

        Write("SKU: ");

        string sku = ReadLine();

        Write("Beskrivning: ");

        string description = ReadLine();

        Write("Bild (URL): ");

        string image = ReadLine();

        Write("Pris: ");

        string price = ReadLine();

       

        var product = new Product(
            name,
            sku,
            description,
            image,
            price);

        Clear();

        WriteLine("Är detta korrekt? (J)a (N)ej");

        char key = Console.ReadKey().KeyChar;

        if (key == 'J' || key == 'j')
        {
            Clear();

            SaveProduct(product);

            WriteLine("Produkten sparad");

        }
        else if (key == 'N' || key == 'n')
        {
            Clear();
            // WriteLine($"Namn: {product.Name}");
            // WriteLine($"Sku: {product.Sku}");
            // WriteLine($"Beskrivning: {product.Description}");
            // WriteLine($"Bild: {product.Image}");
            // WriteLine($"Pris: {product.Price}");
            // WriteLine("(R)adera");
           
        }
        else
        {
             Clear();
             WriteLine("Välj (J)a eller (N)ej");
     
        }

       

       
        

        // try
        // {
        //     SaveProduct(product);

        //     WriteLine("Produkt sparad");
        // }
        // catch
        // {
        //     WriteLine("Studerande redan registrerad");
        // }

        Thread.Sleep(2000);
    }

    private static void SaveProduct(Product product)
    {

        //2 Kommandon

        // Här lägger vi till studerande till DbContext - den är nu medveten om
        // detta objektet men har ännu inte sparat den till databasen.
        context.Product.Add(product);
       

        // Här triggas anrop till databasen för att lagra studerande.
        context.SaveChanges();
    }
   private static void DeleteProduct(Product product)
{
    Clear();

    WriteLine($"Namn: {product.Name}");
    WriteLine($"Sku: {product.Sku}");
    WriteLine($"Beskrivning: {product.Description}");
    WriteLine($"Bild: {product.Image}");
    WriteLine($"Pris: {product.Price}");
    WriteLine("Radera produkt? (J)a eller (N)ej");

    char key = Console.ReadKey().KeyChar;

    if (key == 'J' || key == 'j')
    {
        Clear();
        
        //2 Kommandon

        // Här lägger vi till studerande till DbContext - den är nu medveten om
        // detta objektet men har ännu inte sparat den till databasen.
        context.Product.Remove(product);

        // Här triggas anrop till databasen för att lagra studerande.
        context.SaveChanges();
        WriteLine("Produkt raderad");

        Thread.Sleep(2000);

        Clear();
    }
    else if (key == 'N' || key == 'n')
    {
        Clear();
        // Handle "No" option logic here if needed
    }
}

}