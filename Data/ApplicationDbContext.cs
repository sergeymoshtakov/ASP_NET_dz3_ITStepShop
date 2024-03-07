using ITStepShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Common.CommandTrees;

namespace ITStepShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
