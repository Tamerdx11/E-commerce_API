using System.ComponentModel.DataAnnotations;

namespace E_commerceAPI.Models
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Url]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public int Price { get; set; }
    }
}
