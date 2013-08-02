using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Compression;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Supermarket.Data;
using System.Data.SqlClient;



namespace ReadFromExcel.Client
{
    class ReadFromExcel
    {
        static void Main()
        {
            DeleteData();

            string tempDir = @"tempDir";
            UnzipExcelFiles(tempDir);
            AddDataFromExcelToSQLDB(tempDir);
            Directory.Delete(tempDir, true);
        }

        private static void UnzipExcelFiles(string tempDir)
        {
            string zipPath = @"..\..\..\Sample-Sales-Reports.zip";

            //string extractPath = @"D:\Databases-Teamwork-Practical-Project\result";
            //ZipFile.ExtractToDirectory(zipPath, extractPath);
            
            if(Directory.Exists(tempDir)){
                Directory.Delete(tempDir, true);
            }
            ZipFile.ExtractToDirectory(zipPath, tempDir);
        }

        private static void AddDataFromExcelToSQLDB(string tempDir)
        {
            DataSet sheet1 = new DataSet();
            OleDbConnectionStringBuilder csbuilder = new OleDbConnectionStringBuilder();
            csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csbuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=No");

            DirectoryInfo dirs = new DirectoryInfo(tempDir);
            foreach (var dir in dirs.GetDirectories())
            {
                string dateString = dir.ToString();
                DateTime date = DateTime.ParseExact(dateString, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                foreach (var file in dir.GetFiles())
                {
                    string[] data = file.ToString().Split(new string[] { "-Sales" }, StringSplitOptions.RemoveEmptyEntries);
                    csbuilder.DataSource = (tempDir + "//" + dateString + "//" + file.ToString());
                    using (OleDbConnection connection = new OleDbConnection(csbuilder.ConnectionString))
                    {
                        connection.Open();
                        string selectSql = @"SELECT * FROM [Sales$]";
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectSql, connection))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow item in dt.Rows)
                            {
                                double num;
                                if (double.TryParse(item.ItemArray[0].ToString(), out num))
                                {
                                    string sqlQuery = "INSERT INTO Sales";

                                    Dictionary<string, object> parametters = new Dictionary<string, object>();
                                    parametters.Add("ProductId", item.ItemArray[0]);
                                    parametters.Add("Qantity", item.ItemArray[1]);
                                    parametters.Add("UnitPrice", item.ItemArray[2]);
                                    parametters.Add("Sum", Math.Round(Convert.ToDouble(item.ItemArray[3]), 2));

                                    parametters.Add("MarketId", getMarketId(data[0]));
                                    parametters.Add("Data", date.Date);

                                    SqlProvider.ExecuteSqlQueryInsert(sqlQuery, parametters, null);
                                }
                            }
                        }
                        connection.Close();
                    }
                }
            }
        }

        private static object getMarketId(string str)
        {
            string query = "SELECT * FROM Markets WHERE Name = @Name";

            Dictionary<string, object> parametters = new Dictionary<string, object>();
            parametters.Add("Name", str);
            object id = null;

            SqlProvider.ExecuteSqlQueryReturnValue(query, parametters, delegate(SqlDataReader reader)
            {
                while (reader.Read())
                {
                    id = reader["Id"];
                }
            });

            if (id == null)
            {
                string sqlQuery = "INSERT INTO Markets";

                Dictionary<string, object> insertParametters = new Dictionary<string, object>();
                insertParametters.Add("Name", str);

                SqlProvider.ExecuteSqlQueryInsert(sqlQuery, insertParametters, null);

                id = getMarketId(str);
            }

            return id;
        }

        private static void DeleteData()
        {
            string query = "DELETE FROM Sales";
            SqlProvider.ExecuteSqlQueryDelete(query);
        }
    }
}
