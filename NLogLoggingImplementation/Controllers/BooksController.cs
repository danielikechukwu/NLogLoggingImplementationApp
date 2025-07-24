using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLogLoggingImplementation.Models;

namespace NLogLoggingImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // In-memory storage for demonstration purposes.
        private static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "1984", Author = "George Orwell", YearPublished = 1949 },
            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", YearPublished = 1960 }
        };

        private readonly ILogger<BooksController> _logger;

        // Constructor injection for ILogger<T>
        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                _logger.LogWarning("AddBook: Attempted to add a null book.");

                return BadRequest("Book cannot be null.");
            }

            Books.Add(book);

            _logger.LogInformation("Added a new book {@Book}", book);

            return Ok();
        }

        [HttpGet("get-books")]
        public IActionResult GetBooks()
        {
            _logger.LogInformation("Retrieved all books. Books: {@Books}", Books);

            if (Books.Count == 0)
            {
                _logger.LogWarning("GetBooks: No books found.");

                return NotFound("No books available.");
            }

            _logger.LogInformation("GetBooks: Found {Count} books.", Books.Count);

            return Ok(Books);
        }
    }
}
