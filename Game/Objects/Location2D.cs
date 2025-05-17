using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Objects
{
    internal class Location2D : Updatable
    {
        public double XPos { get; set; }
        public double YPos { get; set; }

        public override void update(IntPtr address)
        {
            base.update(address);
            XPos = MemoryManager.ReadDouble(address + 32);
            YPos = MemoryManager.ReadDouble(address + 40);
        }
    }
}
