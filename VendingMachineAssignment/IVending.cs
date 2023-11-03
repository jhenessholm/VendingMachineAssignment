using System;
using System.Collections.Generic;
namespace Vending_Machine
{
    public interface IVending
    {
        Product Purchase(int productId);
        List<string> ShowAllProducts();
        string GetProductDetails(int productId);
        void InsertMoney(Denomination denomination);
        Dictionary<Denomination, int> EndTransaction();
    }
}


