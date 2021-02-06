namespace NetCoreTest.DL
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public CustomerEntity Customer { get; set; }

        public int CustomerId { get; set; }

        public ItemEntity Item { get; set; }

        public int ItemId { get; set; }

        public int Amount { get; set; }
    }
}
