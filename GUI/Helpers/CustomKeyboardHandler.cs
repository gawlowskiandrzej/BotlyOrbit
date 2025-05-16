using CefSharp;
using System;

namespace BotlyOrbit.GUI.Helpers
{
    internal class CustomKeyboardHandler : IKeyboardHandler
    {
        private readonly Action onKPressed;

        public CustomKeyboardHandler(Action onKPressed)
        {
            this.onKPressed = onKPressed;
        }
        public bool OnPreKeyEvent(
        IWebBrowser chromiumWebBrowser,
        IBrowser browser,
        KeyType type,
        int windowsKeyCode,
        int nativeKeyCode,
        CefEventFlags modifiers,
        bool isSystemKey,
        ref bool isKeyboardShortcut)
        {
            // Tylko na wciśnięcie (nie na puszczenie klawisza)
            if (type == KeyType.RawKeyDown && windowsKeyCode == 75)
            {
                onKPressed?.Invoke();
            }

            return false; // false = nie przechwytuj, przekaż dalej do Chromium
        }

        public bool OnKeyEvent(
            IWebBrowser chromiumWebBrowser,
            IBrowser browser,
            KeyType type,
            int windowsKeyCode,
            int nativeKeyCode,
            CefEventFlags modifiers,
            bool isSystemKey)
        {
            return false;
        }
    }
}
