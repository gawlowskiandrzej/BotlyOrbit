

using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Objects
{
    internal class Trait : Updatable
    {
        public int Radius { get; set; }
        public int Priority { get; set; }
        public int Enabled { get; set; }

        public override void update(IntPtr address)
        {
            base.update(address);

            Radius = MemoryManager.ReadInt(Address + 39);
            Priority = MemoryManager.ReadInt(Address + 43);
            Enabled = MemoryManager.ReadInt(Address + 47);
        }

        public bool IsValid() => (Radius >= 0 && Radius < 4000 && Priority > -4 && Priority < 1000 && (Enabled == 1 || Enabled == 0));

    }
}
