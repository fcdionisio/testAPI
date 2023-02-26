using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookAPI.Models;
using Microsoft.OpenApi.Any;
using System.Collections.Immutable;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        List<Book> oBooks = new List<Book>()
        {
            new Book(){ BookId = 1, Name="Book of Eli", Author="Shiela Canapi", AgeLimit=13, Price=1000,Publisher="Books Inc."},
            new Book(){ BookId = 2, Name="Love and Hate", Author="Michael Rebutoc", AgeLimit=16, Price=2000,Publisher="Love Capsules Publisher"},
            new Book(){ BookId = 3, Name="Story of Superbaby", Author="Abet Dela Cruz", AgeLimit=7, Price=1500,Publisher="Jone and Joy Publisher Inc"},
            new Book(){ BookId = 4, Name="Book for Kids", Author="Jaypee Morgan", AgeLimit=5, Price=500,Publisher="Best Book Publisher Inc"},
            new Book(){ BookId = 4, Name="Book for Kids 2", Author="Jaypee Morgan", AgeLimit=5, Price=600,Publisher="Best Book Publisher Inc"},
        };

        [HttpGet]
        [Route("ListAll")]
        [Route("All")]
        [Route("ShowAll")]
        public ActionResult<Book> ViewAllBooks()
        {
            if(oBooks.Count == 0)
            {
                return NotFound("No Book found...!");
            }

            return Ok(oBooks);
        }

        [HttpGet]
        [Route("DisplayByBookId/{BookId:int:min(1)}")]

        public ActionResult<Book> DisplayBook(int BookId)
        {
            var book = oBooks.SingleOrDefault(found=>found.BookId == BookId);
            return Ok(book);
        }

        [HttpGet]
        [Route("DisplayByBookName/{Name:alpha}")]

        public ActionResult<Book> DisplayBook(string Name)
        {
            var books = oBooks.ToList();
            List<Book> result = new List<Book>();
            foreach (var book in books)
            {
                if (book.Name.ToLower().Contains(Name.ToLower()))
                {
                    result.Add(book);
                }

            }
            if (result.Count == 0)
            {
                return NotFound("No Book found...!");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("DisplayByBookAgeLimit/{AgeLimit:int:range(1,100)}")]

        public ActionResult<Book> DisplayBookByAge (int AgeLimit)
        {
            var books = oBooks.ToList();
            List<Book> result = new List<Book>();
            foreach(var book in books)
            {
                if(book.AgeLimit>=AgeLimit)
                {
                    result.Add(book);
                }
                
            }
            if(result.Count==0)
            {
                return NotFound("No Book found...!");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("DisplayByPrice/{price:double:range(100,10000)}")]

        public ActionResult<Book> DisplayBookByPrice(double price)
        {
            var books = oBooks.ToList();
            List<Book> result = new List<Book>();
            foreach (var book in books)
            {
                if (book.Price <= price)
                {
                    result.Add(book);
                }

            }
            if (result.Count == 0)
            {
                return NotFound("No Book found...!");
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{BookId:int:min(1)}")]
        public ActionResult<Book> DeleteBook(int BookId)
        {
            var delBook = oBooks.SingleOrDefault(found => found.BookId == BookId);
            if (delBook == null)
            {
                return NotFound("No Book found...!");
            }
            oBooks.Remove(delBook);
            return Ok(oBooks);
        }

        [HttpPost]
        [Route("Save")]
        public ActionResult<Book> SaveBook(Book newBook)
        {
            oBooks.Add(newBook);
            if (oBooks.Count == 0)
            {
                return NotFound("No Book found...!");
            }

            return Ok(oBooks);
        }

        [HttpPut]
        [Route("Update/{BookId:int:min(1)}")]
        public ActionResult<Book> UpdateBook(Book newBook, int BookId)
        {
            var updateBook = oBooks.SingleOrDefault(found=>found.BookId==BookId);
            if (updateBook == null)
            {
                return NotFound("No Book found...!");
            }
            oBooks.Remove(updateBook);
            oBooks.Add(newBook);
            return Ok(oBooks);
        }

    }
}
