using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

namespace salcudean_delia_lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public EditModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Book = await _context.Book
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (Book == null)
        {
            return NotFound();
        }

        PopulateAssignedCategoryData(_context, Book);
        LoadDropdowns();

        return Page();
    }

    private void LoadDropdowns()
    {
        var authorList = _context.Set<Author>()
            .Select(a => new
            {
                a.ID,
                FullName = a.FirstName + " " + a.LastName
            });

        ViewData["AuthorID"] = new SelectList(
            authorList, "ID", "FullName", Book.AuthorID);

        ViewData["PublisherID"] = new SelectList(
            _context.Set<Publisher>(),
            "ID", "PublisherName", Book.PublisherID);
    }

    public async Task<IActionResult> OnPostAsync(
        int? id, string[] selectedCategories)
    {
        if (id == null || id != Book.ID)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            PopulateAssignedCategoryData(_context, Book);
            LoadDropdowns();
            return Page();
        }

        var bookToUpdate = await _context.Book
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            b => b.Title,
            b => b.AuthorID,
            b => b.Price,
            b => b.PublishingDate,
            b => b.PublisherID))
        {
            UpdateBookCategories(
                _context, selectedCategories, bookToUpdate);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        PopulateAssignedCategoryData(_context, bookToUpdate);
        LoadDropdowns();

        return Page();
    }
}