namespace NetCoreTest.DL
{
    public class ItemRepositoryData
    {
        public ItemRepositoryData(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}
