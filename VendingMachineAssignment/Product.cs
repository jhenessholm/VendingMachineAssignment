using System;
using System.Collections.Generic;
using System.Linq;

namespace Vending_Machine
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Volume { get; set; }

        public abstract string Examine();
        public abstract string Use();
    }
    public class Drink : Product
    {
        public Dictionary<string, string> Varieties { get; set; }
        public override string Examine()
        {
            if (Varieties != null && Varieties.Count > 0)
            {
                var varietyList = Varieties.Select((v, index) => $"{index + 1}. {v.Key}");
                string varietyText = string.Join(Environment.NewLine, varietyList);
                return $"Available Beverages (In Can):{Environment.NewLine}{varietyText}";
            }
            else
            {
                return $"Beverage - {Name}, Cost: {Cost} kr, Volume: {Volume} ml";
            }
        }
        public override string Use()
        {
            return $"Enjoy your {Name}!";
        }
    }
    public class Snack : Product
    {
        public Dictionary<string, string> Varieties { get; set; }
        public override string Examine()
        {
            if (Varieties != null && Varieties.Count > 0)
            {
                var varietyList = Varieties.Select((v, index) => $"{index + 1}. {v.Key}");
                string varietyText = string.Join(Environment.NewLine, varietyList);
                return $"Available Snacks:{Environment.NewLine}{varietyText}";
            }
            else
            {
                return $"{Name} - Cost: {Cost} kr";
            }
        }
        public override string Use()
        {
            return $"Enjoy your {Name}!";
        }
    }
    public class Candy : Product
    {
        public Dictionary<string, string> Varieties { get; set; }
        public override string Examine()
        {
            if (Varieties != null && Varieties.Count > 0)
            {
                var varietyList = Varieties.Select((v, index) => $"{index + 1}. {v.Key}");
                string varietyText = string.Join(Environment.NewLine, varietyList);
                return $"Available Candy:{Environment.NewLine}{varietyText}";
            }
            else
            {
                return $"{Name} - Cost: {Cost} kr";
            }
        }
        public override string Use()
        {
            return "Enjoy your candy!";
        }
    }
}
