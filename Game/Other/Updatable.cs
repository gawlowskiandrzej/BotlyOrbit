using BotlyOrbit.Game.Interfaces;
using System;
using System.Net;

namespace BotlyOrbit.Game.Other
{
    abstract class Updatable : IUpdatable
    {
        public IntPtr Address { get; set; }
        public virtual void update(IntPtr address)
        {
            Address = MemoryManager.ReadPointer(address);
        }
        public int readIntFromDerefrenced(int offset) => MemoryManager.ReadInt(MemoryManager.ReadPointer(Address + offset) + 40);
    }
}
