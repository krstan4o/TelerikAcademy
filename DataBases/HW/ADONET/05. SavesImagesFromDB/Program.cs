using _01.NumberOfCategories;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
namespace _05.SavesImagesFromDB
{
    class Program
    {
        static void Main()
        {
            SqlConnection con = new SqlConnection(Settings.Default.NortwindConStr);
            con.Open();
            using (con)
            {
                Console.WriteLine("Retriving images from DB and saving to computer...");
                SqlCommand comand = new SqlCommand("SELECT Picture, CategoryId FROM Categories", con);
                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    byte[] photo = (byte[])reader[0];
                    MemoryStream ms = new MemoryStream(photo);
                    int id = (int)reader[1];
                       
                    ms.Close();
                    ImageConverter imgConverter = new ImageConverter();

                    Image img = imgConverter.ConvertFrom(photo) as Image;
                    
                    
                    img.Save(id + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);


                  
                 
                }
                Console.WriteLine("Done.");
            }
        }
    }
}
