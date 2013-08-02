using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Data;
using System.Xml;
static class Program
{
    static void Main()
    {
        // no time :(
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("../../simple-query.xml");
        string xPathQuery = "/review-queries/query";

            XmlNodeList bookList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode book in bookList)
            {
               var atribute = book.Attributes["type"];
               if (atribute.Value == "by-author")
               {
                  string authorName = book.GetChildText("author-name");
               }
               else if (atribute.Value == "by-period")
               {
                  string startDate = book.GetChildText("start-date");
                  string endDate = book.GetChildText("end-date");
               }
            }
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

