using BotlyOrbit.Game.GameTools;
using BotlyOrbit.Game.Managers;
using BotlyOrbit.Game.Objects;
using BotlyOrbit.GUI.Helpers;
using BotlyOrbit.GUI.Models;
using BotlyOrbit.GUI.Views;
using CefSharp.WinForms;
using System.ComponentModel;

namespace BotlyOrbit.GUI.ViewModels
{
    internal class GameViewModel : INotifyPropertyChanged
    {
        BrowserNavigator Navigator { get; set; }

        public GameViewModel(ChromiumWebBrowser browser, int procId)
        {
            Navigator = new BrowserNavigator(browser.Width, browser.Height, browser.GetBrowser().GetHost(), 0, 0);
            browser.KeyboardHandler = new CustomKeyboardHandler(() =>
            {
                Navigator.Click(); // lub dowolna inna metoda
            });

            var init = new InitManager(procId);
            BotViewModel botViewModel = new BotViewModel(ref init);
            botViewModel.ClickCenterEvent += BotViewModel_ClickCenterEvent;
        }

        private void BotViewModel_ClickCenterEvent(object sender, Location2D loc)
        {
            Navigator.Click(loc.XPos, loc.YPos);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
