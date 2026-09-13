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
                Console.ReadKey();
                Console.Clear();
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
        // TODO: Call _bookService.AddBook(...)
        Console.WriteLine("[Coming Soon] Add Book feature to be implemented via BookService.");
    }

    private void ExecuteListBooks()
    {
        Console.WriteLine("--- Book Collection ---");
        // TODO: Call _bookService.GetAllBooks()
        Console.WriteLine("[Coming Soon] List Books feature to be implemented via BookService.");
    }

    private void ExecuteSearchByTitle()
    {
        Console.WriteLine("--- Search Book by Title ---");
        // TODO: Call _bookService.SearchBooksByTitle(...)
        Console.WriteLine("[Coming Soon] Search by Title feature to be implemented via BookService.");
    }

    private void ExecuteSearchByIsbn()
    {
        Console.WriteLine("--- Search Book by ISBN ---");
        // TODO: Call _bookService.GetBookByIsbn(...)
        Console.WriteLine("[Coming Soon] Search by ISBN feature to be implemented via BookService.");
    }

    private void ExecuteSearchByAuthor()
    {
        Console.WriteLine("--- Search Book by Author ---");
        // TODO: Call _bookService.SearchBooksByAuthor(...)
        Console.WriteLine("[Coming Soon] Search by Author feature to be implemented via BookService.");
    }

    private void ExecuteSearchByPublisher()
    {
        Console.WriteLine("--- Search Book by Publisher ---");
        // TODO: Call _bookService.SearchBooksByPublisher(...)
        Console.WriteLine("[Coming Soon] Search by Publisher feature to be implemented via BookService.");
    }

    private void ExecuteDeleteBook()
    {
        Console.WriteLine("--- Delete Book ---");
        // TODO: Call _bookService.DeleteBook(...)
        Console.WriteLine("[Coming Soon] Delete Book feature to be implemented via BookService.");
    }

    private void ExecuteBulkLoad()
    {
        Console.WriteLine("--- Bulk Load Books from JSON ---");
        // TODO: Call _bookService.LoadBooksFromJson("books_dataset.json")
        Console.WriteLine("[Coming Soon] Bulk Load feature to be implemented via BookService.");
    }
}
