﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Supermarket.Data;

namespace LoadVendorExpenses
{
    class Program
    {
        static void Main()
        {
            XmlTextReader reader = new XmlTextReader("../../../VendorExpenses.xml");
            List<VendorExpenses> newListVendorExpenses = new List<VendorExpenses>();
            string vendor = "";
            string data = "";
            double expenses = 1.1d;
            while (reader.Read())
            {            
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        while (reader.MoveToNextAttribute())
                        { // Read the attributes.
                            if (reader.Name == "vendor")
                            {
                                vendor = reader.Value; 
                           //     Console.WriteLine(vendor);
                            }
                            else
                            {
                                data = reader.Value;
                          //      Console.WriteLine(data);
                            }                 
                        }
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        expenses =float.Parse(reader.Value);
                      //  Console.WriteLine(expenses);
                        VendorExpenses newVendor = new VendorExpenses();
                        newVendor.Vendor_name = vendor;
                        newVendor.Date= data;
                        newVendor.Expenses = (double)expenses;
                        newListVendorExpenses.Add(newVendor);
                        break;      
                }             
            }

            string sqlQueryCreateTableOne = "use SupermarketDatabase ";
            string sqlQueryCreateTableTwo = "IF OBJECT_ID('dbo.VendorExpenses', 'U') IS NOT NULL " +
                "DROP TABLE dbo.VendorExpenses " +
                "CREATE TABLE [dbo].[VendorExpenses]( " +
                "[Id] [int] IDENTITY(1,1) NOT NULL, " +
                "[VendorName] [nvarchar](max) NULL, " +
                "[Data] [nvarchar](max) NULL, " +
                "[Expenses] [float]NULL, " +
                "CONSTRAINT [PK_dbo.Courses] PRIMARY KEY CLUSTERED " +
                "([Id] ASC) " +
                "WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) " +
                "ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ";
            SqlProvider.ExecuteSqlQueryReturnValue(sqlQueryCreateTableOne,null,null);
            SqlProvider.ExecuteSqlQueryReturnValue(sqlQueryCreateTableTwo,null,null);
           // SqlProvider.ExecuteSqlQueryReturnValue(sqlQueryCreateTable, null, null);


            // load and delete data
            var expensesList = MongoDbProvider.db.LoadData<VendorExpenses>().ToList();
            foreach (var item in expensesList)
            {
                MongoDbProvider.db.DeleteData<VendorExpenses>(item._id);
            }

            string sqlQuery = "INSERT INTO VendorExpenses ";
            foreach (var expen in newListVendorExpenses)
            {
                MongoDbProvider.db.SaveData(expen);

                Dictionary<string, object> parametters = new Dictionary<string, object>();
                // parametters.Add("Id", expen._id);
                parametters.Add("VendorName", expen.Vendor_name);
                parametters.Add("Data", expen.Date);
                parametters.Add("Expenses", expen.Expenses);

                SqlProvider.ExecuteSqlQueryInsert(sqlQuery, parametters, null);
                
                //Console.WriteLine("{0} {1} {2}",expen.Vendor_name,expen.Date, expen.Expenses);
            }
        }
    }
}