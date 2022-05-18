using HPlusSport.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HPlusSport.API.Controllers
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Sku { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        
        [Required]
        public int CategoryId { get; set; } = 0;

        [JsonIgnore]  // does a category have a product?  does a product have a category?
        public virtual Category? Category  
        {
            get; set;
        }

    }
}
