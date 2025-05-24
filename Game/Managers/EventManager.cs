using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Managers
{
    internal class EventManager : Updatable
    {
        public int Xdest { get; set; }
        public int Ydest { get; set; }
        public override void update(IntPtr address)
        {
            base.update(address);

            Xdest = MemoryManager.ReadInt(Address + 44);
            Ydest = MemoryManager.ReadInt(Address + 48);
        }
    }
}
