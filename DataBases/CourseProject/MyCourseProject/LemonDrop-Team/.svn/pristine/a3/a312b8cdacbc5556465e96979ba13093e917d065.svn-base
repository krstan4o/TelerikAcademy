using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace VendorsTotalReport.Client
{    
    public class VendorsTotalReport
    {
        static void Main()
        {
            List<ProductRaport> productRaports = MongoDbProvider.db.LoadData<ProductRaport>().ToList();
            List<Taxes> taxes =new List<Taxes>();
            string query = "SELECT * FROM Taxes";
            SqLiteProvider.ExecuteSqlQueryReturnValue(query, null, delegate(SQLiteDataReader reader)
            {
                while (reader.Read())
                {
                   taxes.Add(new Taxes()
                    {
                        productName = reader.GetValue(1).ToString(),
                        tax = int.Parse(new string(reader.GetValue(2).ToString().Trim().TakeWhile(c => char.IsDigit(c)).ToArray()))

                    });
                }
            });
           using (SQLiteConnection oConn = new SQLiteConnection(dbSettings.Default.SQLiteConnectionString))
           {
               oConn.Open();

               string deleteTable = "drop table if exists Products";
               SQLiteCommand command = new SQLiteCommand(deleteTable, oConn);
               command.ExecuteNonQuery();

               string sql = "create table Products (ProductId int, ProductName varchar(50),VendorName varchar(50),TotalQuantitySold double,TotalIncomes double,Tax double)";
               command = new SQLiteCommand(sql, oConn);
               command.ExecuteNonQuery();
                 }
               foreach (var product in productRaports)
               {
                   Dictionary<string, object> parametters = new Dictionary<string, object>();
                   parametters.Add("ProductId", product.ProductId);
                   parametters.Add("ProductName", product.ProductName);
                   parametters.Add("VendorName", product.Vendor_name);
                   parametters.Add("TotalQuantitySold", product.TotalQuantitySold);
                   parametters.Add("TotalIncomes", product.TotalIncomes);
                   double totalTax=0;
                   foreach (var p in taxes)
                   {
                       if (p.productName == product.ProductName)
                       {
                           totalTax = ((p.tax*1.0) / 100)*product.TotalIncomes;
                           break;
                       }
                   }
                   parametters.Add("Tax", totalTax);
                   string sqlQuery = "INSERT INTO Products";

                   SqLiteProvider.ExecuteSqlQueryInsert(sqlQuery, parametters, null);
               }


            List<VendorReport> vendors=new List<VendorReport>();
            string queryVendors = "SELECT * FROM Vendors";
            SqlProvider.ExecuteSqlQueryReturnValue(queryVendors, null, delegate(SqlDataReader reader)
            {
                while (reader.Read())
                {
                    string vendorName = reader.GetValue(1).ToString();
                    Dictionary<string, object> parametters = new Dictionary<string, object>();
                    parametters.Add("VendorName", vendorName);
                    string queryTotals = "SELECT * FROM Products WHERE VendorName=@VendorName";
                    double incomesTotal = 0;
                    double taxesTotal = 0;
                    SqLiteProvider.ExecuteSqlQueryReturnValue(queryTotals, parametters, delegate(SQLiteDataReader readerTotal)
                    {
                        while (readerTotal.Read())
                        {
                            incomesTotal = incomesTotal+(double)readerTotal.GetValue(4);
                            taxesTotal = taxesTotal + (double)readerTotal.GetValue(5);
                        }
                    });

                    string queryExpenses = "SELECT * FROM VendorExpenses WHERE VendorName=@VendorName";
                    double expenses = 0;
                    SqlProvider.ExecuteSqlQueryReturnValue(queryExpenses, parametters, delegate(SqlDataReader readerExpenses)
                    {
                        while (readerExpenses.Read())
                        {
                            expenses =expenses+ (double)readerExpenses.GetValue(3);
                        }
                    });

                    vendors.Add(new VendorReport()
                    {
                        VendorName=vendorName,
                        Incomes=incomesTotal,
                        Expenses=expenses,
                        Taxes=taxesTotal
                    });
                }
            });
            CreateExcelTotalReport(vendors);
        }

        private static void CreateExcelTotalReport(List<VendorReport> productRaports)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
           @"Data Source=Products-Total-Report.xlsx; Extended Properties=""Excel 12.0 Xml;HDR=YES""";

            OleDbConnection dbCon = new OleDbConnection(connectionString);

            CreateTable(dbCon);
            foreach (var pr in productRaports)
	        {
                InsertRow(pr, dbCon);                
            }
        }

        private static void InsertRow(VendorReport pr, OleDbConnection dbCon)
        {
            dbCon.Open();
            OleDbCommand cmd = new OleDbCommand(
                "INSERT INTO [Sheet1$] ([Vendor], [Incomes], [Expenses], [Taxes], [Financial Result]) VALUES (@VendorName, @Incomes,@Expenses,@Taxes,@Result)" //add columns
                , dbCon);
            cmd.Parameters.AddWithValue("@VendorName", pr.VendorName);
            cmd.Parameters.AddWithValue("@VendorName", pr.Incomes);
            cmd.Parameters.AddWithValue("@VendorName", pr.Expenses);
            cmd.Parameters.AddWithValue("@VendorName", pr.Taxes);
            cmd.Parameters.AddWithValue("@VendorName", pr.Incomes-pr.Taxes-pr.Expenses);
            cmd.ExecuteNonQuery();
            dbCon.Close();
        }

        private static void CreateTable(OleDbConnection dbCon)
        {
            dbCon.Open();
            OleDbCommand cmd = new OleDbCommand(
                "CREATE TABLE [Sheet1] ([Vendor] nvarchar(255), [Incomes] number, [Expenses] number, [Taxes] number, [Financial Result] number)"
                , dbCon);
            cmd.ExecuteNonQuery();
            dbCon.Close();
        }
    }
}
