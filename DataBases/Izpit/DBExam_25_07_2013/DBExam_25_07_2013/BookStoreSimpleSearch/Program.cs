using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BookStore.Data;
    static class Program
    {
        static void Main()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../simple-query.xml");
            string title = xmlDoc.GetChildText("/query/title");
            string isbn = xmlDoc.GetChildText("/query/isbn");
            string author = xmlDoc.GetChildText("/query/author");

           BookStoreDAL.FindBook(title, isbn, author);


          
        }
        private static string GetChildText(
        this XmlNode node, string xpath)
        {
            XmlNode childNode = node.SelectSingleNode(xpath);
            if (childNode == null)
            {
                return null;
            }
            return childNode.InnerText.Trim();
        }
    }

