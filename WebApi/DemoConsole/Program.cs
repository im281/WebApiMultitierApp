using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Data.Models;
using Repositories.Repos;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book();
            book.BookId = 2;
            book.AuthorName = "Iman";
            book.BookName = "My WebAPI";
            book.BookText = "cool2";

            BookRepository r = new BookRepository();
            r.InsertBook(book);
            var books = r.GetAllBooks();
        }
    }
}
