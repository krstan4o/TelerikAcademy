using _01.NumberOfCategories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.ProductsAndCategoryes
{
    class Program
    {
        static void Main()
        {
            SqlConnection con = new SqlConnection(Settings.Default.NortwindConStr);
            con.Open();
            using (con)
            {
                SqlCommand comand = new SqlCommand("SELECT ProductName, CategoryName" +
                            " FROM Products p JOIN Categories c" +
                            " ON (p.CategoryID = c.CategoryID)" +
                            " ORDER BY CategoryName", con);
               
                SqlDataReader reader = comand.ExecuteReader();
                Console.WriteLine("Product   Category\n");
                using (reader)
                {
                    while (reader.Read())
                    {
                        string product = (string)reader[0];
                        string categ = (string)reader[0];
                        Console.WriteLine(product + " => " + categ);
                    }
                }
            }
        }
    }
}
