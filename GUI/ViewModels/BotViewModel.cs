using BotlyOrbit.GUI.Helpers;
using System.Windows.Input;

namespace BotlyOrbit.GUI.ViewModels
{
    internal class BotViewModel
    {
        public ICommand StartCollectingCM { get; set; }
        public ICommand StartFightingCM { get; set; }
        public EntitiesViewModel EntitiesViewModel { get; set; }


        public BotViewModel()
        {
            StartCollectingCM = new RelayCommand(StartCollecting);
            StartFightingCM = new RelayCommand(StartFighting);
            EntitiesViewModel = new EntitiesViewModel(new Game.Objects.EntityList());
        }


        public void StartCollecting()
        {
            // Initialize collecting module
        }

        public void StartFighting()
        {
            // Initialize figting module
        }
    }
}
