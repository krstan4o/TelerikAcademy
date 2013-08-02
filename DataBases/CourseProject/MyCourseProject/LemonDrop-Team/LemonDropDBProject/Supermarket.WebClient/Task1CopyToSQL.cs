using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Supermarket.Data;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.IO.Compression;
using System.Globalization;

namespace Supermarket.WebClient
{
    public class Task1CopyToSQL
    {
        public static void CopyMysqlToSql()
        {
            DeleteData();
            CopyVendors();
            CopyMesures();
            CopyProducts();
        }
        private static void CopyVendors()
        {
            string query = "SELECT * FROM vendors";
            MySqlProvider.ExecuteSqlQueryReturnValue(query, null, delegate(MySqlDataReader reader)
            {
                while (reader.Read())
                {
                    string sqlQuery = "INSERT INTO Vendors";

                    Dictionary<string, object> parametters = new Dictionary<string, object>();
                    parametters.Add("Id", reader.GetValue(0));
                    parametters.Add("VendorName", reader.GetValue(1));

                    SqlProvider.ExecuteSqlQueryInsert(sqlQuery, parametters, null);
                }
            });
        }

        private static void CopyMesures()
        {
            string query = "SELECT * FROM measures";
            MySqlProvider.ExecuteSqlQueryReturnValue(query, null, delegate(MySqlDataReader reader)
            {
                while (reader.Read())
                {
                    string sqlQuery = "INSERT INTO Measures";

                    Dictionary<string, object> parametters = new Dictionary<string, object>();
                    parametters.Add("Id", reader.GetValue(0));
                    parametters.Add("MeasureName", reader.GetValue(1));

                    SqlProvider.ExecuteSqlQueryInsert(sqlQuery, parametters, null);
                }
            });
        }

        private static void CopyProducts()
        {
            string query = "SELECT * FROM products";
            MySqlProvider.ExecuteSqlQueryReturnValue(query, null, delegate(MySqlDataReader reader)
            {
                while (reader.Read())
                {
                    string sqlQuery = "INSERT INTO Products";

                    Dictionary<string, object> parametters = new Dictionary<string, object>();
                    parametters.Add("Id", reader.GetValue(0));
                    parametters.Add("ProductName", reader.GetValue(1));
                    parametters.Add("VendorId", reader.GetValue(3));
                    parametters.Add("MeasureId", reader.GetValue(2));
                    parametters.Add("BasePrise", reader.GetValue(4));

                    SqlProvider.ExecuteSqlQueryInsert(sqlQuery, parametters, null);
                }
            });
        }
        private static void DeleteData()
        {
            string query = "DELETE FROM Sales";
            SqlProvider.ExecuteSqlQueryDelete(query);
            query = "DELETE FROM Products";
            SqlProvider.ExecuteSqlQueryDelete(query);
            query = "DELETE FROM Measures";
            SqlProvider.ExecuteSqlQueryDelete(query);
            query = "DELETE FROM Vendors";
            SqlProvider.ExecuteSqlQueryDelete(query);
        }

        // Excel
        public static void ReadFromExcel(string tempDir, string zipPath)
        {
            DeleteSalesData();

            UnzipExcelFiles(tempDir, zipPath);
            AddDataFromExcelToSQLDB(tempDir);
            Directory.Delete(tempDir, true);
        }

        private static void UnzipExcelFiles(string tempDir, string zipPath)
        {

            //string extractPath = @"D:\Databases-Teamwork-Practical-Project\result";
            //ZipFile.ExtractToDirectory(zipPath, extractPath);

            if (Directory.Exists(tempDir))
            {
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

        private static void DeleteSalesData()
        {
            string query = "DELETE FROM Sales";
            SqlProvider.ExecuteSqlQueryDelete(query);
        }
    }
}