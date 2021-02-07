using Microsoft.EntityFrameworkCore;

namespace NetCoreTest.DL
{
    public class DatabaseContext : DbContext
    {
        public const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=NetCoreTest;Trusted_Connection=True;MultipleActiveResultSets=true";

        //public DatabaseContext(DbContextOptions<DatabaseContext> options)
        //    : base(options)
        //{
        //}

        public DbSet<ItemEntity> Items { get; set; }
        
        public DbSet<CustomerEntity> Customers { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemEntity>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<ItemEntity>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Item)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CustomerEntity>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<CustomerEntity>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
