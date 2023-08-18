using E_commerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }
        public DbSet<Order> Orders { set; get; }
    }
}
