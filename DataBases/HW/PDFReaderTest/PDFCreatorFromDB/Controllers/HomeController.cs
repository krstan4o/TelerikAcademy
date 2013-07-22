using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PDFCreatorFromDB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            var doc = new Document();
            doc.AddTitle("PDF Sales Report");
            string path = "../../PDFReports";
            
            PdfWriter.GetInstance(doc, new FileStream(path + "/Doc.pdf", FileMode.Create));

            
            PdfPTable table = new PdfPTable(5);
            PdfPCell cell = new PdfPCell();
            PdfPCell firstCell = new PdfPCell();
            firstCell.Phrase = new Phrase("Aggregated Sales Report");
            firstCell.Colspan = 5;
            
                       
            table.AddCell(firstCell);
            table.CompleteRow();

            cell.Phrase = new Phrase("Product");
            table.AddCell(cell);

            cell.Phrase = new Phrase("Quantity");
            table.AddCell(cell);

            cell.Phrase = new Phrase("Unit Price");
            table.AddCell(cell);

            cell.Phrase = new Phrase("Location");
            table.AddCell(cell);

            cell.Phrase = new Phrase("Sum");
            table.AddCell(cell);



            /////////////////
            SqlConnection con = new SqlConnection("Database=.;Initial Catalog=Supermarket;Integrated Security=true");
            con.Open();
            using (con)
            {
                SqlCommand com = new SqlCommand("SELECT * FROM Productss", con);
                SqlDataReader reader = com.ExecuteReader();

                decimal totalSum = 0;

                while (reader.Read())
                {

                    string productName = (string)reader[1];
                    string quantity = (string)reader[2];
                    decimal price = (decimal)reader[3];
                    string location = (string)reader[4];
                    decimal sum = (decimal)reader[5];
                    totalSum += sum;

                    cell.Phrase = new Phrase(productName);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase(quantity);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase(price.ToString());
                    table.AddCell(cell);

                    cell.Phrase = new Phrase(location);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase(sum.ToString());
                    table.AddCell(cell);
                }
                var rows = table.GetRows(0, 4);

                for (int i = 3; i < rows.Count; i++)
                {
                    var row = table.GetRow(i);
                    var cols = row.GetCells();
                    foreach (var col in cols)
                    {
                        Console.Write(col.Phrase.Content + " ");
                    }
                    Console.WriteLine();
                }
                
            }
            doc.Open();
            doc.Add(table);
            doc.Close();

            return Redirect("http://localhost:20144/pdfreports/doc.pdf");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
