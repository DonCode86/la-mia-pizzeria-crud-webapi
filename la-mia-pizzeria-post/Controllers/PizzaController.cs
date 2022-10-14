using la_mia_pizzeria.Models;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace la_mia_pizzeria.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //using (PizzaContext context = new PizzaContext())
            //{
            //    //MI RECUPERO DAL CONTEXT LA LISTA DELLE PIZZE
            //    IQueryable<Pizza> pizzas = context.Pizzas;
            //    //E LI PASSO ALLA VISTA
            //    return View("Index", pizzas.ToList());  
            //}
            using (PizzaContext context = new PizzaContext())
            {
                List<Pizza> pizzas = context.Pizzas.Include("Category").Include("Ingres").ToList();

                return View("Index", pizzas);
            }
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

        //QUESTA CREATE E' LA NOSTRA GET CHE PRODUCE IL FORM CHE DOVRA' ESSERE POST
        [HttpGet]
        public IActionResult Create()
        {
            PizzasCategories pizzasCategories = new PizzasCategories();

            PizzaContext pizzaContext = new PizzaContext();

            pizzasCategories.Categories = pizzaContext.Categories.ToList(); 

            pizzasCategories.Ingres = pizzaContext.Ingres.ToList();

            return View(pizzasCategories); //porto il dato all'interno della vista
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzasCategories formData) //
        {
            PizzaContext db = new PizzaContext();

            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();//popolo le categorie
                formData.Ingres = db.Ingres.ToList();//popolo le categorie
                return View("Create", formData);
            }

            //using (PizzaContext context = new PizzaContext())
            //{
            //    //AGGIUNGO I DATI NEL FORMDATA AL DB E SALVO I CAMBIAMENTI
            //    db.Pizzas.Add(formData);
            //    db.SaveChanges();
            //    //RITORNA ALLA LISTA DEI POST
            //    return RedirectToAction("Index");
            //}

            formData.Pizza.Ingres = db.Ingres.Where(ingre => formData.SelectedIngres.Contains(ingre.Id)).ToList<Ingre>();  //lego le pizze agli ingredienti

            db.Pizzas.Add(formData.Pizza);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            //richiamo il db
            using (PizzaContext pizzaContext = new PizzaContext())//gli passo il model
            {
                Pizza pizzaToEdit = pizzaContext.Pizzas.Include("Category").Include("Ingres").
                    Where(pizza => pizza.Id == id).FirstOrDefault();//vado a prendere dalle istanze del db la pizza

                if (pizzaToEdit == null)
                {
                    return NotFound("Non trovato");
                }

                PizzasCategories pizzasCategories = new PizzasCategories();

                pizzasCategories.Pizza = pizzaToEdit;

                pizzasCategories.Categories = pizzaContext.Categories.ToList();
                pizzasCategories.Ingres = pizzaContext.Ingres.ToList();

                return View(pizzasCategories); //recupero la pizza tramite id e lo restituisco alla vista 
           
            }
                     
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //metodo che gestisce i dati del form (gli passo l'id della pizza, e il modello con i dati inseriti nel form(formData))
        public IActionResult Update(int id, PizzasCategories formData)
        {
            using (PizzaContext pizzaContext = new PizzaContext())//richiamo il db
            {
                if (!ModelState.IsValid)
                {
                    formData.Categories = pizzaContext.Categories.ToList();
                    formData.Ingres = pizzaContext.Ingres.ToList();
                    return View("Update", formData);
                }

                Pizza pizza = pizzaContext.Pizzas.Where(pizza => pizza.Id == id).Include("Ingres").FirstOrDefault();

                pizza.Name = formData.Pizza.Name;
                pizza.Ingredients = formData.Pizza.Ingredients;
                pizza.CategoryId = formData.Pizza.CategoryId;
                pizza.Ingres = pizzaContext.Ingres.Where(tag => formData.SelectedIngres.Contains(tag.Id)).ToList<Ingre>();

                pizzaContext.SaveChanges();

                return RedirectToAction("Index");//.net costruisce l'url 302/200
            }
            //Pizza pizza = pizzaContext.Pizzas.Where(pizzaContext=>pizzaContext.Id == id).FirstOrDefault();//mi riprendo i dati da modificare
            //// e li modifico
            //if (pizza != null)
            //{
            //    pizza.Name = formData.Name;
            //    pizza.Ingredients = formData.Ingredients;
            //    pizza.Price = formData.Price;
            //    pizza.Image = formData.Image;
            //} else
            //{
            //    return NotFound("Non trovato");
            //}

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //gli passo l'id dell'oggetto che voglio eliminare come parametro
        public IActionResult Delete(int id) 
        {
            PizzaContext pizzaContext = new PizzaContext();
            Pizza pizza = pizzaContext.Pizzas.Where(pizza=> pizza.Id == id).FirstOrDefault();

            if (pizza == null)
            {
                return NotFound("Non trovato");
            }
            pizzaContext.Pizzas.Remove(pizza);
            pizzaContext.SaveChanges();

            return RedirectToAction("Index");
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