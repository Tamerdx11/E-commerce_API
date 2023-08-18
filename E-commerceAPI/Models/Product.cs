using System.ComponentModel.DataAnnotations;

namespace E_commerceAPI.Models
{
    public class Product : ProductDto
    {
        [Key]
        public int Id { get; set; }
    }
}