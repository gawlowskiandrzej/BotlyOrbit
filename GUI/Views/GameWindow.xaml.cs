using BotlyOrbit.GUI.Services;
using BotlyOrbit.GUI.ViewModels;
using CefSharp;
using System.Windows;

namespace BotlyOrbit.GUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow(string url)
        {
            InitializeComponent();
            DataContext = new GameViewModel(url);
            brow.FrameLoadEnd += Brow_FrameLoadEnd;
        }

        private void Brow_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                var processFinder = new ProcessFinder();
                processFinder.FindPid();
            }
        }
    }
}
