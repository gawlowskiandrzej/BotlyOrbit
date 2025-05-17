using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Objects
{
    internal class ShipInfo : Updatable
    {
        public int Speed { get; set; }
        public double Angle { get; set; }
        public IntPtr Target { get; set; }
        public IntPtr KeepTargetTime { get; set; }
        public Location2D Destination { get; set; } = new Location2D();
        public override void update(IntPtr address)
        {
            base.update(address);

            Speed = MemoryManager.ReadInt(MemoryManager.ReadPointer(address + 80) + 40);
            Angle = MemoryManager.ReadDouble(MemoryManager.ReadPointer(address + 56) + 32);
            Target = MemoryManager.ReadPointer(address + 120);
            Destination.update(MemoryManager.ReadPointer(address + 104));
        }
    }
}
