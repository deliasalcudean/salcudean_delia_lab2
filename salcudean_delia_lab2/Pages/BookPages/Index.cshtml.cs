using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace salcudean_delia_lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public IndexModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;
    public BookData BookD { get; set; } = default!;
    public int BookID { get; set; } = default!;
    public int CategoryID { get; set; } = default!;
    public string TitleSort { get; set; } = default!;
    public string AuthorSort { get; set; } = default!;
    public string CurrentFilter { get; set; } = default!;
    public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
    {
        BookD = new BookData();

        TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";

        CurrentFilter = searchString;

        BookD.Books = await _context.Book
        .Include(b => b.Publisher)
        .Include(b => b.Author)
        .Include(b => b.BookCategories)
        .ThenInclude(b => b.Category)
        .AsNoTracking()
        .OrderBy(b => b.Title)
        .ToListAsync();

        if (!string.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books.Where(s => s.Author.FirstName.Contains(searchString)
                || s.Author.LastName.Contains(searchString)
                || s.Title.Contains(searchString));

            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books
                .Where(i => i.ID == id.Value).Single();
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
            switch (sortOrder)
            {
                case "title_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s =>
                   s.Title);
                    break;
                case "author_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s =>
                   s.Author.FullName);
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s =>
                   s.Author.FullName);
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title);
                    break;
            }
        }
    }
}
