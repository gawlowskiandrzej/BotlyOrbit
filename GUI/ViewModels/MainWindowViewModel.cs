using BotlyOrbit.GUI.Helpers;
using BotlyOrbit.GUI.Views;
using System.Windows.Input;

namespace BotlyOrbit.GUI.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand OpenBrowserCommand { get; }

        public MainWindowViewModel()
        {
            OpenBrowserCommand = new RelayCommand(OpenBrowserWindow);
            OpenBrowserWindow();
        }

        private void OpenBrowserWindow()
        {
            var browserWindow = new BrowserWindow
            {
                DataContext = new BrowserViewModel()
            };
            browserWindow.Show();
        }
    }
}
