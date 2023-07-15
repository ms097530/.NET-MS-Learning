using BlazingPizza.Data;

var builder = WebApplication.CreateBuilder(args);

// namespace Testspace
// {
//     public class Test { };
//     public class MyTest : Test { };
// }


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// builder.Services.AddScoped<Testspace.Test, Testspace.MyTest>();
// allows app to access HTTP commands
// app uses an HttpClient to get JSON for pizza specials
builder.Services.AddHttpClient();
// registers PizzaStoreContext and provides filename for SQLite db
builder.Services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

app.Run();