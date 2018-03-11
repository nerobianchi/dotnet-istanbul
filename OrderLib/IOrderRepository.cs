using System.Collections.Generic;

namespace OrderLib
{
    public interface IOrderRepository
    {
        List<Order> List();
    }
}