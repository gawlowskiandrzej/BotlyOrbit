using BotlyOrbit.Game.Objects;
using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Managers
{
    internal class MapManager : Updatable
    {
        public EntityList EntityList { get; set; } = new EntityList();
        public override void update(IntPtr address)
        {
            base.update(address);

            IntPtr add = MemoryManager.ReadPointer(Address + 40);
            EntityList.mapManaAddr = Address;
            EntityList.update(add+48);
            
        }
    }
}
