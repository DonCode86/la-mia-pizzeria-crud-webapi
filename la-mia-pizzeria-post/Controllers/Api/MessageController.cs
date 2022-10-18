using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public IActionResult Send([FromBody] Message message)
        {
            PizzaContext ctx = new PizzaContext();

            ctx.Messages.Add(message);
            ctx.SaveChanges();

            return Ok();
        }
    }
}
