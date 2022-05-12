using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        Context context;

        public ShoppingCartRepository(Context context)
        {
            this.context = context;
        }
        public List<ShoppingOrderProducts> getAll()
        {
            return context.shoppingOrders.ToList();

        }
        public ShoppingOrderProducts getById(int id)
        {
            return context.shoppingOrders.FirstOrDefault(c => c.Id == id);
        }
        public ShoppingOrderProducts getByprdId(int id)
        {
            return context.shoppingOrders.FirstOrDefault(c => c.ProductId == id);
        }
        public List<ShoppingOrderProducts> getprdbyOrderId(int orderId)
        {
            return context.shoppingOrders.Where(s => s.UserOrderId == orderId).ToList();

        }

        public int insert(ShoppingOrderProducts  shoppingOrderProduct)
        {
            ShoppingOrderProducts oldshopOrder= getByprdId(shoppingOrderProduct.ProductId);
            if (oldshopOrder == null)
            {
                context.shoppingOrders.Add(shoppingOrderProduct);
                int row = context.SaveChanges();
                return row;
            }
            else {
                if (oldshopOrder.UserOrderId == shoppingOrderProduct.UserOrderId)
                {
                    return update(oldshopOrder.Id, shoppingOrderProduct);
                }
                else
                {

                    context.shoppingOrders.Add(shoppingOrderProduct);
                    int row = context.SaveChanges();
                    return row;
                }


            }
           

               




        }

        public int update(int id, ShoppingOrderProducts crs)
        {
            ShoppingOrderProducts oldprderProduct = context.shoppingOrders.FirstOrDefault(i => i.Id == id);
            if (oldprderProduct.UserOrderId == crs.UserOrderId)
            {
                oldprderProduct.quantity += crs.quantity;
                oldprderProduct.Price = crs.Price;

            }
           

            int row = context.SaveChanges();
            return row;
        }

        public int delete(int id)
        {
            ShoppingOrderProducts crs = getById(id);
            context.shoppingOrders.Remove(crs);
            int rows = context.SaveChanges();
            return rows;

        }
    }
}
