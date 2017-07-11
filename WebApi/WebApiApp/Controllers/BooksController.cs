using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Data.Models;
using Repositories.Repos;

namespace WebAPISample.Controllers
{
    public class BooksController : ApiController
    {
        // GET api/values
        public IEnumerable<Book> Get()
        {
            using (BookRepository entities = new BookRepository())
            {
                return entities.GetAllBooks();
            }
        }

        // GET api/values/5
        public Book Get(int id)
        {
            using (BookRepository entities = new BookRepository())
            {
                return entities.GetBook(id);
            }
        }

        // POST api/values
        public HttpResponseMessage Post(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BookRepository entities = new BookRepository())
                    {
                        entities.InsertBook(book);
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Model state is invalid");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, Book value)
        {
            try
            {
                using (BookRepository entities = new BookRepository())
                {
                    entities.UpdateBookName(id,value);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (BookRepository entities = new BookRepository())
                {
                    entities.DeleteBook(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}