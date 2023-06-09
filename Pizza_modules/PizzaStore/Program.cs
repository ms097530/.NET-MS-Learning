using System.Collections.Immutable;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
// using PizzaStore.DB;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSqlite<PizzaDb>(connectionString);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pizza Store API", Description = "Mama mia!", Version = "v1" });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
});

app.MapGet("/", () => "Hello World!");
app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());
app.MapPost("/pizza", async (PizzaDb db, Pizza pizza) =>
{
    // ? https://stackoverflow.com/questions/42034282/are-there-dbset-updateasync-and-removeasync-in-net-core
    // * sounds like using AddAsync should be avoided
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    // * returns first arg as Location response header
    // * returns second arg as JSON representing created data
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});
app.MapGet("/pizza/{id}", async (int id, PizzaDb db) =>
{
    // return await db.Pizzas.FindAsync(id);
    var pizza = await db.Pizzas.FindAsync(id);

    if (pizza is null)
    {
        return Results.NotFound();
    }

    return Results.Json(pizza);

    // ! gives error 
    // return pizza;
});
app.MapPut("/pizza/{id}", async (int id, PizzaDb db, Pizza updatePizza) =>
{
    // find pizza
    // update parts you want to update
    // save changes to db
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    pizza.Name = updatePizza.Name;
    pizza.Description = updatePizza.Description;
    // db.Pizzas.Update(pizza);
    await db.SaveChangesAsync();
    return Results.Json(new { Status = 200, Message = "Pizza updated" });
});
app.MapDelete("/pizza/{id}", async (int id, PizzaDb db) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();

    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});

// * using Db.cs
// app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
// app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
// app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
// app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
// app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));

app.Run();
