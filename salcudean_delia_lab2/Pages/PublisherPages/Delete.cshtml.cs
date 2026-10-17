using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

namespace salcudean_delia_lab2.Pages.PublisherPages;

public class DeleteModel : PageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public DeleteModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var publisher = await _context.Publisher.FindAsync(id);

        if (publisher != null)
        {
            _context.Publisher.Remove(publisher);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
