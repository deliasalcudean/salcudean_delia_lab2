using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

namespace salcudean_delia_lab2.Pages.PublisherPages;

public class DetailsModel : PageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public DetailsModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    public Publisher Publisher { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Publisher = await _context.Publisher
            .Include(p => p.Books)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (Publisher == null)
        {
            return NotFound();
        }
        return Page();
    }
}
