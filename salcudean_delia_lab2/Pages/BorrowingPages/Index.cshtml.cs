using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

namespace salcudean_delia_lab2.Pages.BorrowingPages;

public class IndexModel : PageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public IndexModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    public IList<Borrowing> Borrowing { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Borrowing != null)
        {
            Borrowing = await _context.Borrowing
            .Include(b => b.Book)
            .ThenInclude(b => b.Author)
            .Include(b => b.Member).ToListAsync();
        }
    }
}
