namespace NetCoreTest.DL
{
    public class OrderRepositoryData
    {
        public OrderRepositoryData(int id, int customerId, int itemId, int amount)
        {
            Id = id;
            CustomerId = customerId;
            ItemId = itemId;
            Amount = amount;
        }

        public int Id { get; }

        public int CustomerId { get; }

        public int ItemId { get; }

        public int Amount { get; }
    }
}
