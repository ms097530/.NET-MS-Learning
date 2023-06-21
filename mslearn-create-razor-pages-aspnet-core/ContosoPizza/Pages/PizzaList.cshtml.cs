using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        // * BindProperty attribute is used to bind the NewPizza property to the Razor page... when an HTTP POST request is made, the NewPizza property will be populated with the user's input
        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;

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

        public IActionResult OnPost()
        {
            // * ModelState.IsValid is based on validation rules inferred from attributes (such as Required and Range) on the Pizza class in Models/Pizza.cs
            // ! if invalid, re-render the page
            if (!ModelState.IsValid || NewPizza == null)
            {
                return Page();
            }

            // * add pizza using _service object and re-render page
            _service.AddPizza(NewPizza);
            return RedirectToAction("Get");
        }

        // ? with current setup, name must be OnPostDelete (because embedded in form with method post and asp-page-handler tag helper set to "Delete")
        // * id is passed via asp-route-id tag helper
        public IActionResult OnPostDelete(int id)
        {
            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }
    }
}
