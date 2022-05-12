using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class UserOrderRepository : IUserOrderRepository
    {
        Context context;

        public UserOrderRepository(Context context)
        {
            this.context = context;
        }

        public List<UserOrder> getAll()
        {
            return context.userOrders.ToList();

        }
        public UserOrder getById(int id)
        {
            return context.userOrders.Include(u => u.ShoppingOrderProducts).FirstOrDefault(c => c.Id == id);
        }
        public UserOrder getByUserId(string userId)
        {
            return context.userOrders.Include(u => u.ShoppingOrderProducts).FirstOrDefault(d => d.ApplicationUserId.Contains( userId)); ;
        }
        public int insert(UserOrder userOrder)
        {
           UserOrder userOrder1= getByUserId(userOrder.ApplicationUserId);
            if(userOrder1 != null)
            {
                return 1;
            }
            else
            {
 context.userOrders.Add(userOrder);
            int row = context.SaveChanges();
            return row;

            }
           

        }
        public int delete(int id)
        {
            UserOrder oldorder = getById(id);
            context.userOrders.Remove(oldorder);
            int rows = context.SaveChanges();
            return rows;

        }
    }

}
