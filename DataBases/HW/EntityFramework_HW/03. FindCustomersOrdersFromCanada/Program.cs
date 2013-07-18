using System;
using System.Collections.Generic;
using System.Linq;
using Northwind.Data;

namespace _03.FindCustomersOrdersFromCanada
{
    class Program
    {
        static void Main()
        {
            NorthwindEntities db = new NorthwindEntities();
            using (db)
            {
                var customers = db.Orders.Where(x => x.ShippedDate.Value.Year == 1997 && x.ShipCountry == "Canada")
                    .Join(db.Customers, o => o.CustomerID, c => c.CustomerID, (o, c) => new
                {
                    shipCountry = o.ShipCountry,
                    shipYear = o.ShippedDate.Value.Year,
                    customerName = c.ContactName
                });
                Console.WriteLine("CustomerName | ShipedCountry | ShipedYear");

                foreach (var customer in customers)
                {
                    Console.WriteLine(customer.customerName + " | " + customer.shipCountry + " | "+ customer.shipYear);
                }
                
            }

        }
    }
}
