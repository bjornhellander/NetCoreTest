namespace NetCoreTest.UI
{
    public class OrderViewModel
    {
        public OrderViewModel(int id, CustomerViewModel customerViewModel, ItemViewModel itemViewModel, int amount)
        {
            Id = id;
            ItemViewModel = itemViewModel;
            CustomerViewModel = customerViewModel;
            Amount = amount;
        }

        public int Id { get; }

        public CustomerViewModel CustomerViewModel { get; }

        public ItemViewModel ItemViewModel { get; }

        public int Amount { get; }
    }
}
