using System;


class Program
{
    static void Main(string[] args)
    {
        // Create US address
        var addressUS = new Address(
            street: "123 Main Street",
            city: "Anytown",
            stateOrProvince: "CA",
            country: "USA"
        );

        // Create international address
        var addressInternational = new Address(
            street: "456 Elm Street",
            city: "Toronto",
            stateOrProvince: "Ontario",
            country: "Canada"
        );

        // Create customers
        var customerUS = new Customer("Juan Pérez", addressUS);
        var customerINT = new Customer("Ana García", addressInternational);

        // ORDER 1: 2 products
        var p1a = new Product("Latte", "P001", 4.50m, 2);
        var p1b = new Product("Cup", "P002", 8.00m, 1);
        var order1 = new Order(customerUS, new List<Product> { p1a, p1b });

        // ORDER 2 (Option 1: non-coffee, non-related products)
        var p2a = new Product("Blue Ink Ballpoint Pen", "P006", 1.25m, 6);
        var p2b = new Product("A5 Notepad", "P007", 3.50m, 4);
        var p2c = new Product("USB-C 30W Charger", "P008", 19.99m, 2);
        var order2 = new Order(customerINT, new List<Product> { p2a, p2b, p2c });

        // Output for Order 1
        Console.WriteLine("Order 1");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine();
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine();
        Console.WriteLine($"Total Price: ${order1.GetTotalCost():0.00}");
        Console.WriteLine(new string('-', 40));

        // Output for Order 2
        Console.WriteLine("Order 2");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine();
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine();
        Console.WriteLine($"Total Price: ${order2.GetTotalCost():0.00}");
        Console.WriteLine(new string('-', 40));
    }
}