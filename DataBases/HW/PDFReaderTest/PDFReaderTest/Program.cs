using System;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using iTextSharp.text;
using System.IO;
using System.Collections.Generic;

namespace PDFReaderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<DateTime, List<DayReport>> salesByDate = GetReportsByDate();
            CreatePDFReport(salesByDate);
        }

        private static Dictionary<DateTime, List<DayReport>> GetReportsByDate()
        {
            Dictionary<DateTime, List<DayReport>> salesByDate = new Dictionary<DateTime, List<DayReport>>();

            SqlConnection con = new SqlConnection("Database=.;Initial Catalog=SupermarketDatabase;Integrated Security=true");
            con.Open();
            using (con)
            {
                SqlCommand com = new SqlCommand("SELECT p.ProductName, CONVERT(nvarchar,s.Qantity) + ' ' + ms.MeasureName AS Quantity, " +
                                                "s.UnitPrice, m.Name, s.Sum, s.Data " +
                                                "FROM Sales s " +
                                                "JOIN Products p " +
                                                "ON(s.ProductId = p.Id) " +
                                                "JOIN Measures ms " +
                                                "ON(p.MeasureId = ms.Id) " +
                                                "JOIN Markets m " +
                                                "ON (s.MarketId = m.Id)" ,
                    con);
                SqlDataReader reader = com.ExecuteReader();

                double totalSum = 0;

                while (reader.Read())
                {
                    string productName = (string)reader[0];
                    string quantity = (string)reader[1];
                    double price = (double)reader[2];
                    string marketName = (string)reader[3];
                    double sum = (double)reader[4];
                    DateTime date = (DateTime)reader[5];
                    DateTime shortDate = date.Date;

                    if (salesByDate.ContainsKey(shortDate))
                    {
                        salesByDate[shortDate].Add(new DayReport(productName,quantity,price,marketName,sum));
                    }
                    else
                    {
                        salesByDate.Add(shortDate, new List<DayReport>());
                        salesByDate[shortDate].Add(new DayReport(productName, quantity, price, marketName, sum));
                    }
                    totalSum += sum;
                }
            }
            return salesByDate;
        }

        private static void CreatePDFReport(Dictionary<DateTime, List<DayReport>> sales)
        {
            //creating the document and setting the path
            var doc = new Document();
            doc.AddTitle("PDF Sales Report");
            string path = "../../PDFReports";
            PdfWriter.GetInstance(doc, new FileStream(path + "\\report.pdf", FileMode.Create));
            PdfPTable table = new PdfPTable(5);

            PdfPCell cell = new PdfPCell();
            PdfPCell firstCell = new PdfPCell();
            firstCell.PaddingLeft = 133;
            firstCell.Phrase = new Phrase("Aggregated Sales Report");
            firstCell.Colspan = 5;
            firstCell.BackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.Aquamarine);
            table.AddCell(firstCell);
            firstCell.PaddingLeft = 2;
            table.CompleteRow();

            foreach (var saleByDate in sales)
            {
                firstCell.Phrase = new Phrase("Date: " + saleByDate.Key.ToShortDateString());
                table.AddCell(firstCell);
                table.CompleteRow();
                cell.BackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.Aquamarine);
                Font boldFont = new Font();
                boldFont.Size = 18;
                cell.Phrase = new Phrase("Product", boldFont);
                table.AddCell(cell);
                cell.Phrase = new Phrase("Quantity", boldFont);
                table.AddCell(cell);
                cell.Phrase = new Phrase("Unit Price", boldFont);
                table.AddCell(cell);
                cell.Phrase = new Phrase("Location", boldFont);
                table.AddCell(cell);
                cell.Phrase = new Phrase("Sum", boldFont);
                table.AddCell(cell);
                cell.BackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
                var reportsByDay = sales[saleByDate.Key];
                double totalSum = 0;
                foreach (var item in reportsByDay)
                {
                    cell.Phrase = new Phrase(item.ProductName);
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(item.Quantity);
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(item.UnitPrice.ToString());
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(item.MarketName);
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(item.Sum.ToString());
                    table.AddCell(cell);
                    totalSum += item.Sum;
                }
                PdfPCell finalCell = new PdfPCell();
                finalCell.Colspan = 4;
                finalCell.Phrase = new Phrase("Total sum for " + saleByDate.Key.ToShortDateString() + ":");
                finalCell.PaddingLeft = 200;
                table.AddCell(finalCell);
                finalCell.Colspan = 1;
                finalCell.PaddingLeft = 1;
                boldFont.Size = 15;
                finalCell.Phrase = new Phrase(totalSum.ToString(),boldFont);
                table.AddCell(finalCell);
            }
                
            doc.Open();
            doc.Add(table);
            doc.Close();
        }
    }
}
