using HPlusSport.API.Controllers;

namespace HPlusSport.API.Models
{
    public class Category
    {
        public int Id { get; set; } 
        public string Name { get; set; } = String.Empty;  // just because it's not nullable right now

        public virtual List<Product> Products
        {
            get; set;
        }
    }
}
