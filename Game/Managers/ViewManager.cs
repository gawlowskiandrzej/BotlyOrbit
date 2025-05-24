using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Managers
{
    internal class ViewManager : Updatable
    {
        public int ClientWidth { get; set; }
        public int ClientHeight { get; set; }

        public double BoundX { get; set; }
        public double BoundY { get; set; }

        private double BoundMaxX { get; set; }
        public double BoundMaxY { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public override void update(IntPtr address)
        {
            base.update(address);

            IntPtr boundAddr = MemoryManager.ReadPointer(Address + 208);

            ClientWidth = MemoryManager.ReadInt(boundAddr + 168);
            ClientHeight = MemoryManager.ReadInt(boundAddr + 172);

            IntPtr updated = MemoryManager.ReadPointer(boundAddr + 280);
            updated = MemoryManager.ReadPointer(updated + 112);

            BoundX = MemoryManager.ReadDouble(updated + 80);
            BoundY = MemoryManager.ReadDouble(updated + 88);
            BoundMaxX = MemoryManager.ReadDouble(updated + 112);
            BoundMaxY = MemoryManager.ReadDouble(updated + 120);
            Width = BoundMaxX - BoundX;
            Height = BoundMaxY - BoundY;
        }
    }
}
