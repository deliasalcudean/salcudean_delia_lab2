using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;
using salcudean_delia_lab2.Models.ViewModels;

namespace salcudean_delia_lab2.Pages.PublisherPages;

public class IndexModel : PageModel
{
    private readonly salcudean_delia_lab2Context _context;

    public IndexModel(salcudean_delia_lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;
    public PublisherIndexData PublisherData { get; set; }
    public int PublisherID { get; set; }
    public int BookID { get; set; }
    public async Task OnGetAsync(int? id, int? bookID)
    {
        PublisherData = new PublisherIndexData();
        PublisherData.Publishers = await _context.Publisher
        .Include(i => i.Books)
        .ThenInclude(c => c.Author)
        .OrderBy(i => i.PublisherName)
        .ToListAsync();
        if (id != null)
        {
            PublisherID = id.Value;
            Publisher publisher = PublisherData.Publishers
            .Where(i => i.ID == id.Value).Single();
            PublisherData.Books = publisher.Books;
        }
            }

}
