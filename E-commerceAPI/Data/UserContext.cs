using E_commerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) {}

        public DbSet<User> Users { set; get; }
    }
}
