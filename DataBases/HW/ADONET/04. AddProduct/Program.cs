using _01.NumberOfCategories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.AddProduct
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0} products inserted.", AddProduct("4USHKI", 1, 1, "10 x 50kg", (decimal)2.50, 5, 2, 1, true));
        }
        static int AddProduct(string name, int supplierId, int categoryId, string quantityPerUnit,
            decimal unitPrice, int unitsInStock, int unitsOnOrder, int reorderLevel, bool discontinued) 
        {

            SqlConnection con = new SqlConnection(Settings.Default.NortwindConStr);
            con.Open();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Products" + 
                    "(ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued) "+
                    "VALUES(@name, @suplier, @cat, @quantity, @price, @unitsInStock, @unitsOnOrder, @reorderLevel, @discontinued)", con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@suplier", supplierId);
                cmd.Parameters.AddWithValue("@cat", categoryId);
                cmd.Parameters.AddWithValue("@quantity", quantityPerUnit);
                cmd.Parameters.AddWithValue("@price", unitPrice);
                cmd.Parameters.AddWithValue("@unitsInStock", unitsInStock);
                cmd.Parameters.AddWithValue("@unitsOnOrder", unitsOnOrder);
                cmd.Parameters.AddWithValue("@reorderLevel", reorderLevel);
                cmd.Parameters.AddWithValue("@discontinued", discontinued);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
           
        }
    }
}
