using _01.NumberOfCategories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.RetriveAllCategories
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(Settings.Default.NortwindConStr);
            con.Open();
            using (con)
            {
                SqlCommand comand = new SqlCommand("SELECT CategoryName, [Description]" +
                    "FROM Categories", con);
                SqlDataReader reader = comand.ExecuteReader();
                Console.WriteLine("CategoryName   CategoryDescription\n");
                using (reader)
                {
                    while (reader.Read())
                    {
                        string name = (string)reader[0];
                        string desc = (string)reader[0];
                        Console.WriteLine(name + " => " + desc);
                    }
                }
            }
        }
    }
}
