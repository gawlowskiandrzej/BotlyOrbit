using BotlyOrbit.GUI.ViewModels;
using System.Windows.Controls;

namespace BotlyOrbit.GUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
            DataContext = new MapViewModel();
        }
    }
}
