using BotlyOrbit.Game.GameTools;
using BotlyOrbit.Game.Interfaces;
using BotlyOrbit.Game.Objects;
using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Entities
{
    internal class Entity : Updatable, IClicable
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

            // TODO adding prop to ent
            // Add id and rest

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
        public string GetAssetString()
        {
            IntPtr firstTrait = TraitsList.Objects[0];
            IntPtr asd = MemoryManager.ReadPointer(MemoryManager.ReadPointer(MemoryManager.ReadPointer(firstTrait + 0x3F)+0x20)+0x18);
            IntPtr next = MemoryManager.ReadPointer(MemoryManager.ReadPointer(asd + 0x8) + 0x10);
            IntPtr next2 = MemoryManager.ReadPointer(next+0x18);
            return MemoryManager.ReadString(next2).Trim();
        }

        public void click()
        {
            
        }
    }
}
