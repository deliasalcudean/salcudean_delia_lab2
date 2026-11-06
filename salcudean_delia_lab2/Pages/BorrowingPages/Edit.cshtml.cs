using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

namespace salcudean_delia_lab2.Pages.BorrowingPages;

public class EditModel : PageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public EditModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public SelectList MemberList { get; set; } = default!;
    public SelectList BookList { get; set; } = default!;

    private async Task PopulateLists()
    {
        MemberList = new SelectList(
            await _context.Member.ToListAsync(),
            "ID",
            "FullName"
        );

        BookList = new SelectList(
            await _context.Book.ToListAsync(),
            "ID",
            "Title"
        );
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Borrowing = await _context.Borrowing
            .FirstOrDefaultAsync(m => m.ID == id);

        if (Borrowing == null)
            return NotFound();

        await PopulateLists();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await PopulateLists();
            return Page();
        }

        var borrowingToUpdate = await _context.Borrowing
            .FindAsync(Borrowing.ID);

        if (borrowingToUpdate == null)
            return NotFound();

        borrowingToUpdate.MemberID = Borrowing.MemberID;
        borrowingToUpdate.BookID = Borrowing.BookID;
        borrowingToUpdate.ReturnDate = Borrowing.ReturnDate;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}