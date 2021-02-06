using System;
using System.Collections.ObjectModel;

namespace NetCoreTest.DL
{
    public class CustomerEntity
    {
        [Obsolete("Intended for EF only")]
        public CustomerEntity()
        {
        }

        public CustomerEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Collection<OrderEntity> Orders { get; set; }
    }
}
