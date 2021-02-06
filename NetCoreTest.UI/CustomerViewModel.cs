namespace NetCoreTest.UI
{
    public class CustomerViewModel
    {
        public CustomerViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}
