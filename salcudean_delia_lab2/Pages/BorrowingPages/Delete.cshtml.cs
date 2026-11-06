using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

namespace salcudean_delia_lab2.Pages.BorrowingPages;

public class DeleteModel : PageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public DeleteModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (Borrowing == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var borrowing = await _context.Borrowing.FindAsync(id);

        if (borrowing != null)
        {
            _context.Borrowing.Remove(borrowing);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}