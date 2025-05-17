using BotlyOrbit.GUI.Services;
using BotlyOrbit.GUI.ViewModels;
using CefSharp.WinForms;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BotlyOrbit.GUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public ChromiumWebBrowser Browser{ get; set; }
        public GameWindow(string url)
        {
            InitializeComponent();
            Browser = new ChromiumWebBrowser();
            Browser.Load(url);
            myBrowserContainer.Child = Browser; // jeśli używasz jakiegoś kontenera
            Browser.FrameLoadEnd += Brow_FrameLoadEnd;
        }

        private async void Brow_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                var processFinder = new ProcessFinder();
                var procId = processFinder.FindPid();

                await Task.Delay(19000);
                
                DataContext = new GameViewModel(Browser, procId);
            }
        }
    }
}
