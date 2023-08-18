using E_commerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Repository.Base
{
    public interface IOrder
    {
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetForUser(int userId);
        Task<Order> Add(Order order);
        void Delete(int orderId);

    }
}
