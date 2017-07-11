using System.Data.Entity;
using Domain.Data.Models;

namespace DataAccess.Context
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext() :
            base("name=BooksDbContext")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Book> Books { get; set; }
    }
}
