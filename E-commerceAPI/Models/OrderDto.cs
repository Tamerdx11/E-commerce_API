using System.ComponentModel.DataAnnotations;

namespace E_commerceAPI.Models
{
    public class OrderDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
    }
}
