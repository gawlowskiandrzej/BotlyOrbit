using BotlyOrbit.Game.Managers;
using BotlyOrbit.GUI.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace BotlyOrbit.GUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy BotWindow.xaml
    /// </summary>
    public partial class BotWindow : Window
    {
        public BotWindow()
        {
            InitializeComponent();
            DataContext = new BotViewModel();
        }
        internal BotWindow(ref InitManager initManager)
        {
            InitializeComponent();
            DataContext = new BotViewModel(ref initManager);
        }
    }
}
