using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Managers
{
    internal class EventManager : Updatable
    {
        public double Xdest { get; set; }
        public double Ydest { get; set; }
        public override void update(IntPtr address)
        {
            base.update(address);

            IntPtr intPtr = MemoryManager.ReadPointer(Address+64);

            Xdest = MemoryManager.ReadDouble(intPtr + 44);
            Ydest = MemoryManager.ReadDouble(intPtr + 48);
        }
    }
}
