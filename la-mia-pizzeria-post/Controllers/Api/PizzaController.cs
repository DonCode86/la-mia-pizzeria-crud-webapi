using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        PizzaContext _ctx = new PizzaContext();

        public PizzaController()
        {
            _ctx = new PizzaContext();
        }
        public IActionResult Get()
        {
            List<Pizza> pizzas = _ctx.Pizzas.ToList();
            return Ok(pizzas);
        }
    }
}
