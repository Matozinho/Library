using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Services.Interfaces;

namespace Library.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public void AddBook(Book book)
    {
        throw new NotImplementedException();
    }

    public void BulkLoad(string filePath)
    {
        throw new NotImplementedException();
    }

    public void LoadBooksFromJson(string filePath)
    {
        throw new NotImplementedException();
    }

    public Book? GetBookById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> GetAllBooks()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> SearchBooksByTitle(string title)
    {
        throw new NotImplementedException();
    }

    public Book? GetBookByIsbn(string isbn)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> SearchBooksByAuthor(string author)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> SearchBooksByPublisher(string publisher)
    {
        throw new NotImplementedException();
    }

    public void DeleteBook(string isbn)
    {
        throw new NotImplementedException();
    }
}
