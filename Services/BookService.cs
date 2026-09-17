using System.Text.Json;
using System.Text.Json.Serialization;
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
        ArgumentNullException.ThrowIfNull(book);

        if (string.IsNullOrWhiteSpace(book.Title))
            throw new ArgumentException("Book title cannot be empty.", nameof(book));

        if (string.IsNullOrWhiteSpace(book.Author))
            throw new ArgumentException("Book author cannot be empty.", nameof(book));

        if (string.IsNullOrWhiteSpace(book.Isbn))
            throw new ArgumentException("Book ISBN cannot be empty.", nameof(book));

        var existingBook = _repository.GetByIsbn(book.Isbn);
        if (existingBook != null)
            throw new InvalidOperationException($"A book with ISBN '{book.Isbn}' already exists.");

        if (book.Id == Guid.Empty)
        {
            book.Id = Guid.NewGuid();
        }

        _repository.Add(book);
    }

    public void LoadBooksFromJson(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The specified dataset file was not found: {filePath}");

        string jsonContent = File.ReadAllText(filePath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var rawBooks = JsonSerializer.Deserialize<List<BookJsonDto>>(jsonContent, options);

        if (rawBooks == null || rawBooks.Count == 0)
            return;

        var books = rawBooks.Select(dto => new Book
        {
            Id = Guid.NewGuid(),
            Title = dto.Title ?? string.Empty,
            Author = dto.Author ?? string.Empty,
            Year = dto.Year?.ToString() ?? string.Empty,
            Genre = dto.Genre ?? string.Empty,
            Isbn = dto.Isbn ?? string.Empty,
            Publisher = dto.Publisher ?? string.Empty,
            Pages = dto.Pages
        });

        _repository.BulkLoad(books);
    }

    public void BulkLoad(string filePath)
    {
        LoadBooksFromJson(filePath);
    }

    public Book? GetBookById(Guid id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _repository.GetAll();
    }

    public IEnumerable<Book> SearchBooksByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Enumerable.Empty<Book>();

        return _repository.SearchByTitle(title);
    }

    public Book? GetBookByIsbn(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return null;

        return _repository.GetByIsbn(isbn);
    }

    public IEnumerable<Book> SearchBooksByAuthor(string author)
    {
        if (string.IsNullOrWhiteSpace(author))
            return Enumerable.Empty<Book>();

        return _repository.SearchByAuthor(author);
    }

    public IEnumerable<Book> SearchBooksByPublisher(string publisher)
    {
        if (string.IsNullOrWhiteSpace(publisher))
            return Enumerable.Empty<Book>();

        return _repository.SearchByPublisher(publisher);
    }

    public void DeleteBook(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN cannot be empty.", nameof(isbn));

        var book = _repository.GetByIsbn(isbn);
        if (book == null)
            throw new KeyNotFoundException($"Book with ISBN '{isbn}' not found.");

        _repository.Delete(book.Id);
    }

    public void UpdateBook(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);

        if (book.Id == Guid.Empty)
            throw new ArgumentException("Book ID cannot be empty.", nameof(book));

        if (string.IsNullOrWhiteSpace(book.Title))
            throw new ArgumentException("Book title cannot be empty.", nameof(book));

        if (string.IsNullOrWhiteSpace(book.Author))
            throw new ArgumentException("Book author cannot be empty.", nameof(book));

        if (string.IsNullOrWhiteSpace(book.Isbn))
            throw new ArgumentException("Book ISBN cannot be empty.", nameof(book));

        var existingBook = _repository.GetById(book.Id);
        if (existingBook == null)
            throw new KeyNotFoundException($"Book with ID '{book.Id}' not found.");

        var existingWithIsbn = _repository.GetByIsbn(book.Isbn);
        if (existingWithIsbn != null && existingWithIsbn.Id != book.Id)
            throw new InvalidOperationException($"Another book with ISBN '{book.Isbn}' already exists.");

        _repository.Update(book);
    }

    private class BookJsonDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public object? Year { get; set; }
        public string? Genre { get; set; }
        public string? Isbn { get; set; }
        public string? Publisher { get; set; }
        public int Pages { get; set; }
    }
}
