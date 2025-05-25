using BotlyOrbit.Game.GameTools;
using BotlyOrbit.Game.Objects;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BotlyOrbit.Game.Managers
{
    internal class MouseManager
    {
        public ViewManager ViewManager { get; set; }
        public MouseManager(ViewManager viewManager)
        {
            ViewManager = viewManager;
        }
        public Location2D GetScreenLocation(Location2D loc)
        {
            Random rand = new Random();   
            Location2D center = new Location2D(ViewManager.BoundX + (ViewManager.Width / 2), ViewManager.BoundY + (ViewManager.Height / 2));
            double angle = center.Angle(loc) + rand.NextDouble() * 0.2 - 0.1;
            double distance = 125 + rand.NextDouble() * 75;
            center.toAngle(center, angle, distance); // ile trzeba sie kratek x oraz y poruszyć do wroga
            Location2D point2 = new Location2D((int)((center.XPos - ViewManager.BoundX) / (ViewManager.Width) * (double)ViewManager.ClientWidth),
               (int)((center.YPos - ViewManager.BoundY) / (ViewManager.Height) * (double)ViewManager.ClientHeight));
            return point2;
        }
    }
}
