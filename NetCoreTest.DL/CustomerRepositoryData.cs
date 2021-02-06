namespace NetCoreTest.DL
{
    public class CustomerRepositoryData
    {
        public CustomerRepositoryData(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}
