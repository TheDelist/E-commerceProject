using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.data.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        int Count(int UserId);
        List<Order> GetsById(int id);

        void DeleteOrder(int id, int ProductId);
        void SetQuantity(int UserId, int ProductId, int Quantity);
        List<OrderHistory> getOrderHistoryList(int userId);
        void CreateOrderHistory(OrderHistory orderHistory);
    }
}