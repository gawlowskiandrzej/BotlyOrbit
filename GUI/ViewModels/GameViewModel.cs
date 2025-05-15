using System.ComponentModel;

namespace BotlyOrbit.GUI.ViewModels
{
    internal class GameViewModel : INotifyPropertyChanged
    {
        public string GameUrl { get; set; }
        public GameViewModel(string url)
        {
            GameUrl = url;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
