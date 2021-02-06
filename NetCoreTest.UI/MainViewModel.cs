using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetCoreTest.UI
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            AddDesignTimeData();
        }

        public ObservableCollection<ItemViewModel> Items { get; } = new ObservableCollection<ItemViewModel>();

        public ObservableCollection<CustomerViewModel> Customers { get; } = new ObservableCollection<CustomerViewModel>();

        public ObservableCollection<OrderViewModel> Orders { get; } = new ObservableCollection<OrderViewModel>();

        private void AddDesignTimeData()
        {
            var items = CreateDesignTimeItems();
            Items.AddRange(items);

            var customers = CreateDesignTimeCustomers();
            Customers.AddRange(customers);

            var orders = CreateDesignTimeOrders(items, customers);
            Orders.AddRange(orders);
        }

        private List<ItemViewModel> CreateDesignTimeItems()
        {
            var items = new List<ItemViewModel>();
            items.Add(new ItemViewModel(1, "Sax"));
            items.Add(new ItemViewModel(1, "Kniv"));
            return items;
        }

        private List<CustomerViewModel> CreateDesignTimeCustomers()
        {
            var customers = new List<CustomerViewModel>();
            customers.Add(new CustomerViewModel(1, "Kalle"));
            customers.Add(new CustomerViewModel(1, "Adam"));
            return customers;
        }

        private List<OrderViewModel> CreateDesignTimeOrders(List<ItemViewModel> items, List<CustomerViewModel> customers)
        {
            var orders = new List<OrderViewModel>();
            orders.Add(new OrderViewModel(1, customers[0], items[0], 11));
            orders.Add(new OrderViewModel(2, customers[1], items[0], 22));
            orders.Add(new OrderViewModel(3, customers[0], items[1], 33));
            return orders;
        }
    }
}
