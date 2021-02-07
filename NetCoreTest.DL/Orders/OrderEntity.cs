using NetCoreTest.DL.Customers;
using NetCoreTest.DL.Items;
using System;

namespace NetCoreTest.DL.Orders
{
    public class OrderEntity
    {
        [Obsolete("Intended for EF only")]
        public OrderEntity()
        {
        }

        public OrderEntity(int id, int customerId, int itemId, int amount)
        {
            Id = id;
            CustomerId = customerId;
            ItemId = itemId;
            Amount = amount;
        }

        public int Id { get; set; }

        public CustomerEntity Customer { get; set; }

        public int CustomerId { get; set; }

        public ItemEntity Item { get; set; }

        public int ItemId { get; set; }

        public int Amount { get; set; }
    }
}
