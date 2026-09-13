using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Infrastructure.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    public void Add(Book book)
    {
        throw new NotImplementedException();
    }

    public void BulkLoad(IEnumerable<Book> books)
    {
        throw new NotImplementedException();
    }

    public Book? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Book book)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> SearchByTitle(string title)
    {
        throw new NotImplementedException();
    }

    public Book? GetByIsbn(string isbn)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> SearchByAuthor(string author)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> SearchByPublisher(string publisher)
    {
        throw new NotImplementedException();
    }
}
