﻿using NetCoreTest.DL;
using NetCoreTest.DL.Customers;
using NetCoreTest.DL.Items;
using NetCoreTest.DL.Orders;
using NetCoreTest.DL.Transactions;
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
        private readonly ITransactionService transactionService;
        private readonly IItemRepositoryService itemRepositoryService;
        private readonly ICustomerRepositoryService customerRepositoryService;
        private readonly IOrderRepositoryService orderRepositoryService;
        private bool isEnabled = true;
        private bool shouldInjectError = false;

        [Obsolete("Design-time constructor")]
        public MainViewModel()
        {
            AddDesignTimeData();
        }

        public MainViewModel(
            ITransactionService transactionService,
            IItemRepositoryService itemRepositoryService,
            ICustomerRepositoryService customerRepositoryService,
            IOrderRepositoryService orderRepositoryService)
        {
            this.transactionService = transactionService;
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

        public bool ShouldInjectError
        {
            get => shouldInjectError;
            set => SetProperty(ref shouldInjectError, value);
        }

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
            using (WithUiLock())
            using (var transaction = await WithTransactionAsync())
            {
                var items = await itemRepositoryService.GetAllItemsAsync(transaction.Id);
                var customers = await customerRepositoryService.GetAllCustomersAsync(transaction.Id);
                var orders = await orderRepositoryService.GetAllOrdersAsync(transaction.Id);

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
            using (WithUiLock())
            using (var transaction = await WithTransactionAsync())
            {
                GetData(out var items, out var customers, out var orders);

                var itemIds = await itemRepositoryService.CreateItemsAsync(transaction.Id, items);
                var customerIds = await customerRepositoryService.CreateCustomersAsync(transaction.Id, customers);

                foreach (var order in orders)
                {
                    var customerIndex = customers.Select((x, i) => (x, i)).Single(y => y.x.Id == order.CustomerId).i;
                    var newCustomerId = customerIds[customerIndex];
                    order.CustomerId = newCustomerId;

                    var itemIndex = items.Select((x, i) => (x, i)).Single(y => y.x.Id == order.ItemId).i;
                    var newItemId = itemIds[itemIndex];
                    order.ItemId = shouldInjectError ? int.MaxValue : newItemId; // !!!!!! CAN INJECT ERROR HERE !!!!!!
                }

                await orderRepositoryService.CreateOrdersAsync(transaction.Id, orders);

                transaction.CommitAsync();
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
            using (WithUiLock())
            using (var transaction = await WithTransactionAsync())
            {
                await orderRepositoryService.DeleteAllAsync(transaction.Id);
                await customerRepositoryService.DeleteAllAsync(transaction.Id);
                await itemRepositoryService.DeleteAllAsync(transaction.Id);

                transaction.CommitAsync();
            }
        }

        private UiLock WithUiLock()
        {
            var uiLock = new UiLock(this);
            return uiLock;
        }

        private async Task<Transaction> WithTransactionAsync()
        {
            var transactionId = await transactionService.StartAsync();
            var transaction = new Transaction(transactionService, transactionId);
            return transaction;
        }

        private class UiLock : IDisposable
        {
            private readonly MainViewModel vm;
            private readonly bool prevValue;
            private bool isDisposed = false;

            public UiLock(MainViewModel vm)
            {
                this.vm = vm;
                prevValue = vm.IsEnabled;
                vm.IsEnabled = false;
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!isDisposed)
                {
                    if (disposing)
                    {
                        vm.IsEnabled = prevValue;
                    }

                    isDisposed = true;
                }
            }
        }

        private class Transaction : IDisposable
        {
            private readonly ITransactionService transactionService;
            private readonly Guid transactionId;
            private bool isDisposed = false;

            public Transaction(ITransactionService transactionService, Guid transactionId)
            {
                this.transactionService = transactionService;
                this.transactionId = transactionId;
            }

            public Guid Id
            {
                get
                {
                    if (isDisposed)
                    {
                        throw new InvalidOperationException("Transaction already disposed");
                    }

                    return transactionId;
                }
            }

            public void CommitAsync()
            {
                if (isDisposed)
                {
                    throw new InvalidOperationException("Transaction already disposed");
                }

                transactionService.CommitAsync(transactionId);
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!isDisposed)
                {
                    if (disposing)
                    {
                        transactionService.StopAsync(transactionId);
                    }

                    isDisposed = true;
                }
            }
        }
    }
}
