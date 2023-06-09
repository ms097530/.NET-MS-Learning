using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models
{
    // * entity class - identifies pizzas
    public class Pizza
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    // * context objects representing a session with the DB - allows querying and saving data
    // * responsible for querying and saving data to entity classes and for creating and managing DB connection
    class PizzaDb : DbContext
    // * DbContext represents a connection or sessions that's used to query and save instances of entitites in a database
    {
        public PizzaDb(DbContextOptions options) : base(options) { }
        public DbSet<Pizza> Pizzas { get; set; } = null!;
    }
}