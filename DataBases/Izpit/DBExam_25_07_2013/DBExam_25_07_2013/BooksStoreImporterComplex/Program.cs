using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using System.Xml;
namespace BooksStoreImporterComplex
{
    static class Program
    {
        static void Main()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../complex-books.xml");
            string xPathQuery = "/catalog/book";

            XmlNodeList bookList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode bookmarkNode in bookList)
            {
                string title = bookmarkNode.GetChildText("title");
                string webSite = bookmarkNode.GetChildText("web-site");
                decimal price;
                decimal.TryParse(bookmarkNode.GetChildText("price"), out price);
                string isbn = bookmarkNode.GetChildText("isbn");
                
                XmlNodeList authors = bookmarkNode.SelectNodes("authors/author");
                XmlNodeList reviews = bookmarkNode.SelectNodes("reviews/review");


                BookStoreDAL.AddBook(authors, reviews, title, webSite, price, isbn);
            }
        }
        private static string GetChildText(this XmlNode node, string tagName)
        {
            XmlNode childNode = node.SelectSingleNode(tagName);
            if (childNode == null)
            {
                return null;
            }
            return childNode.InnerText.Trim();
        }
    }
}
