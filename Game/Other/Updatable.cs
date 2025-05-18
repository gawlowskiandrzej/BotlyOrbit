using BotlyOrbit.Game.Interfaces;
using System;
using System.Net;

namespace BotlyOrbit.Game.Other
{
    abstract class Updatable : IUpdatable
    {
        public IntPtr Address { get; set; } = IntPtr.Zero;
        public virtual void update(IntPtr address)
        {
            if (IsValid())
                Address = MemoryManager.ReadPointer(address);
        }
        public virtual bool IsValid() => Address != IntPtr.Zero && (Address.ToInt64() > 0 && Address.ToInt64() < 0x7FFFFFFFF);
        public int readIntFromDerefrenced(int offset) => MemoryManager.ReadInt(MemoryManager.ReadPointer(Address + offset) + 40);
    }
}
