using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ProductRepository : IProductRepository
    {
        Context context;

        public ProductRepository(Context context)
        {
            this.context = context;
        }

        public List<Product> getAll()
        {
            return context.products.Include(c=>c.Category).ToList();

        }

        public Product getById(int id)
        {
            return context.products.Include(p =>p.Category).FirstOrDefault(c => c.Id == id);
        }

        public List<Product> getprdbyCatId(int catid)
        {
            return context.products.Where(p=>p.CategoryId== catid).ToList();

        }
        public Product getByName(string Name)
        {
            return context.products.FirstOrDefault(c => c.Name == Name);
        }
        public int update(int id, Product crs)
        {
            Product oldcrs = context.products.FirstOrDefault(i => i.Id == id);
            oldcrs.quantity -= crs.quantity;

            int row = context.SaveChanges();
            return row;
        }
        public int adminupdate(int id, Product crs)
        {
            Product oldcrs = context.products.FirstOrDefault(i => i.Id == id);
            oldcrs.CategoryId = crs.CategoryId;
            oldcrs.Name = crs.Name;
            oldcrs.quantity = crs.quantity;
            oldcrs.Price = crs.Price;

            int row = context.SaveChanges();
            return row;
        }
        public int insert(Product product)
        {
            context.products.Add(product);
            int row = context.SaveChanges();
            return row;


        }
        public int delete(int id)
        {
            Product crs = getById(id);
            context.products.Remove(crs);
            int rows = context.SaveChanges();
            return rows;

        }
    }
}
