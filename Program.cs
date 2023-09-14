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

            var keyPressed = ReadKey(); //hämtar in knapptryck

            Clear(); // Rensa skärmen efter vi gjort ett val

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1: //case för menyval1
                case ConsoleKey.NumPad1:

                    RegisterProduct();

                    break;

                case ConsoleKey.D2: //case för menyval2
                case ConsoleKey.NumPad2:

                    SearchProduct();

                    break;

                case ConsoleKey.D3: //case för menyval3
                case ConsoleKey.NumPad3:

                    Environment.Exit(0);

                    return;
            }

            Clear(); // Rensa skärmen efter val i meny
        }
    }

    //Metoderna
    private static void RegisterProduct()
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

        WriteLine("Är detta korrekt? (J)a (N)ej");

        var keyPressed = ReadKey(); //hämtar in knapptryck

        switch (keyPressed.Key)
        {
            case ConsoleKey.J: //case för menyval1

                Clear();

                SaveProduct(product); // Spara produkten

                WriteLine("Produkten sparad");

                break;

            case ConsoleKey.N: //case för menyval1

                Clear();

                RegisterProduct();

                break;

                // default:
                //     Clear();
                //     WriteLine("Välj (J)a eller (N)ej");
                //     break;
        }

        // char key = Console.ReadKey().KeyChar;

        // if (key == 'J' || key == 'j')
        // {
        //     Clear();

        //     SaveProduct(product);//spara patient

        //     WriteLine("Produkten sparad");

        // }
        // else if (key == 'N' || key == 'n')
        // {
        //     Clear();

        //     RegisterProduct();

        // }
        // else
        // {
        //     Clear();

        //     WriteLine("Välj (J)a eller (N)ej");

        // }

        Thread.Sleep(2000);
    }

    private static void SearchProduct()
    {
        Write("SKU: ");

        string sku = ReadLine();

        Clear();

        //skicka in socialSecurityNumber i metoden FindPatient
        var product = FindProduct(sku);

        //Om produkten hittades
        if (product is not null)
        {
            WriteLine($"Namn: {product.Name}");
            WriteLine($"Sku: {product.Sku}");
            WriteLine($"Beskrivning: {product.Description}");
            WriteLine($"Bild: {product.Image}");
            WriteLine($"Pris: {product.Price}");
            WriteLine("(R)adera");

            while (true)
            {

                var keyPressed = ReadKey(); //hämtar in knapptryck

                switch (keyPressed.Key)
                {
                    case ConsoleKey.R: //case för knapptryck R

                        DeleteProduct(product);

                        return;

                    case ConsoleKey.Escape: //case för knapptryck Escape

                        return;
                }
            }
        }
        else
        {
            WriteLine("Produkt finns ej");

            Thread.Sleep(2000);
        }




        //while (ReadKey(true).Key is not ConsoleKey.Escape) ;//väntar på escape för att avluta loopen 

        //     char key = Console.ReadKey().KeyChar;

        //     if (key == 'R' || key == 'r')
        //     {
        //         DeleteProduct(product);
        //     }
        // }
        // else
        // {
        //     WriteLine("Produkten finns ej");

        //     Thread.Sleep(2000);

    }

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


        var keyPressed = ReadKey(); //hämtar in knapptryck

        switch (keyPressed.Key)
        {
            case ConsoleKey.J: //case för menyval1
                Clear();
                // Här lägger vi till studerande till DbContext - den är nu medveten om
                // detta objektet men har ännu inte sparat den till databasen.
                context.Product.Remove(product);

                // Här triggas anrop till databasen för att lagra studerande.
                context.SaveChanges();

                WriteLine("Produkt raderad");

                Thread.Sleep(2000);

                return;

            case ConsoleKey.N: //case för menyval1
                Clear();

                //skicka in socialSecurityNumber i metoden FindPatient

                //Om produkten hittades
                if (product is not null)
                {
                    WriteLine($"Namn: {product.Name}");
                    WriteLine($"Sku: {product.Sku}");
                    WriteLine($"Beskrivning: {product.Description}");
                    WriteLine($"Bild: {product.Image}");
                    WriteLine($"Pris: {product.Price}");
                    WriteLine("(R)adera");

                    while (true)
                    {

                        var keyPressedTest = ReadKey(); //hämtar in knapptryck

                        switch (keyPressedTest.Key)
                        {
                            case ConsoleKey.R: //case för knapptryck R

                                DeleteProduct(product);

                                return;

                            case ConsoleKey.Escape: //case för knapptryck Escape

                                return;
                        }
                    }
                }

                return;
        }

        // char key = Console.ReadKey().KeyChar;

        // if (key == 'J' || key == 'j')
        // {

        //     Clear();


        //     // Här lägger vi till studerande till DbContext - den är nu medveten om
        //     // detta objektet men har ännu inte sparat den till databasen.
        //     context.Product.Remove(product);

        //     // Här triggas anrop till databasen för att lagra studerande.
        //     context.SaveChanges();

        //     WriteLine("Produkt raderad");

        //     Thread.Sleep(2000);

        // }
        // else if (key == 'N' || key == 'n')
        // {

        // }
    }

}