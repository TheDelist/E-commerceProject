using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.business.Abstract
{
    public interface IOrderService
    {
        void Create(Order order);
        void Delete(int id);
        void DeleteOrder(int id, int ProductId);
        void Update(Order entity);
        List<Order> GetsById(int id);
        int Count(int UserId);
        List<Order> GetAll();

        void SetQuantity(int UserId, int ProductId, int Quantity);
        void CreateOrderHistory(OrderHistory orderHistory);

        List<OrderHistory> getOrderHistoryList(int userId);

    }
}