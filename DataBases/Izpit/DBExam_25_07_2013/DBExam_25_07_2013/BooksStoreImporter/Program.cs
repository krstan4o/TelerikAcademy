using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BookStore.Data;

static class Program
{
   
    static void Main()
    {
        // Task 3
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("../../simple-books.xml");
        string xPathQuery = "/catalog/book";

        XmlNodeList bookList = xmlDoc.SelectNodes(xPathQuery);
        foreach (XmlNode bookmarkNode in bookList)
        {
            string authorName = bookmarkNode.GetChildText("author");
            string title = bookmarkNode.GetChildText("title");
            string webSite = bookmarkNode.GetChildText("web-site");
            decimal price;
            decimal.TryParse(bookmarkNode.GetChildText("price"),out price);
            string isbn = bookmarkNode.GetChildText("isbn");
            BookStoreDAL.AddBook(authorName, title, webSite, price, isbn);
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
