using System;
using Northwind.Data;
using System.Linq;

class Program
{
    static void Main()
    {
        DateTime startDate = new DateTime(1994,1,1);
        DateTime endDate = new DateTime(1997, 7, 17);
        FindOrders("SP", startDate, endDate);
    }

    static void FindOrders(string region, DateTime startDate, DateTime endDate) 
    {
        
        NorthwindEntities db = new NorthwindEntities();
        using (db)
        {
            var sales = db.Orders.Where(o => o.ShipRegion == region && o.OrderDate >= startDate && o.OrderDate <= endDate).
                OrderBy(o => o.ShipRegion);
            Console.WriteLine("ShipRegion | Date");
            foreach (Order sale in sales)
            {
                Console.WriteLine(sale.ShipRegion + " | " + sale.ShippedDate);
            }
        }
    }
}

