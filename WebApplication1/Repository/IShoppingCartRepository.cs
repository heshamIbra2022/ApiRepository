using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IShoppingCartRepository
    {
        List<ShoppingOrderProducts> getAll();
        public ShoppingOrderProducts getById(int id);
        List<ShoppingOrderProducts> getprdbyOrderId(int orderId);
        public ShoppingOrderProducts getByprdId(int id);
        public int insert(ShoppingOrderProducts shoppingOrderProduct);
        public int update(int id, ShoppingOrderProducts crs);
        public int delete(int id);
    }
}