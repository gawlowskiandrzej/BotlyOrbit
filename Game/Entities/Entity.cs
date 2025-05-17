using BotlyOrbit.Game.GameTools;
using BotlyOrbit.Game.Interfaces;
using BotlyOrbit.Game.Objects;
using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Entities
{
    abstract class Entity : Updatable, IClicable
    {
        public int Id { get; set; }
        public int Radius { get; set; }
        public int Priority { get; set; }
        public int Enabled { get; set; }
        public Location2D Location { get; set; } = new Location2D();
        public SWFList TraitsList { get; set; } = new SWFList();
        public Trait Clickable { get; set; } = new Trait();

        public override void update(IntPtr address)
        {
            base.update(address);
            Location.update(Address + 63);
            TraitsList.update(MemoryManager.ReadPointer(Address + 47) + 48 + 16);

            GetValidTrait();
        }

        private void GetValidTrait()
        {
            foreach (var trait in TraitsList.Objects)
            {
                Clickable.update(trait);
                if (Clickable.IsValid())
                    return;
            }
        }

        public void click()
        {
            
        }
    }
}
