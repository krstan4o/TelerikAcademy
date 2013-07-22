using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.EntityModel;
using System.Xml.Linq;
using System.Xml;

namespace Supermarket.GenerateSalesReport
{
    class Program
    {
        static void Main()
        {
            using(SupermarketDatabaseEntities db = new SupermarketDatabaseEntities())
            {
               // XDocument miXML = new XDocument(
               //new XDeclaration("1.0", "utf-8", "yes"),

               //new XElement("Sales",
               //                    new XElement("sale",
               //                        new XAttribute("vendor", "VENDOR NAME"),
               //                        new XElement("summary",
               //                            new XAttribute("date", "Data"),
               //                            new XAttribute("total-sum", "Sum"))
               //                            )
               //                            )
               //                           );

                string path  = "C:\\Users\\User\\Desktop\\LemonSecond";
                XDocument doc = new XDocument();
                XElement sales = new XElement("Sales");
                doc.Add(sales);
                foreach (var vendor in db.sales.GroupBy(x =>x.Products.Vendors.Vendor_Name))
                {
                    string vendorName = vendor.First().Products.Vendors.Vendor_Name;

                    XElement root = new XElement("Sale");
                    root.Add(new XAttribute("vendor", vendorName));
                    
                    foreach (var peni in vendor.GroupBy(x=>x.Data))
                    {
                        double total = 0.0;
                        foreach (var item in peni)
	                        {
		                       total += item.Sum;
	                        }
                        XElement summary = new XElement("summary");
                        string data =  peni.First().Data.ToString();
                        summary.Add(new XAttribute("date", data));
                        summary.Add(new XAttribute("total-sum", total.ToString()));
                    }
                    sales.Add(root);

                }
                //doc.Element("Sales").Add(root);
                doc.Save(path);  

                //XmlDocument doc = new XmlDocument();
                //XmlNode docNode = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                //doc.AppendChild(docNode);
              
                //XmlElement root = doc.CreateElement("Sales");  
                //doc.InsertBefore(docNode, doc.DocumentElement);
                //doc.AppendChild(root);
                //XmlNode productsNode = doc.CreateElement("Sale");
            }
        }
    }
}
