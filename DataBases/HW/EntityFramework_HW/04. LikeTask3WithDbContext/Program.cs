using System;
using System.Linq;
using System.Data.SqlClient;

namespace _04.LikeTask3WithDbContext
{
    class Program
    {
        static void Main()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True;");
            con.Open();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT c.ContactName, o.ShipCountry, o.ShippedDate "+
                        "FROM Orders o "+
                        "JOIN Customers c "+
                        "ON(o.CustomerID = c.CustomerID) "+
                        "WHERE(DATEPART(year, o.ShippedDate)='1997') AND o.ShipCountry = 'Canada'",con);
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("CustomerName | ShipedCountry | ShipedYear");
                while (reader.Read())
                {
                    string contactName = (string)reader[0];
                    string shipCountry = (string)reader[1];
                    DateTime shipDate = (DateTime)reader[2];
                    Console.WriteLine(contactName + " | " + shipCountry + " | " + shipDate.Year);
                }
            }
            
        }
    }
}
