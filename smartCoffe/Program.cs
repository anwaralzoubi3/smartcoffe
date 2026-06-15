using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// ===================== MVC =====================
builder.Services.AddControllersWithViews();

// ===================== DATABASE =====================
builder.Services.AddDbContext<SmartCoffeeDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ===================== SESSION =====================
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ===================== SEED DATABASE =====================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SmartCoffeeDbContext>();

    db.Database.EnsureCreated();

    // ===================== SEED PLANS =====================
    if (!db.Plans.Any())
    {
        db.Plans.AddRange(
            new Plan
            {
                PlanName = "Basic",
                CoffeeShopName = "Star Coffee",
                CupsPerMonth = 10,
                Price = 15,
                IsActive = true
            },
            new Plan
            {
                PlanName = "Standard",
                CoffeeShopName = "Arabica Cafe",
                CupsPerMonth = 20,
                Price = 25,
                IsActive = true
            },
            new Plan
            {
                PlanName = "Premium",
                CoffeeShopName = "Coffee Bean",
                CupsPerMonth = 30,
                Price = 35,
                IsActive = true
            }
        );

        db.SaveChanges();
    }

    // ===================== SEED COFFEE PRODUCTS =====================
    if (!db.CoffeeProducts.Any())
    {
        db.CoffeeProducts.AddRange(
            new CoffeeProduct
            {
                ProductName = "Espresso",
                Description = "Strong Coffee",
                CupsCost = 1,
                IsActive = true
            },
            new CoffeeProduct
            {
                ProductName = "Latte",
                Description = "Milk Coffee",
                CupsCost = 1,
                IsActive = true
            },
            new CoffeeProduct
            {
                ProductName = "Cappuccino",
                Description = "Foamy Coffee",
                CupsCost = 1,
                IsActive = true
            },
            new CoffeeProduct
            {
                ProductName = "Americano",
                Description = "Classic Coffee",
                CupsCost = 1,
                IsActive = true
            },
            new CoffeeProduct
            {
                ProductName = "Mocha",
                Description = "Chocolate Coffee",
                CupsCost = 2,
                IsActive = true
            }
        );

        db.SaveChanges();
    }
}

// ===================== PIPELINE =====================
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Splash}/{id?}");

app.Run();