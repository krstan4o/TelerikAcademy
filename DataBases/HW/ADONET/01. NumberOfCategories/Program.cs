using _01.NumberOfCategories;
using System;
using System.Data.SqlClient;

namespace _01.NumberOfCategories
{
    class Program
    {
        static void Main()
        {
            SqlConnection con = new SqlConnection(Settings.Default.NortwindConStr);
            con.Open();
            using (con)
            {
                SqlCommand comand = new SqlCommand("SELECT COUNT(*) FROM Categories", con);
                int numberOfRows = (int)comand.ExecuteScalar();
                Console.WriteLine("The number of rows in Categories is: {0}", numberOfRows);
            }
        }
    }
}
