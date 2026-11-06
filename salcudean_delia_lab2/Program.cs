
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using salcudean_delia_lab2.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("salcudean_delia_lab2Context")
    ?? throw new InvalidOperationException("Connection string 'salcudean_delia_lab2Context' not found.");

// Contextul bazei de date pentru aplicație
builder.Services.AddDbContext<salcudean_delia_lab2Context>(options =>
    options.UseSqlServer(connectionString));

// Contextul bazei de date pentru Identity
builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(connectionString));

// Configurare Identity cu ApplicationUser
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryIdentityContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configurarea aplicației
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
