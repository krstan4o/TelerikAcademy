using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data;

    public class BookStoreDAL
    {
        public static void AddBook(string authorName,
            string title, string webSite, string ISBN, string price)
        {
            BookStoreEntities context = new BookStoreEntities();
            Book newBook = new Book();
            newBook.Authors.Add(CreateOrLoadUser(context, authorName));
            newBook.Title = title;
            newBook.WebSite = webSite;
            newBook.ISBN = ISBN;
            if (authorName == null || title == null)
            {
                throw new ArgumentNullException("AuthorName and BookTitle has not to be null");
            }
            context.Books.Add(newBook);
            context.SaveChanges();
        }
        private static Author CreateOrLoadUser(
             BookStoreEntities context, string authorName)
        {
            Author existingAuthor = context.Authors.FirstOrDefault(x => x.Name == authorName);
               
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
    
}
