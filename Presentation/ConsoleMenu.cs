using Library.Domain.Entities;
using Library.Services.Interfaces;

namespace Library.Presentation;

public class ConsoleMenu
{
    private readonly IBookService _bookService;

    public ConsoleMenu(IBookService bookService)
    {
        _bookService = bookService;
    }

    private bool _isRunning = true;

    public void Run()
    {
        _isRunning = true;

        while (_isRunning)
        {
            DisplayMenu();
            string? option = Console.ReadLine();
            Console.WriteLine();

            ProcessOption(option);

            if (_isRunning)
            {
                Console.WriteLine("\nPress any key to return to the main menu...");
                if (Console.IsInputRedirected)
                {
                    Console.ReadLine();
                }
                else
                {
                    Console.ReadKey();
                }

                if (!Console.IsOutputRedirected && !Console.IsInputRedirected)
                {
                    try { Console.Clear(); } catch { }
                }
            }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("       LIBRARY MANAGEMENT SYSTEM        ");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Register New Book");
        Console.WriteLine("2. List All Books");
        Console.WriteLine("3. Search Book by Title");
        Console.WriteLine("4. Search Book by ISBN");
        Console.WriteLine("5. Search Book by Author");
        Console.WriteLine("6. Search Book by Publisher");
        Console.WriteLine("7. Delete Book");
        Console.WriteLine("8. Bulk Load Books from JSON");
        Console.WriteLine("9. Update Book by Id");
        Console.WriteLine("0. Exit");
        Console.WriteLine("========================================");
        Console.Write("Select an option: ");
    }

    private void ProcessOption(string? option)
    {
        switch (option)
        {
            case "1":
                ExecuteAddBook();
                break;
            case "2":
                ExecuteListBooks();
                break;
            case "3":
                ExecuteSearchByTitle();
                break;
            case "4":
                ExecuteSearchByIsbn();
                break;
            case "5":
                ExecuteSearchByAuthor();
                break;
            case "6":
                ExecuteSearchByPublisher();
                break;
            case "7":
                ExecuteDeleteBook();
                break;
            case "8":
                ExecuteBulkLoad();
                break;
            case "9":
                ExecuteUpdateBook();
                break;
            case "0":
                Console.WriteLine("Exiting Library Management System. Goodbye!");
                _isRunning = false;
                break;
            default:
                Console.WriteLine("Invalid option!");
                break;
        }
    }

    private void ExecuteAddBook()
    {
        Console.WriteLine("--- Register New Book ---");
        try
        {
            Console.Write("Title: ");
            string title = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Author: ");
            string author = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Year: ");
            string year = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Genre: ");
            string genre = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("ISBN: ");
            string isbn = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Publisher: ");
            string publisher = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Pages: ");
            int.TryParse(Console.ReadLine()?.Trim(), out int pages);

            var book = new Book
            {
                Title = title,
                Author = author,
                Year = year,
                Genre = genre,
                Isbn = isbn,
                Publisher = publisher,
                Pages = pages
            };

            _bookService.AddBook(book);
            Console.WriteLine("\n[SUCCESS] Book registered successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR] Failed to register book: {ex.Message}");
        }
    }

    private void ExecuteListBooks()
    {
        Console.WriteLine("--- Book Collection ---");
        try
        {
            var books = _bookService.GetAllBooks().ToList();
            if (books.Count == 0)
            {
                Console.WriteLine("No books found in the collection.");
                return;
            }

            Console.WriteLine($"Found {books.Count} book(s):\n");
            DisplayBooks(books);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR] Failed to list books: {ex.Message}");
        }
    }

    private void ExecuteSearchByTitle()
    {
        Console.WriteLine("--- Search Book by Title ---");
        try
        {
            Console.Write("Enter title search term: ");
            string title = Console.ReadLine()?.Trim() ?? string.Empty;

            var books = _bookService.SearchBooksByTitle(title).ToList();
            if (books.Count == 0)
            {
                Console.WriteLine("No books found matching the title search.");
                return;
            }

            Console.WriteLine($"Found {books.Count} matching book(s):\n");
            DisplayBooks(books);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR] Search failed: {ex.Message}");
        }
    }

    private void ExecuteSearchByIsbn()
    {
        Console.WriteLine("--- Search Book by ISBN ---");
        try
        {
            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine()?.Trim() ?? string.Empty;

            var book = _bookService.GetBookByIsbn(isbn);
            if (book == null)
            {
                Console.WriteLine("Book not found with the specified ISBN.");
                return;
            }

            Console.WriteLine("Book details:\n");
            DisplayBook(book);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR] Search failed: {ex.Message}");
        }
    }

    private void ExecuteSearchByAuthor()
    {
        Console.WriteLine("--- Search Book by Author ---");
        try
        {
            Console.Write("Enter author name search term: ");
            string author = Console.ReadLine()?.Trim() ?? string.Empty;

            var books = _bookService.SearchBooksByAuthor(author).ToList();
            if (books.Count == 0)
            {
                Console.WriteLine("No books found matching the author search.");
                return;
            }

            Console.WriteLine($"Found {books.Count} matching book(s):\n");
            DisplayBooks(books);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR] Search failed: {ex.Message}");
        }
    }

    private void ExecuteSearchByPublisher()
    {
        Console.WriteLine("--- Search Book by Publisher ---");
        try
        {
            Console.Write("Enter publisher name search term: ");
            string publisher = Console.ReadLine()?.Trim() ?? string.Empty;

            var books = _bookService.SearchBooksByPublisher(publisher).ToList();
            if (books.Count == 0)
            {
                Console.WriteLine("No books found matching the publisher search.");
                return;
            }

            Console.WriteLine($"Found {books.Count} matching book(s):\n");
            DisplayBooks(books);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR] Search failed: {ex.Message}");
        }
    }

    private void ExecuteDeleteBook()
    {
        Console.WriteLine("--- Delete Book ---");
        try
        {
            Console.Write("Enter ISBN of book to delete: ");
            string isbn = Console.ReadLine()?.Trim() ?? string.Empty;

            _bookService.DeleteBook(isbn);
            Console.WriteLine($"\n[SUCCESS] Book with ISBN '{isbn}' deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR] Failed to delete book: {ex.Message}");
        }
    }

    private void ExecuteBulkLoad()
    {
        Console.WriteLine("--- Bulk Load Books from JSON ---");
        try
        {
            Console.Write("Enter file path [Default: books_dataset.json]: ");
            string inputPath = Console.ReadLine()?.Trim() ?? string.Empty;
            string filePath = string.IsNullOrWhiteSpace(inputPath) ? "books_dataset.json" : inputPath;

            Console.WriteLine($"Loading books from '{filePath}'...");
            _bookService.LoadBooksFromJson(filePath);

            int count = _bookService.GetAllBooks().Count();
            Console.WriteLine($"\n[SUCCESS] Dataset loaded successfully! Total books in collection: {count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR] Failed to bulk load dataset: {ex.Message}");
        }
    }

    private void ExecuteUpdateBook()
    {
        Console.WriteLine("--- Update Book by Id ---");
        try
        {
            Console.Write("Enter Book ID (Guid): ");
            string idInput = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!Guid.TryParse(idInput, out Guid id))
            {
                Console.WriteLine("\n[ERROR] Invalid Guid format.");
                return;
            }

            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                Console.WriteLine("\n[ERROR] Book not found with the specified ID.");
                return;
            }

            Console.WriteLine("\nCurrent Book Details:");
            DisplayBook(book);
            Console.WriteLine("\nEnter new values (or press Enter to keep current value):");

            Console.Write($"Title [{book.Title}]: ");
            string titleInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(titleInput)) book.Title = titleInput;

            Console.Write($"Author [{book.Author}]: ");
            string authorInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(authorInput)) book.Author = authorInput;

            Console.Write($"Year [{book.Year}]: ");
            string yearInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(yearInput)) book.Year = yearInput;

            Console.Write($"Genre [{book.Genre}]: ");
            string genreInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(genreInput)) book.Genre = genreInput;

            Console.Write($"ISBN [{book.Isbn}]: ");
            string isbnInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(isbnInput)) book.Isbn = isbnInput;

            Console.Write($"Publisher [{book.Publisher}]: ");
            string publisherInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(publisherInput)) book.Publisher = publisherInput;

            Console.Write($"Pages [{book.Pages}]: ");
            string pagesInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(pagesInput) && int.TryParse(pagesInput, out int pages))
            {
                book.Pages = pages;
            }

            _bookService.UpdateBook(book);
            Console.WriteLine("\n[SUCCESS] Book updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR] Failed to update book: {ex.Message}");
        }
    }

    private static void DisplayBooks(IEnumerable<Book> books)
    {
        foreach (var book in books)
        {
            DisplayBook(book);
            Console.WriteLine(new string('-', 40));
        }
    }

    private static void DisplayBook(Book book)
    {
        Console.WriteLine($"{"ID:",-11} {book.Id}");
        Console.WriteLine($"{"Title:",-11} {book.Title}");
        Console.WriteLine($"{"Author:",-11} {book.Author}");
        Console.WriteLine($"{"Year:",-11} {book.Year}");
        Console.WriteLine($"{"Genre:",-11} {book.Genre}");
        Console.WriteLine($"{"ISBN:",-11} {book.Isbn}");
        Console.WriteLine($"{"Publisher:",-11} {book.Publisher}");
        Console.WriteLine($"{"Pages:",-11} {book.Pages}");
    }
}
