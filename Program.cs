using ProductManager.Domain;
using ProductManager.Data;
using static System.Console;

namespace ProductManager;

class Program
{
    public static void Main()
    {
        Title = "Product Manager"; //sätter namnet på tabben
        CursorVisible = false; //stänger av markör

        while (true) //Loop som körs tills vi stänger ner den
        {
            WriteLine("1. Ny produkt");
            WriteLine("2. Sök produkt");
            WriteLine("3. Avsluta");

            var keyPressed = ReadKey(true); //Läser in knapptryck

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
        CursorVisible = true;

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

        CursorVisible = false;


        //Samla ihop alla värden och för över alla värden i ett objekt/klass som representera Produkten.
        var product = new Product
        {
            Name = name,
            Sku = sku,
            Description = description,
            Image = image,
            Price = price
        };

        WriteLine("");
        WriteLine("");
        Write("Är detta korrekt? (J)a (N)ej");

        var keyPressed = ReadKey(); //hämtar in knapptryck

        switch (keyPressed.Key)
        {
            case ConsoleKey.J: //case för menyval1

                Clear();

                SaveProduct(product); // Spara produkten

                WriteLine("Produkt sparad");

                Thread.Sleep(2000);

                break;

            case ConsoleKey.N: //case för menyval1

                Clear();

                RegisterProduct();

                break;

        }

    }

    private static void SearchProduct()
    {
        CursorVisible = true;

        Write("SKU: ");

        string sku = ReadLine();

        CursorVisible = false;

        Clear();

        //skicka in socialSecurityNumber i metoden FindPatient
        var product = FindProduct(sku);

        //Om produkten hittades
        if (product is not null)
        {
            WriteLine($"Namn: {product.Name}");
            WriteLine($"Sku: {product.Sku}");
            WriteLine($"Beskrivning: {product.Description}");
            WriteLine($"Bild (URL): {product.Image}");
            WriteLine($"Pris: {product.Price}");
            WriteLine("");
            WriteLine("");
            WriteLine("(R)adera" + "  " + "(U)ppdatera");

            while (true)
            {

                var keyPressed = ReadKey(true); //hämtar in knapptryck

                switch (keyPressed.Key)
                {
                    case ConsoleKey.R: //case för knapptryck R

                        DeleteProduct(product);

                        return;

                    case ConsoleKey.Escape: //case för knapptryck Escape

                        return;

                    case ConsoleKey.U: //case för knapptryck U

                        UpdateProduct(product);

                        return;
                }
            }
        }
        else
        {
            WriteLine("Produkt finns ej");

            Thread.Sleep(2000);
        }

    }

    private static void UpdateProduct(Product product)
    {
        Clear();

        using var context = new ApplicationContext();//vårt context

        context.Product.Attach(product);//attacha product till contexten com parameter - attach-läge innan vi ändrar den 
                                        //Samlar in och uppdatera nuvarande product - fattar att den ska ändras
                                        //Hämtar in nya värden ifrån användaren
        CursorVisible = true;
        Write("Namn: ");//läser in alla värden på  nytt

        product.Name = ReadLine();

        WriteLine($"Sku: {product.Sku}");

        Write("Beskrivning: ");

        product.Description = ReadLine();

        Write("Bild(URL): ");

        product.Image = ReadLine();

        Write("Pris: ");

        product.Price = ReadLine();

        CursorVisible = false;

        WriteLine("");
        WriteLine("");
        Write("Är detta korrekt? (J)a (N)ej");

        var keyPressed = ReadKey(true); //hämtar in knapptryck

        switch (keyPressed.Key)
        {
            case ConsoleKey.J: //case för menyval1

                Clear();

                context.SaveChanges(); // Spara ändringarna och genererar en uppdate som skickas till databashanteraren,
                //då DbContext ser att vi har förändrat modellen och därför krävs det en 
                //uppdatering av motsvarande data i databasen

                WriteLine("Produkt sparad");

                Thread.Sleep(2000);

                return;

            case ConsoleKey.N: //case för menyval1

                Clear();

                UpdateProduct(product);

                return;
        }

    }

    private static Product? FindProduct(string sku) // den här metoden returna två olika värden antingen movie eller null
    {

        using var context = new ApplicationContext();//vårt context

        // Resultatet av detta uttryck är att den första produkten i databastabellen som matchar sku
        // kommer att lagras i variabeln produkt. Om ingen matchning hittas kommer produkt att vara null.
        var product = context
       .Product
       .FirstOrDefault(x => x.Sku == sku);

        return product;

    }

    private static void SaveProduct(Product product)
    {
        using var context = new ApplicationContext();//vårt context-klass

        // Lägger vi till produkt till DbContext Product
        context.Product.Add(product);
        // Sparas sedan till databasen 
        context.SaveChanges();
    }

    private static void DeleteProduct(Product product)
    {
        using var context = new ApplicationContext();//vårt context

        Clear();

        WriteLine($"Namn: {product.Name}");
        WriteLine($"Sku: {product.Sku}");
        WriteLine($"Beskrivning: {product.Description}");
        WriteLine($"Bild: {product.Image}");
        WriteLine($"Pris: {product.Price}");
        WriteLine("");
        WriteLine("");
        WriteLine("Radera produkt? (J)a eller (N)ej");


        var keyPressed = ReadKey(true); //hämtar in knapptryck

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
                // if (product is not null)
                // {
                WriteLine($"Namn: {product.Name}");
                WriteLine($"Sku: {product.Sku}");
                WriteLine($"Beskrivning: {product.Description}");
                WriteLine($"Bild: {product.Image}");
                WriteLine($"Pris: {product.Price}");
                WriteLine("");
                WriteLine("");
                WriteLine("(R)adera" + "  " + "(U)ppdatera");

                while (true)
                {
                    var keyPressedTest = ReadKey(true); //hämtar in knapptryck

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

        // return;
        // }
    }

}