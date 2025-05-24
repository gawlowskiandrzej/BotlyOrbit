using BotlyOrbit.Game.Objects;
using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Managers
{
    internal class MapManager : Updatable
    {
        public EntityList EntityList { get; set; } = new EntityList();
        public int internalWidth { get; set; } = 21000;
        public int internalHeight { get; set; } = 13100;

        public int currMapId { get; set; }
        public override void update(IntPtr address)
        {
            base.update(address);

            IntPtr add = MemoryManager.ReadPointer(Address + 40);
            EntityList.mapManaAddr = Address;
            EntityList.update(add+48);

            internalWidth = MemoryManager.ReadInt(Address + 76);
            internalHeight = MemoryManager.ReadInt(Address + 80);

            currMapId = MemoryManager.ReadInt(Address + 84);

        }
    }
}
