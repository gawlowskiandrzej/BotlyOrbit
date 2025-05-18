using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Managers
{
    internal class HeroManager : Updatable
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int Speed { get; set; }
        public int Bool { get; set; }
        public int Val { get; set; }
        public int Cargo { get; set; }
        public int MaxCargo { get; set; }
        public override void update(IntPtr address)
        {
            base.update(address);
            
            Id = MemoryManager.ReadInt(address);
            Level = MemoryManager.ReadInt(address+4);
            Speed = MemoryManager.ReadInt(address+8);
            Bool = MemoryManager.ReadInt(address+12);
            Val = MemoryManager.ReadInt(address+16);
            Cargo = MemoryManager.ReadInt(MemoryManager.ReadPointer(address+280)+40);
            MaxCargo = MemoryManager.ReadInt(MemoryManager.ReadPointer(address+288)+40);
        }
        public new bool IsValid() =>
             (Level >= 0 && Level <= 32
                    && Speed > 50 && Speed < 2000
                    && (Bool == 1 || Bool == 2)
                    && Val == 0
                    && Cargo >= 0
                    && MaxCargo >= 100 && MaxCargo < 100_000);
    }
}
