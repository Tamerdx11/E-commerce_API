using E_commerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
