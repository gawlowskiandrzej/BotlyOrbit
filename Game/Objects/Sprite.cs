using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Objects
{
    internal class Sprite : Updatable
    {
        public int Id { get; set; }
        public override void update(IntPtr address)
        {
            base.update(address);

            Id = MemoryManager.ReadInt(Address + 167);
        }
    }
}
