using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

namespace salcudean_delia_lab2.Pages.BookPages;

public class DetailsModel : PageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public DetailsModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (book == null)
        {
            return NotFound();
        }

        Book = book;

        return Page();
    }
}