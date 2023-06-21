using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        // * holds reference to a PizzaService object - can't be changed after set in ctor
        private readonly PizzaService _service;
        // * will hold a list of Pizza objects
        // * initialized to default! to indicate to compiler that it will be initialized later, so null safety checks aren't required
        public IList<Pizza> PizzaList { get; set; } = default!;

        // * PizzaService object is provided by dependency injection (registered in Program.cs via builder.services.AddScoped<PizzaService>())
        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }
        // * retrieves list of pizzas from PizzaService object and stores it
        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }
    }
}
