using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IProductRepository
    {
        int delete(int id);
        List<Product> getAll();
        public List<Product> getprdbyCatId(int catid);
        Product getById(int id);
        int insert(Product product);
        int update(int id, Product crs);
        public int adminupdate(int id, Product crs);
        Product getByName(string Name);
    }
}