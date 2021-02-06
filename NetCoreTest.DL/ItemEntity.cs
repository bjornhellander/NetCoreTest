using System.Collections.ObjectModel;

namespace NetCoreTest.DL
{
    public class ItemEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Collection<OrderEntity> Orders { get; set; }
    }
}
