using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ICategoryRepository
    {
        int delete(int id);
        List<Category> getAll();
        Category getById(int id);
        int insert(Category category);
        int update(int id, Category category);
    }
}