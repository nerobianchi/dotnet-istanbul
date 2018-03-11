using System;
using System.Collections.Generic;
using Moq;
using OrderLib;
using Xunit;

namespace TestProject3
{
    public class OrderServiceTests
    {
        [Theory]
        [MemberData(nameof(GetOrderList))]
        public void given_any_order_when_listing_then_the_number_of_orders_should_that_much(int numberOfOrders, List<Order> orders)
        {
            //arrange
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(m => m.List()).Returns(orders);
            IOrderRepository orderRepository = mockOrderRepository.Object;
            OrderService sut = new OrderService(orderRepository);

            //act
            List<Order> actuaList = sut.List();

            //assertion
            Assert.Equal(numberOfOrders, actuaList.Count);
        }

        public static object[][] GetOrderList()
        {
            return new object[][]
            {
                new object[] {1, new List<Order> {new Order()}}, new object[] {2, new List<Order> {new Order(), new Order()}}
            };
        }

        [Theory]
        [MemberData(nameof(GetExceptionalOrderList))]
        public void given_zero_order_when_listing_then_there_is_no_order_exception_is_thrown(List<Order> orders)
        {
            //arrange
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(m => m.List()).Returns(orders);
            IOrderRepository orderRepository = mockOrderRepository.Object;
            OrderService sut = new OrderService(orderRepository);

            Action action = () => sut.List();

            Assert.Throws<ThereIsNoOrderException>(action);
        }

        public static object[][] GetExceptionalOrderList()
        {
            return new[]
            {
                new object[] {new List<Order> { }}, new object[] {(List<Order>)null}
            };
        }
    }
}