using System.Windows;

namespace NetCoreTest.UI
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainViewModel = new MainViewModel();
            var mainWindow = new MainWindow(mainViewModel);
            mainWindow.Show();
        }
    }
}
