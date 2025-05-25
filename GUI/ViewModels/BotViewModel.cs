using BotlyOrbit.Game.Entities;
using BotlyOrbit.Game.GameTools;
using BotlyOrbit.Game.Managers;
using BotlyOrbit.Game.Objects;
using BotlyOrbit.GUI.Helpers;
using BotlyOrbit.GUI.Views;
using System;
using System.Linq;
using System.Net;
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
        public event EventHandler<Location2D> ClickCenterEvent;


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
                    InitManager.Update();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        EntitiesViewModel.EntityList = InitManager.ScreenManager.MapManager.EntityList;
                    });
                    Thread.Sleep(500);
                }
            });

            BotWindow window = new BotWindow(this);
            window.Show();
        }

        private void EntitiesViewModel_EntityDoubleClicked(object sender, Game.Entities.Entity e)
        {

            MouseManager mouse = new MouseManager(InitManager.ScreenManager.ViewManager);
            Location2D loc = mouse.GetScreenLocation(e.Location);
            ClickCenterEvent?.Invoke(this, loc);

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
