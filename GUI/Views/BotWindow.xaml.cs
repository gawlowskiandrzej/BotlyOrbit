using BotlyOrbit.GUI.ViewModels;
using System.Windows;

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
    }
}
