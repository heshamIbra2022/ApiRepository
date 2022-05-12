using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IUserOrderRepository
    {
        int delete(int id);
        List<UserOrder> getAll();
        UserOrder getById(int id);
        UserOrder getByUserId(string userid);
        public int insert(UserOrder userOrder);
    }
}