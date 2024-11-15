using Microsoft.EntityFrameworkCore;
using TrubyiPakety.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TrubyiPaketyDbContext>(options =>
    options.UseNpgsql("Host=postgres;Port=5432;Database=trubyipaketyDb;Username=shamil;Password=shamil1998"));
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TrubyiPaketyDbContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pipes}/{action=Index}/{id?}");

app.Run();
