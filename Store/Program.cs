using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Store;
using Store.Authentication;

var builder = WebApplication.CreateBuilder(args);

// database connection string SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<Store.Data.StoreContext>(options => options.UseSqlite(connectionString));
var authenticationConnectionString = builder.Configuration.GetConnectionString("AuthenticationConnectionString");
builder.Services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(authenticationConnectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
DbInitializer.Seed(app);
app.Run();
