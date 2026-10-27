
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

namespace salcudean_delia_lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public IndexModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = new List<Category>();

    public IList<Book> Books { get; set; } = new List<Book>();

    public string? SelectedCategoryName { get; set; }

    public async Task OnGetAsync(int? categoryId)
    {
        Category = await _context.Category.ToListAsync();

        if (categoryId.HasValue)
        {
            var selectedCategory = await _context.Category
                .FirstOrDefaultAsync(c => c.ID == categoryId.Value);

            if (selectedCategory != null)
            {
                SelectedCategoryName = selectedCategory.CategoryName;

                Books = await _context.Book
                    .Where(b => b.BookCategories!
                        .Any(bc => bc.CategoryID == categoryId.Value))
                    .Include(b => b.Author)
                    .ToListAsync();
            }
        }
    }
}
