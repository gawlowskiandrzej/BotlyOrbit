using BotlyOrbit.Game.Objects;
using BotlyOrbit.Game.Other;
using System;
using System.Collections.Generic;

namespace BotlyOrbit.Game.GameTools
{
    internal class SWFList : Updatable
    {
        public int Count { get; set; }
        public List<Trait> Objects { get; set; }

        public override void update(IntPtr address)
        {
            base.update(address);

            Count = MemoryManager.ReadInt(address + 8);
            Objects = new List<Trait>(Count);
            if (Count <= 0) return;
            Address += 16;
            for (int i = 0; i < Count; i++)
            {
                if ((Address + (i * 8)) != IntPtr.Zero)
                {
                    Trait trait = new Trait();
                    trait.update(Address + (i * 8));
                    Objects.Add(trait);
                }
            }

        }
    }
}
