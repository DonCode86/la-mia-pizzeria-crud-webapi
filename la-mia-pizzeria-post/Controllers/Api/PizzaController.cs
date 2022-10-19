using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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


       
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Pizza pizza = _ctx.Pizzas.Where(p => p.Id == id).FirstOrDefault();



            return Ok(pizza);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Pizza pizza = _ctx.Pizzas.Where(p => p.Id == id).FirstOrDefault();

            if (pizza == null)
            {
                return NotFound(new {Message="Pizza non trovata"});
            }
            try
            {
                _ctx.Pizzas.Remove(pizza);
                _ctx.SaveChanges();
            }catch (SqlException e)
            {
                return UnprocessableEntity(new { Message = e.Message });
            }
          
            return Ok(new { Message = "Scusa Paolo, Pizza eliminata" });
        }
    }

}
