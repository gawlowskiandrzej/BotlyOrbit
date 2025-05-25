using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Objects
{
    internal class Location2D : Updatable
    {
        public double XPos { get; set; }
        public double YPos { get; set; }

        public Location2D()
        {
            
        }
        public Location2D(double x, double y)
        {
            XPos = x; YPos = y;
        }
        public override void update(IntPtr address)
        {
            base.update(address);
            XPos = MemoryManager.ReadDouble(Address + 32);
            YPos = MemoryManager.ReadDouble(Address + 40);
        }
        public Location2D toAngle(Location2D center, double angle, double distance)
        {
            this.XPos = center.XPos - (Math.Cos(angle) * distance);
            this.YPos = center.YPos - (Math.Sin(angle) * distance);
            return this;
        }
        public double Angle(Location2D o)
        {
            return Math.Atan2(YPos - o.YPos, XPos - o.XPos);
        }
    }
}
