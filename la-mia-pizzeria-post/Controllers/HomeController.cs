using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Diagnostics;

namespace la_mia_pizzeria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            using (PizzaContext context = new PizzaContext())
            {
                //FACCIO RICHIESTA DELLE PIZZE ANDANDO A SELEZIONARE LA PIZZA SPECIFICA
                //pizzaFound e' LINQ (questa e' la method syntax)
                Pizza pizzaFound = context.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).Include(pizza => pizza.Ingres).FirstOrDefault();
                //SE IL POST NON VIENE TROVATO
                if (pizzaFound == null)
                {
                    return NotFound($"La pizza con id {id} non è stato trovata");
                }
                else //ALTRIMENTI VIENE PASSATO ALLA VISTA DI DETTAGLIO CON PIZZAFOUND
                {
                    return View("Details", pizzaFound);
                }
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}