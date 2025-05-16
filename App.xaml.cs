using CefSharp.WinForms;
using CefSharp;
using System.IO;
using System.Windows;
using System;

namespace BotlyOrbit
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var settings = new CefSettings();

            string pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "pepflashplayer.dll");

            if (File.Exists(pluginPath))
            {
                settings.CefCommandLineArgs.Add("ppapi-flash-path", pluginPath);
                settings.CefCommandLineArgs.Add("ppapi-flash-version", "32.0.0.465"); // lub twoja wersja
                settings.CefCommandLineArgs.Add("allow-outdated-plugins", "1");
                settings.PersistSessionCookies = true;
            }

            settings.CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyCefCache");

            settings.UserAgent = "BigPointClient/1.6.9";

            Cef.Initialize(settings);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
