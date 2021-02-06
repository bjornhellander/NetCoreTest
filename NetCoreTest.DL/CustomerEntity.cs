using System.Collections.ObjectModel;

namespace NetCoreTest.DL
{
    public class CustomerEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Collection<OrderEntity> Orders { get; set; }
    }
}
