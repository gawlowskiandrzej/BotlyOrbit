using BotlyOrbit.Game.Interfaces;
using System;
using System.Net;

namespace BotlyOrbit.Game.Other
{
    public abstract class Updatable : IUpdatable
    {
        public IntPtr Address { get; set; } = IntPtr.Zero;
        public virtual void update(IntPtr address)
        {
            Address = MemoryManager.ReadPointer(address);
            if (!IsValid())
                Address = IntPtr.Zero;
        }
        public virtual bool IsValid() => Address != IntPtr.Zero && (Address.ToInt64() > 0 && Address.ToInt64() < 0x7fffffffffff);
        public int readIntFromDerefrenced(int offset) => MemoryManager.ReadInt(MemoryManager.ReadPointer(Address + offset) + 40);
    }
}
