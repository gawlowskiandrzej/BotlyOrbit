using BotlyOrbit.GUI.ViewModels;
using System.Windows.Controls;

namespace BotlyOrbit.GUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy StatsView.xaml
    /// </summary>
    public partial class StatsView : UserControl
    {
        public StatsView()
        {
            InitializeComponent();
            DataContext = new StatsViewModel();
        }
    }
}
