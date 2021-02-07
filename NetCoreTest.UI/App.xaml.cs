using NetCoreTest.DL.Customers;
using NetCoreTest.DL.Items;
using NetCoreTest.DL.Orders;
using System.Windows;

namespace NetCoreTest.UI
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var itemRepositoryService = new ItemRepositoryService();
            var customerRepositoryService = new CustomerRepositoryService();
            var orderRepositoryService = new OrderRepositoryService();

            var mainViewModel = new MainViewModel(itemRepositoryService, customerRepositoryService, orderRepositoryService);
            var mainWindow = new MainWindow(mainViewModel);
            mainWindow.Show();
        }
    }
}
