using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using salcudean_delia_lab2.Models;

namespace salcudean_delia_lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public CreateModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Set<Publisher>(), "ID", "PublisherName");

        ViewData["AuthorID"] = new SelectList(
            _context.Set<Author>()
                .Select(a => new
                {
                    a.ID,
                    FullName = a.FirstName + " " + a.LastName
                }),
            "ID", "FullName");

        Book = new Book();
        Book.BookCategories = new List<BookCategory>();

        PopulateAssignedCategoryData(_context, Book);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        Book.BookCategories = new List<BookCategory>();

        if (selectedCategories != null)
        {
            foreach (var cat in selectedCategories)
            {
                Book.BookCategories.Add(new BookCategory
                {
                    CategoryID = int.Parse(cat)
                });
            }
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}