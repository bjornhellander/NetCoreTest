using Microsoft.EntityFrameworkCore;
using NetCoreTest.DL.Customers;
using NetCoreTest.DL.Items;
using NetCoreTest.DL.Orders;
using System.Data.Common;

namespace NetCoreTest.DL
{
    public class DatabaseContext : DbContext
    {
        public const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=NetCoreTest;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly DbConnection connection;

        public DatabaseContext()
        {
            connection = null;
        }

        public DatabaseContext(DbConnection connection)
        {
            this.connection = connection;
        }

        //public DatabaseContext(DbContextOptions<DatabaseContext> options)
        //    : base(options)
        //{
        //}

        public DbSet<ItemEntity> Items { get; set; }
        
        public DbSet<CustomerEntity> Customers { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (connection != null)
            {
                options.UseSqlServer(connection);
            }
            else
            {
                options.UseSqlServer(ConnectionString);
            }
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
