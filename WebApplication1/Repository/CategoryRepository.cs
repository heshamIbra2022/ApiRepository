using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        Context context;

        public CategoryRepository(Context context)
        {
            this.context = context;
        }

        public List<Category> getAll()
        {
            return context.categories.ToList();

        }

        public Category getById(int id)
        {
            return context.categories.Include(c=>c.Product).FirstOrDefault(c => c.Id == id);
        }

        public int update(int id, Category category)
        {
            Category oldCategory = context.categories.FirstOrDefault(i => i.Id == id);
            oldCategory.Name = category.Name;

            int row = context.SaveChanges();
            return row;
        }
        public int insert(Category category)
        {
            context.categories.Add(category);
            int row = context.SaveChanges();
            return row;


        }
        public int delete(int id)
        {
            Category crs = getById(id);
            context.categories.Remove(crs);
            int rows = context.SaveChanges();
            return rows;

        }
    }
}
