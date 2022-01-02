using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using E_commerceProject.data.Abstract;
using E_commerceProject.entity;

namespace E_commerceProject.business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

        }

        public int Count(int UserId)
        {
            return _orderRepository.Count(UserId);
        }

        public void Create(Order order)
        {
            _orderRepository.Create(order);
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }

        public void DeleteOrder(int id, int ProductId)
        {
            _orderRepository.DeleteOrder(id, ProductId);
        }

        public List<entity.Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public List<Order> GetsById(int id)
        {
            return _orderRepository.GetsById(id);
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }
        public void SetQuantity(int UserId, int ProductId, int Quantity)
        {
            _orderRepository.SetQuantity(UserId, ProductId, Quantity);
        }
        public void CreateOrderHistory(OrderHistory orderHistory)
        {
            _orderRepository.CreateOrderHistory(orderHistory);
        }
        public List<OrderHistory> getOrderHistoryList(int userId)
        {
            return _orderRepository.getOrderHistoryList(userId);
        }
    }
}