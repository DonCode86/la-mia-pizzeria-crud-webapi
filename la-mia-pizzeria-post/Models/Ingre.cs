using la_mia_pizzeria.Models;

namespace la_mia_pizzeria_static.Models
{
    public class Ingre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Pizza>? Pizzas { get; set; }

        public Ingre()
        {

        }
    }
}
