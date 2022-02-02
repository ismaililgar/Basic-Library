using Kutuphane.Data;
using Kutuphane.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.Controllers
{

    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ValuesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        [Route("Books")]
        public IEnumerable<Books> GetBooks()
        {

            return _context.Books.Include(x => x.Authors).Include(x => x.Publisher).ToList();
        }
        [Route("Book/{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = _context.Books.Include(x => x.Authors).Include(x => x.Publisher).FirstOrDefault(x => x.BookId == id);
            return book != null ? Ok(book) : NotFound(default);
        }
        [Route("Book/authors/{id}")]
        public async Task<IActionResult> GetAuthorsBook(int id)
        {
            var book = _context.Books.Include(x => x.Authors).Include(x => x.Publisher).Where(x => x.Authors.AuthorId == id);
            return book != null ? Ok(book) : NotFound(default);
        }

        [HttpPost]
        [Route("Books")]
        public async Task<IActionResult> SetBooks(Books book)
        {
            if (_context.Books.FirstOrDefault(x => x.BookName == book.BookName) == null)
            {
                _context.Add(new Books()
                {
                    Authors = _context.Authors.FirstOrDefault(x => x.AuthorId == book.Authors.AuthorId),
                    Publisher = _context.Publisher.FirstOrDefault(x => x.PublisherId == book.Publisher.PublisherId),
                    BookName = book.BookName,
                    NumOfPages = book.NumOfPages

                });
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }
        [HttpGet]
        [Route("Authors")]

        public IEnumerable<Authors> GetAuthors()
        {

            return _context.Authors.ToList();
        }
        [HttpPost]
        [Route("Authors")]
        public async Task<IActionResult> SetBooks(Authors author)
        {
            if (_context.Books.FirstOrDefault(x => x.Authors.AuthorId == author.AuthorId) == null)
            {
                _context.Authors.Add(new Authors()
                {
                    AuthorName = author.AuthorName,
                    NumOfBooks = author.NumOfBooks
                });
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }
        [HttpGet]
        [Route("Publisher")]
        public IEnumerable<Publisher> GetPublishers()
        {
            return _context.Publisher.ToList();
        }
        [HttpPost]
        [Route("Publisher")]
        public async Task<IActionResult> SetPublisher(Publisher publisher)
        {
            if (_context.Publisher.FirstOrDefault(x => x.PublisherName == publisher.PublisherName) == null)
            {
                _context.Publisher.Add(new Publisher()
                {
                    PublisherName = publisher.PublisherName,
                    Adress = publisher.Adress
                });
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }
        [HttpDelete]
        [Route("Books/{id}")]
        public async Task<IActionResult> DeleteBooks(int id)
        {

            {
                var t = _context.Books.FirstOrDefault(x => x.BookId == id);
                if (t != null)
                {
                    _context.Books.Remove(t);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("Authors/{id}")]
        public async Task<IActionResult> DeleteAuthors(int id)
        {

            {
                var t = _context.Authors.FirstOrDefault(x => x.AuthorId == id);
                if (t != null)
                {
                    _context.Authors.Remove(t);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("Publisher/{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {

            {
                var t = _context.Publisher.FirstOrDefault(x => x.PublisherId == id);
                if (t != null)
                {
                    _context.Publisher.Remove(t);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }

    }
}
