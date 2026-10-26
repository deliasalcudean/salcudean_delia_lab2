using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

public class salcudean_delia_lab2Context : DbContext
{
    public salcudean_delia_lab2Context(DbContextOptions<salcudean_delia_lab2Context> options)
        : base(options)
    {
    }

    public DbSet<salcudean_delia_lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<salcudean_delia_lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<salcudean_delia_lab2.Models.Author> Author { get; set; } = default!;
    public DbSet<salcudean_delia_lab2.Models.Category> Category { get; set; } = default!;
    public DbSet<salcudean_delia_lab2.Models.BookCategory> BookCategory { get; set; } = default!;
}