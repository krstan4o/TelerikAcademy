using Supermarket.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Supermarket.WebClient
{
    public class Task3GenerateXML
    {
        public static void GenerateXMLSalesRaport(string path)
        {
        using(SupermarketDatabaseEntities db = new SupermarketDatabaseEntities())
            {   
                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                XElement sales = new XElement("Sales");
                doc.Add(sales);
                foreach (var vendor in db.sales.GroupBy(x =>x.Product.Vendor.VendorName))
                {
                    string vendorName = vendor.First().Product.Vendor.VendorName;

                    XElement root = new XElement("Sale");
                    root.Add(new XAttribute("vendor", vendorName));
                    
                    foreach (var peni in vendor.GroupBy(x=>x.Data))
                    {
                        double total = 0.0;
                        //total = peni.First().Sum;
                        foreach (var item in peni)
                        {
                            total += item.Sum;
                        }
                        XElement summary = new XElement("summary");
                        string data =  peni.First().Data.ToString();
                        summary.Add(new XAttribute("date", data));
                        summary.Add(new XAttribute("total-sum", total.ToString()));
                        root.Add(summary);
                        total = 0.0;

                    }
                    sales.Add(root);

                }
                doc.Save(path);  
            }
        }
    }
}