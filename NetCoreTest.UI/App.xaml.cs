using NetCoreTest.DL.Customers;
using NetCoreTest.DL.Items;
using NetCoreTest.DL.Orders;
using NetCoreTest.DL.Transactions;
using System.Windows;

namespace NetCoreTest.UI
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var transactionService = new TransactionService();
            var itemRepositoryService = new ItemRepositoryService(transactionService);
            var customerRepositoryService = new CustomerRepositoryService(transactionService);
            var orderRepositoryService = new OrderRepositoryService(transactionService);

            var mainViewModel = new MainViewModel(
                transactionService,
                itemRepositoryService,
                customerRepositoryService,
                orderRepositoryService);
            var mainWindow = new MainWindow(mainViewModel);
            mainWindow.Show();
        }
    }
}
