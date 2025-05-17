using BotlyOrbit.Game.Other;
using System;
using System.Collections.Generic;

namespace BotlyOrbit.Game.GameTools
{
    internal class SWFList : Updatable
    {
        public int Count { get; set; }
        public List<IntPtr> Objects { get; set; }

        public override void update(IntPtr address)
        {
            base.update(address);

            Count = MemoryManager.ReadInt(address + 8);
            IntPtr listPtr = MemoryManager.ReadPointer(address);

            Objects = new List<IntPtr>(Count);
            for (int i = 0; i < Count; i+=8)
            {
                if ((listPtr + i) != IntPtr.Zero)
                    Objects.Add(listPtr+i);
            }

        }
    }
}
