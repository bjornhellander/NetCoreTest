using NetCoreTest.DL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetCoreTest.UI
{
    public class MainViewModel
    {
        private readonly ItemRepositoryService itemRepositoryService;
        private readonly CustomerRepositoryService customerRepositoryService;
        private readonly OrderRepositoryService orderRepositoryService;

        [Obsolete("Design-time constructor")]
        public MainViewModel()
        {
            AddDesignTimeData();
        }

        public MainViewModel(
            ItemRepositoryService itemRepositoryService,
            CustomerRepositoryService customerRepositoryService,
            OrderRepositoryService orderRepositoryService)
        {
            this.itemRepositoryService = itemRepositoryService;
            this.customerRepositoryService = customerRepositoryService;
            this.orderRepositoryService = orderRepositoryService;

            LoadCommand = new AsyncCommand(DoLoadData);
        }

        public ObservableCollection<ItemViewModel> Items { get; } = new ObservableCollection<ItemViewModel>();

        public ObservableCollection<CustomerViewModel> Customers { get; } = new ObservableCollection<CustomerViewModel>();

        public ObservableCollection<OrderViewModel> Orders { get; } = new ObservableCollection<OrderViewModel>();

        public ICommand LoadCommand { get; }

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
            var result = new List<ItemViewModel>();
            result.Add(new ItemViewModel(1, "Sax"));
            result.Add(new ItemViewModel(2, "Kniv"));
            return result;
        }

        private List<CustomerViewModel> CreateDesignTimeCustomers()
        {
            var result = new List<CustomerViewModel>();
            result.Add(new CustomerViewModel(1, "Kalle"));
            result.Add(new CustomerViewModel(2, "Adam"));
            return result;
        }

        private List<OrderViewModel> CreateDesignTimeOrders(List<ItemViewModel> items, List<CustomerViewModel> customers)
        {
            var result = new List<OrderViewModel>();
            result.Add(new OrderViewModel(1, customers[0], items[0], 11));
            result.Add(new OrderViewModel(2, customers[1], items[0], 22));
            result.Add(new OrderViewModel(3, customers[0], items[1], 33));
            return result;
        }

        private async Task DoLoadData()
        {
            var items = await itemRepositoryService.GetAllItemsAsync();
            var customers = await customerRepositoryService.GetAllCustomersAsync();
            var orders = await orderRepositoryService.GetAllOrdersAsync();

            SetData(items, customers, orders);
        }

        private void SetData(
            List<ItemRepositoryData> items,
            List<CustomerRepositoryData> customers,
            List<OrderRepositoryData> orders)
        {
            var itemViewModels = new List<ItemViewModel>();
            foreach (var item in items)
            {
                var itemViewModel = new ItemViewModel(item.Id, item.Name);
                itemViewModels.Add(itemViewModel);
            }

            var customerViewModels = new List<CustomerViewModel>();
            foreach (var customer in customers)
            {
                var customerViewModel = new CustomerViewModel(customer.Id, customer.Name);
                customerViewModels.Add(customerViewModel);
            }

            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var itemVm = itemViewModels.Single(x => x.Id == order.ItemId);
                var customerVm = customerViewModels.Single(x => x.Id == order.CustomerId);

                var orderViewModel = new OrderViewModel(order.Id, customerVm, itemVm, order.Amount);
                orderViewModels.Add(orderViewModel);
            }

            Items.Clear();
            Items.AddRange(itemViewModels);

            Customers.Clear();
            Customers.AddRange(customerViewModels);

            Orders.Clear();
            Orders.AddRange(orderViewModels);
        }
    }
}
