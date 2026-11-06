using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Models;

namespace salcudean_delia_lab2.Data
{
    public class LibraryIdentityContext : IdentityDbContext
    {
        public LibraryIdentityContext(DbContextOptions<LibraryIdentityContext> options)
            : base(options)
        { }
            public DbSet<Member> Member { get; set; } = default!;
        public DbSet<Book> Book { get; set; } = default!;
    }
}

