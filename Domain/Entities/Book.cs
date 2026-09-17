namespace Library.Domain.Entities;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Year { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public string Isbn { get; set; } = string.Empty;

    public string Publisher { get; set; } = string.Empty;

    public int Pages { get; set; }
}
