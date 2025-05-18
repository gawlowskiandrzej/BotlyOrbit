using BotlyOrbit.Game.Objects;
using BotlyOrbit.Game.Other;
using System;
using System.Collections.Generic;

namespace BotlyOrbit.Game.Managers
{
    internal class ScreenManager : Updatable
    {
        public HeroManager HeroManager { get; set; } = new HeroManager();
        public EntityList EntityList { get; set; } = new EntityList();
        public override void update(IntPtr address)
        {
            base.update(address);

            int id = MemoryManager.ReadInt(MemoryManager.ReadPointer(Address + 240) + 56);

            if (id == 0) return;

            List<IntPtr> addresses = MemoryManager.QueryMemoryInt(id);

            foreach (IntPtr item in addresses)
            {
                HeroManager.update(item);
                if (HeroManager.IsValid())
                    break;
            }
            EntityList.update(address);
        }
    }
}
