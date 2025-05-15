using BotlyOrbit.GUI.ViewModels;
using BotlyOrbit.GUI.Views;
using System.Windows;

namespace BotlyOrbit
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
