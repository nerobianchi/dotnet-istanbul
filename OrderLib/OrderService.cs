using System.Collections.Generic;

namespace OrderLib
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public List<Order> List()
        {
            var orders = this.orderRepository.List();
            if (orders == null || orders.Count == 0)
            {
                throw new ThereIsNoOrderException();
            }

            return orders;
        }
    }
}