using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BookStore.Data
{
    public static class BookStoreDAL
    {
        // Task 1 - 2 are in Code First aproach and I em using DataAnotattions Atributes for adding constraints
        public static void AddBook(string author,
            string title, string webSite, decimal price, string isbn)
        {
            if (author == null || title == null)
            {
                throw new ArgumentNullException("BookTitle and BookAuthor(s) must not be null");
            }
            BookStoreDB context = new BookStoreDB();
            Book newBook = new Book();
            newBook.Authors.Add(CreateOrLoadAuthor(context, author));
            newBook.Title = title;
            newBook.Website = webSite;
            newBook.Price = price;
            newBook.Isbn = isbn;
          
            context.Books.Add(newBook);
            context.SaveChanges();
        }

        public static void AddBook(XmlNodeList authors, XmlNodeList reviews,
            string title, string webSite, decimal price, string isbn)
        {
            if (title == null)
            {
                throw new ArgumentNullException("BookTitle must not be null");
            }

            BookStoreDB context = new BookStoreDB();

            Book newBook = new Book();
            foreach (XmlNode author in authors)
            {
                string authorName = author.InnerText;
                newBook.Authors.Add(CreateOrLoadAuthor(context, authorName));
            }

            foreach (XmlNode review in reviews)
            {
                string authorName = null;
                XmlAttributeCollection atributes = review.Attributes;
                string reviewText = review.InnerText.Trim();
                if (review.Attributes.GetNamedItem("author") != null)
                {
                    authorName  = review.Attributes.GetNamedItem("author").Value;
                }
                string date = "";
                if (review.Attributes.GetNamedItem("date") != null)
                {
                     date = review.Attributes["date"].Value;
                }
                DateTime parsed;
                string dateFormat = "d-MMM-yyyy";
                  DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed);
               
                newBook.Reviews.Add(CreateReview(context, parsed, authorName, reviewText));
            }
            newBook.Title = title;
            newBook.Website = webSite;
            newBook.Price = price;
            newBook.Isbn = isbn;

            context.Books.Add(newBook);
            context.SaveChanges();
        }

        private static Review CreateReview(BookStoreDB context, DateTime date, string authorName, string reviewText)
        {
            Review review = new Review();
            if (!string.IsNullOrEmpty(authorName))
            {
                review.Author = CreateOrLoadAuthor(context, authorName);
            }
            
            review.Date = date;
            review.TextReview = reviewText;
            return review;
        }

        private static Author CreateOrLoadAuthor(
            BookStoreDB context, string authorName)
        {
            Author existingAuthor =
                (from u in context.Authors
                 where u.Name == authorName
                 select u).FirstOrDefault();
            if (existingAuthor != null)
            {
                return existingAuthor;
            }

            Author newAuthor = new Author();
            newAuthor.Name = authorName;
            context.Authors.Add(newAuthor);
            context.SaveChanges();

            return newAuthor;
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


        public static void FindBook(string title, string isbn, string author)
        {
            BookStoreDB db = new BookStoreDB();
          
            var findedBooksByAuthor = db.Authors.FirstOrDefault(x => x.Name.ToLower() == author.ToLower()).Books;
            var findedBooksByTitle = db.Books.Where(x => x.Title.ToLower() == title.ToLower()).ToList();
            var findedBooksByIsbn = db.Books.Where(x => x.Isbn == isbn).ToList();

            var findedBooks = findedBooksByAuthor.Union(findedBooksByTitle).Union(findedBooksByIsbn);
            if (findedBooks.Count() > 0)
            {
                Console.WriteLine("{0} books found:", findedBooksByIsbn.Count());
                foreach (var book in findedBooksByIsbn)
                {
                    if (book.Reviews.Count() > 0)
                    {
                        Console.WriteLine(book.Title + " --> " + book.Reviews.Count);
                    }
                    else
                    {
                        Console.WriteLine(book.Title + " --> no reviews");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nothing found");
            }
            
        }
    }
}
