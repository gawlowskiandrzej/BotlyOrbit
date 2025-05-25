using BotlyOrbit.Game.Objects;
using BotlyOrbit.Game.Other;
using System;
using System.Collections.Generic;

namespace BotlyOrbit.Game.Managers
{
    internal class ScreenManager : Updatable
    {
        public HeroManager HeroManager { get; set; } = new HeroManager();
        public MapManager MapManager { get; set; } = new MapManager();
        public ViewManager ViewManager { get; set; } = new ViewManager();
        public MiniMapManager MiniMapManager { get; set; } = new MiniMapManager();
        public EventManager EventManager { get; set; } = new EventManager();
        public override void update(IntPtr address)
        {
            base.update(address);

            int id = MemoryManager.ReadInt(MemoryManager.ReadPointer(Address + 240) + 56);

            if (id == 0) return;

            if (HeroManager.Address != IntPtr.Zero)
            {
                List<IntPtr> addresses = MemoryManager.QueryMemoryInt(id);

                foreach (IntPtr item in addresses)
                {
                    HeroManager.update(item);
                    if (HeroManager.IsValid())
                        break;
                }
            }

            MapManager.update(Address + 256);
            ViewManager.update(Address + 216);
            MiniMapManager.update(Address + 224);
            EventManager.update(Address + 200);
        }
    }
}
