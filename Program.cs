using ProductManager.Domain;
using ProductManager.Data;
using static System.Console;

namespace ProductManager;

class Program
{
    public static void Main()
    {
        Title = "Product Manager";
        CursorVisible = false;

        while (true) // Loop som körs tills vi stänger ner applikationen
        {
            WriteLine("1. Ny produkt");
            WriteLine("2. Sök produkt");
            WriteLine("3. Avsluta");

            var keyPressedMenuChoice = ReadKey(true); // Väntar in knapptryck

            Clear();

            switch (keyPressedMenuChoice.Key)
            {
                case ConsoleKey.D1: // Case för menyval1
                case ConsoleKey.NumPad1:

                    RegisterProduct();

                    break;

                case ConsoleKey.D2: // Case för menyval2
                case ConsoleKey.NumPad2:

                    SearchProduct();

                    break;

                case ConsoleKey.D3: // Case för menyval3
                case ConsoleKey.NumPad3:

                    Environment.Exit(0); // Metodanrop i C# 

                    return;
            }

            Clear();
        }
    }

    //Metoderna
    private static void RegisterProduct() // Registrera en ny produkt
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


        var product = new Product // Samla ihop alla värden/för över värden i ett objekt/klass som representera Produkten.
        {
            Name = name,
            Sku = sku,
            Description = description,
            Image = image,
            Price = price
        };

        WriteLine();
        WriteLine();
        Write("Är detta korrekt? (J)a (N)ej");

        while (true)
        {
            var keyPressedConfirmRegisterProduct = ReadKey(true); // Väntar in knapptryck

            switch (keyPressedConfirmRegisterProduct.Key)
            {
                case ConsoleKey.J: // Case för knapptryck J

                    Clear();

                    SaveProduct(product); // Spara produkten

                    WriteLine("Produkt sparad");

                    Thread.Sleep(2000);

                    return;

                case ConsoleKey.N: // Case för knapptryck N

                    Clear();

                    RegisterProduct(); // Registrera produkten

                    return;
            }
        }

    }

    private static void SearchProduct() // Sök efter produkt
    {
        CursorVisible = true;

        Write("SKU: ");

        string sku = ReadLine();

        CursorVisible = false;

        Clear();


        var product = FindProduct(sku); // Skicka in sku i metoden FindProduct

        if (product is not null) // Om produkten hittades
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
                var keyPressedDeleteOrUpdateProduct = ReadKey(true); // Väntar in knapptryck

                switch (keyPressedDeleteOrUpdateProduct.Key) // Hantera knapptryck
                {
                    case ConsoleKey.R: // Case för knapptryck R

                        DeleteProduct(product);

                        return;

                    case ConsoleKey.Escape: // Case för knapptryck Escape

                        return;

                    case ConsoleKey.U: // Case för knapptryck U

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

    private static void UpdateProduct(Product product) // Updatera produkt
    {
        Clear();

        using var context = new ApplicationContext(); // Vårt context

        context.Product.Attach(product); // Fäster product till contexten - fattar att den ska ändras

        CursorVisible = true;

        Write("Namn: ");

        product.Name = ReadLine();

        WriteLine($"Sku: {product.Sku}"); // Visa värdet för egenskapen sku i produkt

        while (true) // While-loop som körs tills användaren trycker på Enter innan description visas
        {
            var keyPressedEnterAfterSku = ReadKey(true);
            if (keyPressedEnterAfterSku.Key == ConsoleKey.Enter)
                break;
        }

        Write("Beskrivning: ");

        product.Description = ReadLine();

        Write("Bild(URL): ");

        product.Image = ReadLine();

        Write("Pris: ");

        product.Price = ReadLine();

        CursorVisible = false;

        WriteLine();
        WriteLine();
        Write("Är detta korrekt? (J)a (N)ej");

        while (true)
        {
            var keyPressedConfirmUpdateProduct = ReadKey(true);

            switch (keyPressedConfirmUpdateProduct.Key)
            {
                case ConsoleKey.J:

                    Clear();

                    context.SaveChanges(); // Spara ändringarna i databasen 

                    WriteLine("Produkt sparad");

                    Thread.Sleep(2000);

                    return;

                case ConsoleKey.N:
                    Clear();

                    UpdateProduct(product);

                    return;
            }
        }
    }

    private static Product? FindProduct(string sku) // Hitta produkt - den här metoden returna två olika värden antingen produkten eller null
    {

        using var context = new ApplicationContext(); // Vårt context

        // Söker efterförsta produkten i produkttabellen som matchar sku
        // kommer att lagras i variabeln produkt. Om ingen matchning hittas kommer produkt att vara null.
        var product = context
       .Product
       .FirstOrDefault(x => x.Sku == sku);

        return product;
    }


    private static void SaveProduct(Product product)
    {
        using var context = new ApplicationContext(); // Vårt context-klass


        context.Product.Add(product); // Lägger vi till produkt till DbContext Product

        context.SaveChanges(); // Sparas sedan till databasen 
    }

    private static void DeleteProduct(Product product) // Radera produkt
    {
        using var context = new ApplicationContext();

        Clear();

        WriteLine($"Namn: {product.Name}");
        WriteLine($"Sku: {product.Sku}");
        WriteLine($"Beskrivning: {product.Description}");
        WriteLine($"Bild: {product.Image}");
        WriteLine($"Pris: {product.Price}");
        WriteLine("");
        WriteLine("");
        WriteLine("Radera produkt? (J)a eller (N)ej");

        while (true)
        {

            var keyPressedConfirmDeleteProduct = ReadKey(true); // Väntar in knapptryck

            switch (keyPressedConfirmDeleteProduct.Key)
            {
                case ConsoleKey.J:
                    Clear();

                    context.Product.Remove(product); // Ta bort produkt

                    context.SaveChanges(); // Spara i databasen

                    WriteLine("Produkt raderad");

                    Thread.Sleep(2000);

                    return;

                case ConsoleKey.N:

                    Clear();

                    WriteLine($"Namn: {product.Name}");
                    WriteLine($"Sku: {product.Sku}");
                    WriteLine($"Beskrivning: {product.Description}");
                    WriteLine($"Bild: {product.Image}");
                    WriteLine($"Pris: {product.Price}");
                    WriteLine("");
                    WriteLine("");
                    WriteLine("(R)adera" + "  " + "(U)ppdatera");

                    break;

            }

            switch (keyPressedConfirmDeleteProduct.Key)
            {
                case ConsoleKey.R: // Case för knapptryck R

                    DeleteProduct(product);

                    return;

                case ConsoleKey.Escape: // Case för knapptryck Escape

                    return;
            }
        }
    }
}

