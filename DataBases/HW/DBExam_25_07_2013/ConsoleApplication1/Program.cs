using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BookStore.Data;
static class Program
{
    static void Main()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("../../simple-books.xml");
        string xPathQuery = "/catalog/book";

        XmlNodeList bookmarksList = xmlDoc.SelectNodes(xPathQuery);
        foreach (XmlNode bookmarkNode in bookmarksList)
        {
            string author = bookmarkNode.GetChildText("author");
            string title = bookmarkNode.GetChildText("title");
          
            string website = bookmarkNode.GetChildText("web-site");
            
            string isbn = bookmarkNode.GetChildText("isbn");
            string price = bookmarkNode.GetChildText("price");
            BookStoreDAL.AddBook(author, title, website, isbn, price);
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