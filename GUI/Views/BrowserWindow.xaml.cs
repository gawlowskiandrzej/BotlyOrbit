using BotlyOrbit.GUI.Services;
using BotlyOrbit.GUI.ViewModels;
using System.Windows;

namespace BotlyOrbit.GUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {
        public BrowserWindow()
        {
            InitializeComponent();
            var browserModel =  new BrowserViewModel();
            DataContext = browserModel;
            browser.Load(browserModel.Url);
            browser.LifeSpanHandler = new LifeSpanHandler();
        }

    }
}
