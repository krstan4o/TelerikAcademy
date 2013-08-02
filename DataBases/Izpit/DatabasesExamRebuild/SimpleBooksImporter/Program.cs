using System;
using System.Collections.Generic;
using System.Linq;
using Bookstore.Model;
using System.Xml;
using Bookstore.Data;
namespace SimpleBooksImporter
{
    static class Program
    {
        static void Main()
        {
            // Import the books from simple-books.xml file into the database.
            BookstoreDb db = new BookstoreDb();
            using (db)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("../../simple-books.xml");
                var books = doc.SelectNodes("catalog/book");

                foreach (XmlNode book in books)
                {
                   string authorName = book.GetChildText("author");
                   if (authorName==null)
                   {
                       throw new ArgumentNullException("Book must have an author.");
                   }
                   string bookTitle = book.GetChildText("title");
                   if (bookTitle == null)
                   {
                       throw new ArgumentNullException("Book must have an title.");
                   }

                   decimal bookPrice;
                   decimal.TryParse(book.GetChildText("price"), out bookPrice);
                   string bookIsbn = book.GetChildText("isbn");
                   string bookWebsite = book.GetChildText("web-site");
                    // check if author exist in the db
                   Author author = db.Authors.FirstOrDefault(x => x.AuthorName == authorName);
                   if (author == null)
                   {
                       author = new Author();
                       author.AuthorName = authorName;
                       db.Authors.Add(author);
                      
                   }

                   Book bookToSave = new Book();
                   bookToSave.BookTitle = bookTitle;
                   bookToSave.Authors.Add(author);
                   bookToSave.ISBN = bookIsbn;
                   bookToSave.Price = bookPrice;
                   bookToSave.Website = bookWebsite;

                   db.Books.Add(bookToSave);
                   db.SaveChanges();
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
}
