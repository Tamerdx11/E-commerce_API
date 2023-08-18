using E_commerceAPI.Data;
using E_commerceAPI.Models;
using E_commerceAPI.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.Repository
{
    public class OrderRepository : IOrder
    {
        private OrderContext _orderContext;

        public OrderRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderContext.Set<Order>().ToList();
        }
        public async Task<Order> Add(Order order)
        {
            await _orderContext.AddAsync(order);
            await _orderContext.SaveChangesAsync();
            return order;
        }

        public void Delete(int orderId)
        {
            _orderContext.Orders!.Where(o => o.Id == orderId).ExecuteDelete();
            _orderContext.SaveChanges();
        }

        public IEnumerable<Order> GetForUser(int userId)
        {
            var orders = from o in _orderContext.Orders select o;
            orders = orders.Where(o => o.UserId == userId);
            return orders.ToList();
        }

    }
}
