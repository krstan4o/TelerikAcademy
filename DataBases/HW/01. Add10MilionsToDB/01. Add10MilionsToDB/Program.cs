using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace _01.Add10MilionsToDB
{
    class Program
    {
        static void Main()
        {
            // use sql script in the folder of the project for creating the database
            SqlConnection con = new SqlConnection("Server=.;Database=Performance;Integrated Security=true");
            con.Open();
            using (con)
            {
              
                for (long i = 0; i < 10000000; i++)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO DateText " +
              "VALUES(" + DateTime.Now.ToString() + ", " + "SOME FUCKINGGGGGGGGGG TEXTTTTTT" + ")"
              , con);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
