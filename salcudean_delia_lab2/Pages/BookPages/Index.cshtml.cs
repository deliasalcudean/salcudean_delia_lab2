using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

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
    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD = new BookData();

        BookD.Books = await _context.Book
        .Include(b => b.Publisher)
        .Include(b => b.Author)
        .Include(b => b.BookCategories)
        .ThenInclude(b => b.Category)
        .AsNoTracking()
        .OrderBy(b => b.Title)
        .ToListAsync();

        if (id != null)
        {
            BookID = id.Value;
            Book book = BookD.Books
            .Where(i => i.ID == id.Value).Single();
            BookD.Categories = book.BookCategories.Select(s => s.Category);
        }
    }
}
