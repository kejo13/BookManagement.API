using BookManagement.API.Interfaces;
using BookManagement.API.Models;

namespace BookManagement.API.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books;

        public BookService()
        {
            _books = new List<Book>
            {
                new Book { Id = 1, Title = "", Author = "", YearPublished = 2025},
                new Book { Id = 2, Title = "", Author = "", YearPublished = 2025},
            };
        }

        public Task AddBookAsync(Book book)
        {
            book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
            return Task.CompletedTask;
        }

        public Task<List<Book>> GetAllBooksAsync()
        {
            return Task.FromResult(_books.ToList());
        }

        public Task<Book?> GetBookByIdAsync(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return Task.FromResult(book);
        }
    }
}
