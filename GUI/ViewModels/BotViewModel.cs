using BotlyOrbit.Game.Managers;
using BotlyOrbit.GUI.Helpers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BotlyOrbit.GUI.ViewModels
{
    internal class BotViewModel
    {
        public ICommand StartCollectingCM { get; set; }
        public ICommand StartFightingCM { get; set; }
        public ICommand DoubleClickEntity { get; set; }
        public EntitiesViewModel EntitiesViewModel { get; set; }
        public InitManager InitManager { get; set; }

        public BotViewModel()
        {
        }
        public BotViewModel(ref InitManager initManager)
        {
            StartCollectingCM = new RelayCommand(StartCollecting);
            StartFightingCM = new RelayCommand(StartFighting);

            EntitiesViewModel = new EntitiesViewModel(new Game.Objects.EntityList());
            EntitiesViewModel.EntityDoubleClicked += EntitiesViewModel_EntityDoubleClicked;

            InitManager = initManager;

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    InitManager.InitValues();
                    EntitiesViewModel.EntityList = InitManager.ScreenManager.MapManager.EntityList;
                    Thread.Sleep(500);
                }
            });
        }

        private void EntitiesViewModel_EntityDoubleClicked(object sender, Game.Entities.Entity e)
        {
            MessageBox.Show(e.AssetName);
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
