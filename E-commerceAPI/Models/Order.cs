using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace E_commerceAPI.Models
{
    public class Order : OrderDto
    {
        [Key]
        public int Id { get; set; }
    }
}
