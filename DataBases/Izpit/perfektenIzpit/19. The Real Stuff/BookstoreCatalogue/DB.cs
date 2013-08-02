namespace BookstoreCatalogue
{
    using Logs.Data;
    using Logs.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Xml;

    public class DB
    {
        private BookstoreEntities db = new BookstoreEntities();
        private LogsContext log;
        private readonly string dateTimeFormat = "d-MMM-yyyy";
        
        public DB(bool resetBookstore, bool resetLog, LogsContext log)
        {
            if (resetBookstore)
            {
                ResetDbData();                       
            }
            if (resetLog)
            {
                log.Database.ExecuteSqlCommand("DELETE Logs");
                log.SaveChanges();
            }
            this.log = log;
        }

        private void ResetDbData()
        {
            db.Database.ExecuteSqlCommand(@"DELETE Reviews
                  DELETE BooksToAuthors
                  DELETE Authors
                  DELETE Books");
        }

        internal void ProcessBooks(string inputPath, bool areComplex)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(inputPath);

            XmlNodeList autors = doc.SelectNodes("/catalog/book");
            foreach (XmlNode item in autors)
            {
                InsertBook(item, areComplex);                
                db.SaveChanges();
            }
        }

        private void InsertBook(XmlNode bookNode, Boolean isComplex)
        {
            XmlNode isbnNode = bookNode.SelectSingleNode("isbn");
            String isbn = null;
            if (isbnNode != null)
            {
                isbn = isbnNode.InnerText;
            }
            Book newBook;
            if (isbn != null)
            {
                newBook = db.Books.FirstOrDefault(x => x.ISBN == isbn);
                if (newBook == null)
                {
                    newBook = new Book() { ISBN = isbn };
                    db.Books.Add(newBook);
                }
            }
            else
            {
                newBook = new Book() { };
                db.Books.Add(newBook);
            }

            XmlNode titleNode = bookNode.SelectSingleNode("title");
            if (titleNode == null)
            {
                throw new InvalidOperationException("Book must have title");
            }
            newBook.Title = titleNode.InnerText;

            String authorXpath;
            if (isComplex)
            {
                authorXpath = "authors/author";
            }
            else
            {
                authorXpath = "author";
            }
            XmlNodeList authorsNodes = bookNode.SelectNodes(authorXpath);
            if (authorsNodes != null)
            {
                foreach (XmlNode authorNode in authorsNodes)
                {
                    String authorName = authorNode.InnerText;
                    Author currentAuthor = db.Authors.FirstOrDefault(x => x.Name == authorName);
                    if (currentAuthor == null)
                    {
                        currentAuthor = new Author() { Name = authorName };
                        db.Authors.Add(currentAuthor);
                    }
                    newBook.Authors.Add(currentAuthor);
                }
            }

            XmlNodeList reviewsNodes = bookNode.SelectNodes("reviews/review");
            if (reviewsNodes != null)
            {
                foreach (XmlNode reviewNode in reviewsNodes)
                {
                    Review curReview = new Review();
                    curReview.ReviewText = reviewNode.InnerText.Trim();

                    XmlAttribute reviewDateAttr = reviewNode.Attributes["date"];
                    if (reviewDateAttr != null)
                    {
                        string reviewDateStr = reviewDateAttr.Value;
                        curReview.DateAdded = DateTime.ParseExact(reviewDateStr,
                            dateTimeFormat, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        curReview.DateAdded = DateTime.Now;
                    }

                    XmlAttribute reviewAuthorAttr = reviewNode.Attributes["author"];
                    if (reviewAuthorAttr != null)
                    {
                        string authorName = reviewAuthorAttr.Value;
                        Author currentAuthor = db.Authors.FirstOrDefault(x => x.Name == authorName);
                        if (currentAuthor == null)
                        {
                            currentAuthor = new Author() { Name = authorName };
                            db.Authors.Add(currentAuthor);
                        }
                        curReview.Author = currentAuthor;
                        db.Reviews.Add(curReview);
                    }
                    newBook.Reviews.Add(curReview);
                }
            }

            XmlNode priceNode = bookNode.SelectSingleNode("price");
            if (priceNode != null)
            {
                newBook.Price = decimal.Parse(priceNode.InnerText);
            }

            XmlNode webSiteNode = bookNode.SelectSingleNode("web-site");
            if (webSiteNode != null)
            {
                newBook.WebSite = webSiteNode.InnerText;
            }
        }

        internal string ExecuteSimpleQuery(string inputPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(inputPath);
            XmlNode queryNode = doc.SelectSingleNode("/query");
            var bookQuery =
                           from b in db.Books.Include("Reviews").Include("Authors")
                           select b;
            XmlNode titleNode = queryNode.SelectSingleNode("title");
            if (titleNode != null)
            {
                String title = titleNode.InnerText;
                bookQuery = bookQuery.Where(x => x.Title == title);
            }

            XmlNode authorNode = queryNode.SelectSingleNode("author");
            if (authorNode != null)
            {
                String author = authorNode.InnerText;
                bookQuery = bookQuery.Where(
                    x => x.Authors.Any(a => a.Name == author));
            }

            XmlNode isbnNode = queryNode.SelectSingleNode("isbn");
            if (isbnNode != null)
            {
                String isbn = isbnNode.InnerText;
                bookQuery = bookQuery.Where(x => x.ISBN == isbn);
            }

            List<QueryEntry> result = new List<QueryEntry>();
            foreach (Book book in bookQuery)
            {
                result.Add(new QueryEntry()
                {
                    BookName = book.Title,
                    ReviewsCount = book.Reviews.Count.ToString(),
                });
            }
            if (result.Count == 0)
            {
                return "Nothing found";
            }
            result.Sort((a, b) => a.BookName.CompareTo(b.BookName));

            return String.Join(Environment.NewLine, result);
        }

        internal void ExecuteReviewsQueries(string inputPath, string outputPath)
        {
            int id = log.Logs.Count();            
            XmlDocument inputDoc = new XmlDocument();
            inputDoc.Load(inputPath);
            XmlNodeList queries = inputDoc.SelectNodes("/review-queries/query");
            using (XmlTextWriter writer = new XmlTextWriter(outputPath, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("search-result");

                foreach (XmlNode item in queries)
                {
                    log.Logs.Add(new Log()
                    {
                        Id=id++,
                        LogDate = DateTime.Now,
                        QueryXml = item.OuterXml,
                    });
                    writer.WriteStartElement("result-set");
                    string queryType = item.Attributes["type"].Value;
                    switch (queryType)
                    {
                        case "by-period":
                            var periodQuerySorted = ParsePeriodQuery(item);
                            ProcessReviewQuery(periodQuerySorted, writer);
                            break;
                        case "by-author":
                            var authorQuerySorted = ParseAuthorQuery(item);
                            ProcessReviewQuery(authorQuerySorted, writer);
                            break;
                        default:
                            throw new InvalidOperationException("Query type not recognized - " + queryType);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                log.SaveChanges();
            }
        }
  
        private IOrderedEnumerable<Review> ParseAuthorQuery(XmlNode item)
        {
            String authorName = item.SelectSingleNode("author-name").InnerText;
            if (authorName==null || authorName.Length==0)
            {
                throw new InvalidOperationException("Author query must have author tag");
            }
            var authorQuery =
                             from r in db.Reviews.Include("Author").Include("Book")
                             where r.Author.Name == authorName
                             select r;
            var authorQuerySorted = authorQuery.ToList().OrderBy(r => r.DateAdded).
            ThenBy(r => r.ReviewText);
            return authorQuerySorted;
        }
  
        private IOrderedEnumerable<Review> ParsePeriodQuery(XmlNode item)
        {
            XmlNode startDateNode = item.SelectSingleNode("start-date");
            if (startDateNode==null || startDateNode.InnerText.Length==0)
            {
                throw new InvalidOperationException("Period query must have non empty start-date tag");
            }
            DateTime startDate = DateTime.ParseExact(startDateNode.InnerText,
                dateTimeFormat, CultureInfo.InvariantCulture);

            XmlNode endDateNode = item.SelectSingleNode("end-date");
            if (endDateNode == null || endDateNode.InnerText.Length==0)
            {
                throw new InvalidOperationException("Period query must have non empty end-date tag");
            }
            DateTime endDate = DateTime.ParseExact(endDateNode.InnerText,
                dateTimeFormat, CultureInfo.InvariantCulture);

            var periodQuery =
                             from r in db.Reviews.Include("Book").Include("Author")
                             where r.DateAdded >= startDate && r.DateAdded <= endDate
                             select r;
            var periodQuerySorted = periodQuery.ToList().OrderBy(r => r.DateAdded).
            ThenBy(r => r.ReviewText);
            return periodQuerySorted;
        }

        private void ProcessReviewQuery(IOrderedEnumerable<Review> authorQuery,
            XmlTextWriter writer)
        {
            foreach (Review curReview in authorQuery)
            {
                writer.WriteStartElement("review");

                if (curReview.DateAdded != null)
                {
                    writer.WriteElementString("date",
                        curReview.DateAdded.ToString(dateTimeFormat));
                }
                writer.WriteElementString("content", curReview.ReviewText);

                writer.WriteStartElement("book");
                writer.WriteElementString("title", curReview.Book.Title);
                var authors = curReview.Book.Authors.OrderBy(a => a.Name).Select(x => x.Name).ToList();
                if (authors.Count > 0)
                {
                    writer.WriteElementString("authors", String.Join(", ", authors));
                }
                if (curReview.Book.ISBN != null)
                {
                    writer.WriteElementString("isbn", curReview.Book.ISBN);
                }
                if (curReview.Book.WebSite != null)
                {
                    writer.WriteElementString("url", curReview.Book.WebSite);
                }
                writer.WriteEndElement(); //Add close tag for book

                writer.WriteEndElement(); //Add close tag for review
            }
        }
    }
}