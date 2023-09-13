using ProductManager.Domain;
using ProductManager.Data;
using static System.Console;

namespace ProductManager;

class Program
{
    // static string connectionString = "ProductManager;User=SA;Password=Dinoaugust123456!;ConnectRetryCount=0;MultipleActiveResultSets=true;Encrypt=False";
    //Global Context
    static ApplicationContext context = new ApplicationContext();

    public static void Main()
    {
        Title = "Product Manager"; //sätter namnet på tabben
        CursorVisible = false; //stänger av markör

        while (true) //Loop som körs tills vi stänger ner den
        {
            WriteLine("1. Ny produkt");
            WriteLine("2. Sök produkt");
            WriteLine("3. Avsluta");

            var keyPressed = ReadKey(intercept: true); //hämtar in värdet

            Clear(); // Rensa skärmen efter vi gjort ett val

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1: //case för menyval1
                case ConsoleKey.NumPad1:

                    ShowRegisterProductView();

                    break;

                case ConsoleKey.D2: //case för menyval2
                case ConsoleKey.NumPad2:

                    ShowSearchProductView();

                    break;

                case ConsoleKey.D3: //case för menyval3
                case ConsoleKey.NumPad3:

                    Environment.Exit(0);

                    return;
            }

            Clear(); // Rensa skärmen efter vi fyllt i 
        }
    }

    //Metoderna
    private static Product? FindProduct(string sku) //returnera produkt eller null
    {

        //Kollar om det finns en produkt med Sku-numret användaren skrive i
        //om det finns returna produkten
        var product = context
       .Product
       .FirstOrDefault(x => x.Sku == sku);

        return product;

        //return context.Product.FirstOrDefault(x => x.Sku == sku);

    }
    private static void ShowSearchProductView()
    {
        Write("SKU: ");

        string sku = ReadLine();

        Clear();

        //skicka in socialSecurityNumber i metoden FindPatient
        var product = FindProduct(sku);

        //Om patienten hittades
        if (product is not null)
        {
            WriteLine($"Namn: {product.Name}");
            WriteLine($"Sku: {product.Sku}");
            WriteLine($"Beskrivning: {product.Description}");
            WriteLine($"Bild: {product.Image}");
            WriteLine($"Pris: {product.Price}");
            WriteLine("(R)adera");

            //while (ReadKey(true).Key is not ConsoleKey.Escape) ;//väntar på escape för att avluta loopen 

            char key = Console.ReadKey().KeyChar;

            if (key == 'R' || key == 'r')
            {
                DeleteProduct(product);
            }
        }
        else
        {
            WriteLine("Produkten finns ej");

            Thread.Sleep(2000);
        }
    }

    private static void ShowRegisterProductView()
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


        //För över värderna till Patient objektet (klassen)
        var product = new Product
        {
            Name = name,
            Sku = sku,
            Description = description,
            Image = image,
            Price = price
        };

        Clear();

        WriteLine("Är detta korrekt? (J)a (N)ej");

        char key = Console.ReadKey().KeyChar;

        if (key == 'J' || key == 'j')
        {
            Clear();

            SaveProduct(product);//spara patient

            WriteLine("Produkten sparad");

        }
        else if (key == 'N' || key == 'n')
        {
            Clear();

            ShowRegisterProductView();

        }
        else
        {
            Clear();

            WriteLine("Välj (J)a eller (N)ej");

        }

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


            // Här lägger vi till studerande till DbContext - den är nu medveten om
            // detta objektet men har ännu inte sparat den till databasen.
            context.Product.Remove(product);

            // Här triggas anrop till databasen för att lagra studerande.
            context.SaveChanges();

            WriteLine("Produkt raderad");

            Thread.Sleep(2000);

        }
        else if (key == 'N' || key == 'n')
        {
            
        }
    }

}