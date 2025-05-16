using BotlyOrbit.GUI.Helpers;
using BotlyOrbit.GUI.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BotlyOrbit.GUI.ViewModels
{
    public class BrowserViewModel : INotifyPropertyChanged
    {
        BrowserNavigator Navigator;

        public event PropertyChangedEventHandler PropertyChanged;

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        public BrowserViewModel()
        {
            var browserModel = new BrowserModel();
            
            Url = browserModel.InitialUrl;
            
        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
