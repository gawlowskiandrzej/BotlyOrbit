using BotlyOrbit.Game.GameTools;
using BotlyOrbit.Game.Interfaces;
using BotlyOrbit.Game.Objects;
using BotlyOrbit.Game.Other;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net;

namespace BotlyOrbit.Game.Entities
{
    internal class Entity : Updatable, IClicable
    {
        public int Id { get; set; }
        public EntityType EntityType { get; set; }
        public string AssetName { get; set; }
        public Location2D Location { get; set; } = new Location2D();
        public SWFList TraitsList { get; set; } = new SWFList();
        public Trait Clickable { get; set; } = new Trait();
        public IntPtr Container { get; set; } = new IntPtr();


        public override void update(IntPtr address)
        {
            base.update(address);

            // TODO adding prop to ent
            // Add id and rest
            Id = MemoryManager.ReadInt(Address + 55);
            Container = MemoryManager.ReadPointer(Address + 95);
            Location.update(Address + 63);
            TraitsList.update(MemoryManager.ReadPointer(Address + 47) + 48);

            GetValidTrait();
        }

        public new bool IsInValid(IntPtr mapAddr)
        {
            base.IsValid();
            Id = MemoryManager.ReadInt(Address + 55);
            Container = MemoryManager.ReadPointer(Address + 95);
            return Container != mapAddr;

        }
        private void GetValidTrait()
        {
            foreach (var trait in TraitsList.Objects)
            {
                if (trait.IsValid())
                {
                    Clickable = trait;
                    return;
                }
            }
        }
        public string GetAssetString()
        {
            Trait firstTrait = TraitsList.Objects.FirstOrDefault();
            if (firstTrait == null) return "ERROR";
            IntPtr asd = MemoryManager.ReadPointer(MemoryManager.ReadPointer(MemoryManager.ReadPointer(firstTrait.Address + 0x3F)+0x20)+0x18);
            IntPtr next = MemoryManager.ReadPointer(MemoryManager.ReadPointer(asd + 0x8) + 0x10);
            IntPtr next2 = MemoryManager.ReadPointer(next+0x18);
            return MemoryManager.ReadString(next2).Trim();
        }

        public void click()
        {
            
        }
    }
}
