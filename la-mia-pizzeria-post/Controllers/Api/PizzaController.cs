using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
        [HttpGet]
        public IActionResult Get(string? name)
        {
            IQueryable<Pizza> pizzas;

            if (name != null)
            {
                pizzas = _ctx.Pizzas.Where(pizza => pizza.Name.ToLower().Contains(name.ToLower()));
            }
            else
            {
                pizzas = _ctx.Pizzas;
            }

            return Ok(pizzas.ToList());
        }


        //api/post/get/[qualqune numero]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Pizza pizza = _ctx.Pizzas.Where(p => p.Id == id).FirstOrDefault();

            return Ok(pizza);
        }

    }
}
