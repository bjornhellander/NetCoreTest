using NetCoreTest.DL.Orders;
using System;
using System.Collections.ObjectModel;

namespace NetCoreTest.DL.Items
{
    public class ItemEntity
    {
        [Obsolete("Intended for EF only")]
        public ItemEntity()
        {
        }

        public ItemEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Collection<OrderEntity> Orders { get; set; }
    }
}
