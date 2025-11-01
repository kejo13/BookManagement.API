using BookManagement.API.Interfaces;
using BookManagement.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<ActionResult<Book>> GetBookByIdAsync(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound(new { message = $"Book with ID {id} not found." });

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBookAsync([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book data is required.");

            await _bookService.AddBookAsync(book);
            return CreatedAtRoute("GetBookById", new { id = book.Id }, book);
        }
    }
}
