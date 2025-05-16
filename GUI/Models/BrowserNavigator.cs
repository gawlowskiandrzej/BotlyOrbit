using CefSharp;

namespace BotlyOrbit.GUI.Models
{
    internal class BrowserNavigator
    {
        private readonly IBrowserHost _host;

        public double XPos { get; set; }
        public double YPos { get; set; }
        public BrowserNavigator(double gameWidth, double gameHeight, IBrowserHost host, double xOffset = 0, double yOffset = 0)
        {
            XPos = gameWidth/2 + xOffset;
            YPos = gameHeight/2 + yOffset;
            _host = host;
        }
        public BrowserNavigator(double xPos, double yPos)
        {
            XPos = xPos;
            YPos = yPos;
        }

        public void Click(double x = 0, double y = 0)
        {
            if (x == 0)
                x = XPos;
            if (y == 0)
                y = YPos;

            // Kliknięcie w środek
            _host.SendMouseClickEvent((int)x,(int)y, MouseButtonType.Left,false, 1, CefEventFlags.None);
            _host.SendMouseClickEvent((int)x, (int)y, MouseButtonType.Left, true, 1, CefEventFlags.None);

        }
    }
}
