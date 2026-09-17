using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Infrastructure.Repositories;

public class InMemoryBookRepository : IBookRepository
{
  private readonly List<Book> _books = [];

  public void Add(Book book)
  {
    _books.Add(book);
  }

  public void BulkLoad(IEnumerable<Book> books)
  {
    _books.AddRange(books.Where(x => x != null));
  }

  public Book? GetById(Guid id)
  {
    return _books.Find(b => id == b.Id);
  }

  public IEnumerable<Book> GetAll()
  {
    return _books;
  }

  public void Update(Book book)
  {
    int storedBookIdx = _books.FindIndex(b => b.Id == book.Id);

    if (storedBookIdx == -1)
      throw new KeyNotFoundException("Book not found");

    _books[storedBookIdx] = book;
  }

  public void Delete(Guid id)
  {
    int storedBookIdx = _books.FindIndex(b => b.Id == id);

    if (storedBookIdx == -1)
      throw new KeyNotFoundException("Book not found");

    _books.RemoveAt(storedBookIdx);
  }

  public IEnumerable<Book> SearchByTitle(string title)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(title);
    return _books.FindAll(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
  }

  public Book? GetByIsbn(string isbn)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(isbn);

    return _books.Find(b => String.Equals(b.Isbn, isbn, StringComparison.OrdinalIgnoreCase));
  }

  public IEnumerable<Book> SearchByAuthor(string author)
  {
    ArgumentException.ThrowIfNullOrEmpty(author);

    return _books.FindAll(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
  }

  public IEnumerable<Book> SearchByPublisher(string publisher)
  {
    ArgumentException.ThrowIfNullOrEmpty(publisher);

    return _books.FindAll(b => b.Publisher.Contains(publisher, StringComparison.OrdinalIgnoreCase));
  }
}
