using System;
using System.Collections;
using System.Collections.Generic;
using Vending_Machine;


class Program
{
    static void Main(string[] args)
    {
        VendingMachine vendingMachine = new VendingMachine();
        vendingMachine.AddDenomination(Denomination.OneKr, 10);
        vendingMachine.AddDenomination(Denomination.FiveKr, 10);
        vendingMachine.AddDenomination(Denomination.TenKr, 10);
        vendingMachine.AddDenomination(Denomination.TwentyKr, 10);
        vendingMachine.AddDenomination(Denomination.FiftyKr, 10);
        vendingMachine.AddDenomination(Denomination.HundredKr, 10);
        vendingMachine.AddDenomination(Denomination.FiveHundredKr, 10);
        vendingMachine.AddDenomination(Denomination.ThousandKr, 10);

        var snackVarieties = new Dictionary<string, string>
{
{ "OLW Grill Chips", "These crispy chips have a smoky barbecue flavor and are perfect for a snack or as an accompaniment to a cold beer." },
{
            "Estrella Dill Chips", "If you love dill, you will love these chips. The Estrella Dill Chips are made with real dill and have a subtle, yet distinct flavor." },
{
                "OLW Sour Cream & Onion", "Another classic flavor, the sour cream and onion chips from OLW have a perfect balance of tangy sour cream and savory onion." },
{
                    "Estrella Ranch Chips", "These chips have a tangy and creamy flavor with a hint of spice.They are perfect for dipping in ranch dressing." },
{
                        "OLW Ostbågar", "These cheese-flavored puffs are a favorite in Sweden. They are light and airy with a strong cheese flavor." },
{
                            "Estrella Paprika Chips", "These chips have a bold, smoky flavor with a hint of paprika.They are perfect for snacking or as an accompaniment to a sandwich." }
};
        var beverageVarieties = new Dictionary<string, string>
{
{ "Söderblandning", "Söderblandning is a tea blend invented in Sweden. It is based on black tea blended with tropical fruit and flowers." },
{ "Gotlandsdricka", "Gotlandsdricka is a beer style that originated on the Swedish island of Gotland. The brew was made with local ingredients and traditional techniques, and it is often classified as a type of farmhouse ale." },
{ "Punsch", "Swedish punsch is an arrack-based liqueur that was first introduced in the 18th century when the Swedish East India Company started importing Batavian arrack from Java. In its early days, punsch was enjoyed warm and prepared just before serving, by heating it and then adding spices and wine." },
{ "Pucko", "Pucko is a Swedish drink made with three basic ingredients: milk, chocolate, and sugar. It comes in an iconic glass bottle that was created by Arla Foods, a company which released it in 1954. Today, it is produced by a Danish company named Cocio." },
{ "Baltic Porter", "This beer style evolved from the British porter, and it was partially influenced by imperial stouts. The beers that fall under this category will typically have high alcohol content, usually between 5.5 and 9.5% ABV, while their color ranges from mahogany red to dark brown." },
{
                            "Akvavit", "Akvavit is a Scandinavian spirit that is usually distilled from grains or potatoes.After distillation, it is infused with various botanicals, though the most common are caraway and dill." },
{
                                "Julmust", "Julmust is a very popular Swedish soft drink that is consumed at Christmas time.This dark drink is often described as a mixture of cola and root beer, while its flavor is typically sweeter than most sodas." },
{
                                    "Glögg", "Glögg is a favorite winter drink in Scandinavia. In its basic form, this version of mulled wine combines red wine, sugar, and various spices such as cardamom, cinnamon, cloves, allspice, ginger, and orange zest." }
};
        var candyVarieties = new Dictionary<string, string>
{
{ "Marianne", "These classic Swedish wrapped hard candies are white and minty with a chocolate center." },
{ "Energy Jellies", "These yellow, blue, and red gummies, dusted in sour sugar, are reminiscent of The Matrix's red pill/blue pill quandary." },
{ "Sweet Hearts", "Half-jelly, half-gummy, what's not to love about these big sweets?" },
{ "Skumkantereller", "The name means 'foam chanterelles'; these marshmallow sweets taste nothing like mushrooms. Texture-wise, they are chewier than American marshmallows." },
{ "Bilar", "Bilar means 'cars' in Swedish. These chewy gummies have a light citrus-floral perfume to them. They can also be found at IKEA. The tagline, 'Sveriges mest kopta bil,' means 'Sweden's most purchased car.'" }
};
        vendingMachine.AddProduct(1, new Drink { Id = 1, Name = "Beverages", Cost = 20, Volume = 300, Varieties = beverageVarieties });
        vendingMachine.AddProduct(2, new Snack { Id = 2, Name = "Chips", Cost = 15, Volume = 200, Varieties = snackVarieties });
        vendingMachine.AddProduct(3, new Candy { Id = 3, Name = "Minty Candy", Cost = 5, Volume = 100, Varieties = candyVarieties });

        Console.WriteLine("Welcome to the Vending Machine!");

        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Your current balance:{vendingMachine.GetCurrentBalance()} kr");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Please select a product:");
            foreach (string productInfo in vendingMachine.ShowAllProducts())
            {
                Console.WriteLine(productInfo);
            }
            Console.WriteLine();
            Console.Write("Enter product number to purchase (0 to exit, R to recharge, C to claim change): ");
            string input = Console.ReadLine();
            if (input.Equals("R", StringComparison.OrdinalIgnoreCase))
            {
                RechargeBalance(vendingMachine);
            }
            else if (input.Equals("C", StringComparison.OrdinalIgnoreCase))
            {
                if (vendingMachine.GetCurrentBalance() > 0)
                {
                    var change = vendingMachine.EndTransaction();
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Change claimed. Goodbye! Have a great day.");
                    Console.WriteLine("-------------------------------");
                    foreach (var denomination in change)
                    {
                        Console.WriteLine($"Change: {denomination.Value} x {denomination.Key}");
                    }
                }
                else
                {
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("You don't have a balance to claim.");
                }
                isRunning = true;
            }
            else if (int.TryParse(input, out int productId))
            {
                if (productId == 0)
                {
                    var change = vendingMachine.EndTransaction();
                    Console.WriteLine("Goodbye! Have a great day.");
                    foreach (var denomination in change)
                    {
                        Console.WriteLine($"Change: {denomination.Value} x {denomination.Key}");
                    }
                    isRunning = false;
                }
                else
                {
                    Product product = vendingMachine.Purchase(productId);
                    if (product != null)
                    {
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds or invalid product.");
                    }
                }
            }
        }
    }
    static void RechargeBalance(VendingMachine vendingMachine)
    {
        Console.WriteLine();
        Console.WriteLine("Recharge Balance Menu:");
        Console.WriteLine();
        Console.WriteLine("Available Denominations:");
        foreach (Denomination denomination in Enum.GetValues(typeof(Denomination)))
        {
            Console.WriteLine($"{(int)denomination} kr: {denomination}");
        }
        int rechargeAmount = 0;
        while (true)
        {
            Console.WriteLine();
            Console.Write("Enter the denomination to recharge (0 to proceed): ");
            if (int.TryParse(Console.ReadLine(), out int denominationValue) &&
            denominationValue >= 0)
            {
                if (denominationValue == 0)
                {
                    break;
                }
                if (Enum.IsDefined(typeof(Denomination), denominationValue))
                {
                    rechargeAmount += denominationValue;
                    vendingMachine.InsertMoney((Denomination)denominationValue);
                    Console.WriteLine();
                    Console.WriteLine($"You have recharged {denominationValue} kr.");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid denomination. Please choose from available denomina0tions.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive integer or 0 to proceed.");
            }
        }
    }
}