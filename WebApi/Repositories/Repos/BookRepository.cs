using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using Domain.Data.Models;

namespace Repositories.Repos
{
    public class BookRepository : IDisposable
    {
        private BooksDbContext _context = null;

        public void InsertBook(Book b)
        {
            _context = new BooksDbContext();
            _context.Books.Add(b);
            _context.SaveChanges();
        }

        public Book GetBook(int id)
        {
            _context = new BooksDbContext();
            try
            {
                return _context.Books.Where(b => b.BookId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return null;
        }
        public IEnumerable<Book> GetAllBooks()
        {
            _context = new BooksDbContext();
            try
            {
              return _context.Books.ToList();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return null;
        }

        public void UpdateBookName(int id, Book book)
        {
            _context = new BooksDbContext();
            Book foundBook = _context.Books.SingleOrDefault<Book>(b => b.BookId == id);
            if (foundBook != null)
                foundBook.BookName = book.BookName;
                _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            _context = new BooksDbContext();
            Book foundBook = _context.Books.SingleOrDefault<Book>(b => b.BookId == id);
            if(foundBook != null)
                _context.Books.Remove(foundBook);

        }

        public void Dispose()
        {
           
        }
    }
}
