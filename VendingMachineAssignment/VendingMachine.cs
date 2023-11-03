using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace Vending_Machine
{
    public class VendingMachine
    {
        private readonly Dictionary<Denomination, int> availableDenominations = new
        Dictionary<Denomination, int>();
        private Dictionary<int, Product> products = new Dictionary<int, Product>();
        private int moneyPool = 0;

        public void AddDenomination(Denomination denomination, int count)
        {
            availableDenominations[denomination] = count;
        }
        public void AddProduct(int id, Product product)
        {
            products[id] = product;
        }
        public void InsertMoney(Denomination denomination)
        {
            if (availableDenominations.ContainsKey(denomination) && availableDenominations[denomination] > 0)
            {
                moneyPool += (int)denomination;
                availableDenominations[denomination]--;
            }
        }
        public List<string> ShowAllProducts()
        {
            var productInfo = new List<string>();
            foreach (var product in products)
            {
                productInfo.Add($"{product.Key}: {product.Value.Name} - {product.Value.Cost} kr");
            }
            return productInfo;
        }
        public string GetProductDetails(int productId)
        {
            if (products.ContainsKey(productId))
            {
                return products[productId].Examine();
            }
            return "Product not found.";
        }
        public Product Purchase(int productId)
        {
            if (products.ContainsKey(productId) && moneyPool >= products[productId].Cost)
            {
                moneyPool -= products[productId].Cost;

                if (products[productId] is Drink drink)
                {
                    Console.WriteLine();
                    Console.WriteLine(drink.Examine());
                    Console.WriteLine();
                    Console.Write("Select a beverage (1-8): ");
                    if (int.TryParse(Console.ReadLine(), out int varietyChoice) && varietyChoice >= 1 && varietyChoice <= drink.Varieties.Count)
                    {
                        var selectedVariety = drink.Varieties.ElementAt(varietyChoice - 1);

                        Console.WriteLine("------------------------------- ");
                        Console.WriteLine("RECEIPT");
                        Console.WriteLine("");
                        Console.WriteLine($"You purchased: {selectedVariety.Key}");
                        Console.WriteLine("Category: Beverages");
                        Console.WriteLine();
                        Console.WriteLine($"Description: {selectedVariety.Value}");
                        Console.WriteLine();
                        Console.WriteLine($"Instruction: Pull up the tab with your index finger and thumb, the tab will pierce the can and create an opening.After that you can now {drink.Use()}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid beverage variety selection.");
                        return null;
                    }
                }
                else if (products[productId] is Snack snack)
                {
                    Console.WriteLine();
                    Console.WriteLine(snack.Examine());
                    Console.WriteLine();
                    Console.Write("Select a flavor (1-6): ");
                    if (int.TryParse(Console.ReadLine(), out int varietyChoice) && varietyChoice >= 1 && varietyChoice <= snack.Varieties.Count)
                    {
                        var selectedVariety = snack.Varieties.ElementAt(varietyChoice - 1);
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("RECEIPT");
                        Console.WriteLine("");
                        Console.WriteLine($"You purchased: {selectedVariety.Key}");
                        Console.WriteLine("Category: Chips");
                        Console.WriteLine();
                        Console.WriteLine($"Description: {selectedVariety.Value}");
                        Console.WriteLine();
                        Console.WriteLine($"Instruction: Locate the opening tab or tear along the top of the chip bag and gently pull the bag apart to open it without tearing it too much, After that you can now{snack.Use()}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid chip variety selection.");
                        return null;
                    }
                }
                else if (products[productId] is Candy candy)
                {
                    Console.WriteLine();
                    Console.WriteLine(candy.Examine());
                    Console.WriteLine();
                    Console.Write("Select a candy (1-5): ");
                    if (int.TryParse(Console.ReadLine(), out int varietyChoice) && varietyChoice >= 1 &&
                    varietyChoice <= candy.Varieties.Count)
                    {
                        var selectedVariety = candy.Varieties.ElementAt(varietyChoice - 1);
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("RECEIPT");
                        Console.WriteLine("");
                        Console.WriteLine($"You purchased: {selectedVariety.Key}");
                        Console.WriteLine("Category: Candy");
                        Console.WriteLine();
                        Console.WriteLine($"Description: {selectedVariety.Value}");
                        Console.WriteLine();
                        Console.WriteLine($"Instruction: Unwrap the candy and hold it with your two fingers, After that place it in your mouth and {candy.Use()}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid candy variety selection.");
                        return null;
                    }
                }
                return products[productId];
            }
            return null;
        }
        public Dictionary<Denomination, int> EndTransaction()
        {
            Dictionary<Denomination, int> change = new Dictionary<Denomination, int>();
            int remainingChange = moneyPool;
            foreach (Denomination denomination in Enum.GetValues(typeof(Denomination))
            .Cast<Denomination>()
            .OrderByDescending(d => (int)d))
            {
                int denominationValue = (int)denomination;
                if (denominationValue == 0)
                {
                    continue;
                }
                int denominationCount = remainingChange / denominationValue;
                if (denominationCount > 0)
                {
                    change[denomination] = denominationCount;
                    remainingChange -= denominationCount * denominationValue;
                }
            }
            moneyPool = 0;
            return change;
        }
        public int GetCurrentBalance()
        {
            return moneyPool;
        }
    }
}
