using NetCoreTest.DL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetCoreTest.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IItemRepositoryService itemRepositoryService;
        private readonly ICustomerRepositoryService customerRepositoryService;
        private readonly IOrderRepositoryService orderRepositoryService;
        private bool isEnabled = true;

        [Obsolete("Design-time constructor")]
        public MainViewModel()
        {
            AddDesignTimeData();
        }

        public MainViewModel(
            IItemRepositoryService itemRepositoryService,
            ICustomerRepositoryService customerRepositoryService,
            IOrderRepositoryService orderRepositoryService)
        {
            this.itemRepositoryService = itemRepositoryService;
            this.customerRepositoryService = customerRepositoryService;
            this.orderRepositoryService = orderRepositoryService;

            LoadCommand = new AsyncCommand(DoLoadDataAsync);
            GenerateCommand = new AsyncCommand(DoGenerateDataAsync);
            ResetCommand = new AsyncCommand(DoResetDataAsync);
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }

        public ObservableCollection<ItemViewModel> Items { get; } = new ObservableCollection<ItemViewModel>();

        public ObservableCollection<CustomerViewModel> Customers { get; } = new ObservableCollection<CustomerViewModel>();

        public ObservableCollection<OrderViewModel> Orders { get; } = new ObservableCollection<OrderViewModel>();

        public ICommand LoadCommand { get; }

        public ICommand GenerateCommand { get; }

        public ICommand ResetCommand { get; }

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

        private async Task DoLoadDataAsync()
        {
            using (LockUi())
            {
                var items = await itemRepositoryService.GetAllItemsAsync();
                var customers = await customerRepositoryService.GetAllCustomersAsync();
                var orders = await orderRepositoryService.GetAllOrdersAsync();

                SetData(items, customers, orders);
            }
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

        private async Task DoGenerateDataAsync()
        {
            using (LockUi())
            {
                GetData(out var items, out var customers, out var orders);

                var itemIds = await itemRepositoryService.CreateItemsAsync(items);
                var customerIds = await customerRepositoryService.CreateCustomersAsync(customers);

                foreach (var order in orders)
                {
                    var customerIndex = customers.Select((x, i) => (x, i)).Single(y => y.x.Id == order.CustomerId).i;
                    var newCustomerId = customerIds[customerIndex];
                    order.CustomerId = newCustomerId;

                    var itemIndex = items.Select((x, i) => (x, i)).Single(y => y.x.Id == order.ItemId).i;
                    var newItemId = itemIds[itemIndex];
                    order.ItemId = newItemId;
                }

                await orderRepositoryService.CreateOrdersAsync(orders);
            }
        }

        private void GetData(
            out List<ItemRepositoryData> items,
            out List<CustomerRepositoryData> customers,
            out List<OrderRepositoryData> orders)
        {
            var itemViewModels = CreateDesignTimeItems();
            var customerViewModels = CreateDesignTimeCustomers();
            var orderViewModels = CreateDesignTimeOrders(itemViewModels, customerViewModels);

            items = new List<ItemRepositoryData>();
            foreach (var itemViewModel in itemViewModels)
            {
                items.Add(new ItemRepositoryData(itemViewModel.Id, itemViewModel.Name));
            }

            customers = new List<CustomerRepositoryData>();
            foreach (var customerViewModel in customerViewModels)
            {
                customers.Add(new CustomerRepositoryData(customerViewModel.Id, customerViewModel.Name));
            }

            orders = new List<OrderRepositoryData>();
            foreach (var orderViewModel in orderViewModels)
            {
                orders.Add(new OrderRepositoryData(orderViewModel.Id, orderViewModel.CustomerViewModel.Id, orderViewModel.ItemViewModel.Id, orderViewModel.Amount));
            }
        }

        private async Task DoResetDataAsync()
        {
            using (LockUi())
            {
                await orderRepositoryService.DeleteAllAsync();
                await customerRepositoryService.DeleteAllAsync();
                await itemRepositoryService.DeleteAllAsync();
            }
        }

        private IDisposable LockUi()
        {
            var uiLocker = new UiLocker(this);
            return uiLocker;
        }

        private class UiLocker : IDisposable
        {
            private readonly MainViewModel vm;
            private bool isDisposed = false;

            public UiLocker(MainViewModel vm)
            {
                this.vm = vm;
                vm.IsEnabled = false;
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!isDisposed)
                {
                    if (disposing)
                    {
                        vm.IsEnabled = true;
                    }

                    isDisposed = true;
                }
            }

            // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
            // ~UiLocker()
            // {
            //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
