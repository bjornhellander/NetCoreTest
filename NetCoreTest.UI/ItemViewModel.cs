namespace NetCoreTest.UI
{
    public class ItemViewModel
    {
        public ItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}
