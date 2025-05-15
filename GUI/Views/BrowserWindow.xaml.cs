using BotlyOrbit.GUI.Services;
using BotlyOrbit.GUI.ViewModels;
using CefSharp.Wpf;
using System;
using System.Diagnostics;
using System.Windows;

namespace BotlyOrbit.GUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {
        public BrowserWindow()
        {
            InitializeComponent();
            DataContext = new BrowserViewModel();
            browser.LifeSpanHandler = new LifeSpanHandler();
        }

    }
}
