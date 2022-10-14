using la_mia_pizzeria.Models;
using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria.Models
{
    public class PizzasCategories
    {
        //pizza che avevamo gia'
        public Pizza Pizza { get; set; }

        public  List<Category> Categories { get; set; }

        public List<Ingre> Ingres { get; internal set; }

        public List<int> SelectedIngres { get; set; }

        public PizzasCategories()
        {
            Pizza = new Pizza();
            Categories = new List<Category>();
            Ingres = new List<Ingre>();
            SelectedIngres = new List<int>();
        }
    }
}

//questo e' il modello della nostra vista