using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Objects
{
    internal class PlayerInfo : Updatable
    {
        public string Username { get; set; }
        public string ClanTag { get; set; }
        public int Rank { get; set; }
        public int ClanId { get; set; }
        public int FactionId { get; set; }
        public override void update(IntPtr address)
        {
            base.update(address);

            Username = MemoryManager.ReadString(MemoryManager.ReadPointer(address + 40) +16 , 20);
            FactionId = MemoryManager.ReadInt(MemoryManager.ReadPointer(address+72) + 40);

        }
    }
}
