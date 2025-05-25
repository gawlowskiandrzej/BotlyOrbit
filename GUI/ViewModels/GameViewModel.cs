using BotlyOrbit.Game.Managers;
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
            Navigator = new BrowserNavigator(browser.Width, browser.Height, browser.GetBrowser().GetHost(), 0, -100);
            browser.KeyboardHandler = new CustomKeyboardHandler(() =>
            {
                Navigator.Click(); // lub dowolna inna metoda
            });

            var init = new InitManager(procId);
            BotWindow botWindow = new BotWindow(ref init);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
