using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Context:IdentityDbContext<ApplicationUser>
    {
        public Context()
        {

        }
        public Context(DbContextOptions options) : base(options)
        {
           
        }


        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlServer(@"Data Source=DESKTOP-354JS6L\SQLEXPRESS;Initial Catalog=MyProducts;Integrated Security=True");

        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product>  products { get; set; }
        public DbSet<ShoppingOrderProducts>  shoppingOrders { get; set; }
        public DbSet<UserOrder> userOrders { get; set; }
    }
}
