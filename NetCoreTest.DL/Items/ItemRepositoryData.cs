namespace NetCoreTest.DL.Items
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
