using System;
using System.Collections.Generic;
using System.Linq;
using Bookstore.Data;
using Bookstore.Model;
using System.Xml;
using System.Globalization;
using System.Transactions;

namespace ComplexBooksImporter
{
    static class Program
    {
        static void Main()
        {
            BookstoreDb db = new BookstoreDb();

            using (db)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("../../complex-books.xml");
                var books = doc.SelectNodes("catalog/book");

                foreach (XmlNode book in books)
                {
                    //TransactionScope tran = new TransactionScope(
                    //    TransactionScopeOption.Required,
                    //    new TransactionOptions()
                    //    {
                    //        IsolationLevel = IsolationLevel.RepeatableRead
                    //    });
                    //tran.Complete();
                    string bookTitle = book.GetChildText("title");
                    if (bookTitle == null)
                    {
                        throw new ArgumentNullException("Book must have an title.");
                    }
                    string bookIsbn = book.GetChildText("isbn");
                    string bookWebsite = book.GetChildText("web-site");
                    decimal bookPrice;
                    decimal.TryParse(book.GetChildText("price"), out bookPrice);

                    Book bookToSave = new Book();
                    bookToSave.BookTitle = bookTitle;
                    bookToSave.ISBN = bookIsbn;
                    bookToSave.Price = bookPrice;
                    bookToSave.Website = bookWebsite;

                    // get all the authors and parse them
                    var authors = book.SelectSingleNode("authors");
                    if (authors != null)
                    {
                        foreach (XmlNode author in authors.ChildNodes)
                        {
                            string authorName = author.InnerText;
                            Author authorFromDb = db.Authors.FirstOrDefault(x => x.AuthorName == authorName);
                            if (authorFromDb == null)
                            {
                                authorFromDb = new Author();
                                authorFromDb.AuthorName = authorName;
                                db.Authors.Add(authorFromDb);
                                db.SaveChanges();
                            }
                            bookToSave.Authors.Add(db.Authors.FirstOrDefault(x => x.AuthorName == authorName));
                        }
                    }
                    
                  
                    // Get all the reviews for the book;
                    var reviews = book.SelectSingleNode("reviews");
                    if (reviews != null)
                    {
                        foreach (XmlNode review in reviews.ChildNodes)
                        {
                            string reviewContent = review.InnerText;
                            var authorOfReview = review.Attributes.GetNamedItem("author");
                            var dateOfReview = review.Attributes.GetNamedItem("date");
                            Review reviewToSave = new Review();
                            reviewToSave.ReviewContent = reviewContent;
                            if (authorOfReview != null)
                            {
                                string authorName = authorOfReview.InnerText.Trim();
                                // check if author exists
                                Author author = db.Authors.FirstOrDefault(x => x.AuthorName == authorName);
                                if (author == null)
                                {
                                    author = new Author();
                                    author.AuthorName = authorName;
                                    db.Authors.Add(author);
                                   
                                }
                                reviewToSave.Author = author;
                            }

                            // parse the date
                            if (dateOfReview != null)
                            {
                                string date = dateOfReview.InnerText;
                                string format = "d-MMM-yyyy";
                                DateTime dateParsed = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
                                reviewToSave.Date = dateParsed.Date;
                            }
                            else
                            {
                                reviewToSave.Date = DateTime.Now.Date;
                            }
                            bookToSave.Reviews.Add(reviewToSave);
                            
                            db.Reviews.Add(reviewToSave);
                           
                        }
                    }
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
