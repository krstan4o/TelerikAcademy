using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Xml;
using Microsoft.Office.Interop.Excel;


namespace ZipReading
{
    class Program
    {
        static void Main(string[] args)
        {
            string zipPath = @"D:\Databases-Teamwork-Practical-Project\Sample-Sales-Reports.zip";
            //string extractPath = @"D:\Databases-Teamwork-Practical-Project\result";
            //ZipFile.ExtractToDirectory(zipPath, extractPath);

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".xls"))
                    {
                        Application app = new Application();
                        Workbook wb = app.Workbooks.Open(entry.FullName);

                        Worksheet sheet = (Worksheet)wb.Sheets[0];

                        Range excelRange = sheet.UsedRange;

                        foreach (Microsoft.Office.Interop.Excel.Range row in excelRange.Rows)
                        {
                            int rowNumber = row.Row;
                            Console.WriteLine(rowNumber);
                        }
                    }
                }
            }
        }
    }
}
