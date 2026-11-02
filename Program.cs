
using Microsoft.EntityFrameworkCore;
using Wilson_Movies_Project.Data;
using Wilson_Movies_Project.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MVC controllers and views to the application.
builder.Services.AddControllersWithViews();

// Register the SQLite database context with dependency injection.
builder.Services.AddDbContext<WilsonMoviesContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("WilsonMoviesContext")
        ?? throw new InvalidOperationException(
            "Connection string 'WilsonMoviesContext' not found."
        )
    )
);

var app = builder.Build();

// Create a service scope to initialize the database and sample records.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Obtain the database context from dependency injection.
    var context = services.GetRequiredService<WilsonMoviesContext>();

    // Create or update the database according to the EF Core migrations.
    context.Database.Migrate();

    // Insert sample movies if the database is empty.
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Preserve the MVC default controller route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id:int?}");


app.Run();
