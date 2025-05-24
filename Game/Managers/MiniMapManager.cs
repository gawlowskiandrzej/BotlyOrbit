using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Managers
{
    internal class MiniMapManager : Updatable
    {
        public int MinimapX { get; set; }
        public int MinimapY { get; set; }
        public override void update(IntPtr address)
        {
            base.update(address);

            MinimapX = MemoryManager.ReadInt(Address + 168);
            MinimapY = MemoryManager.ReadInt(Address + 172);

            IntPtr layeardPtr = MemoryManager.ReadPointer(Address + 248);
            IntPtr Vector = MemoryManager.ReadPointer(Address + 168);

        }
    }
}
