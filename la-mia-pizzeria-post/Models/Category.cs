namespace la_mia_pizzeria.Models
{
    public class Category
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public List<Pizza>? Pizzas { get; set; } //creo collegamento a molti dichiarando la lista delle entità che abbiamo collegato a molti

        public Category()
        {
            
        }
    }   
}