using System;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Domain.Data.Models;

namespace DataAccess.Context
{
    public class BooksMongoDBcontext
    {
        string dbName = "BookDb";
        MongoClient client;
        IMongoDatabase db;
        IMongoCollection<Book> collection;

        public BooksMongoDBcontext()
        {
            InitializeDb();
        }
        private void InitializeDb()
        {
            client = new MongoClient();
            db = client.GetDatabase(dbName);
            collection = db.GetCollection<Book>("BookStore");
        }


        public void InsertBookStore(Book b)
        {
            collection.InsertOne(b);
        }

        public void GetFeature(int featureId)
        {

        }
        public void RunQueries()
        {
            //Query data
            //var bookCount = collection.AsQueryable().Where(b => b.TotalPages > 200);
            //Console.WriteLine("\n\nBooks count having pages more than 200 is => " + bookCount.Count());

            //var book = collection.AsQueryable().Where(b => b.BookTitle.StartsWith("Mongo"));
            //Console.WriteLine("\n\nBook whose title starts with 'Mongo' is => " + book.First().BookTitle);

            //var cheapestBook = collection.AsQueryable().OrderBy(b => b.Price).First();
            //Console.WriteLine("\n\nCheapest Book is => " + cheapestBook.BookTitle);

            //var bookWithISBN = collection.AsQueryable().Single(b => b.ISBN == "6779799933389898yu");
            //Console.WriteLine("\n\nBook with ISBN number 6779799933389898yu is => " + bookWithISBN.BookTitle);

            //collection.RemoveAll();
            //Console.ReadLine();
        }
    }
}
