using BotlyOrbit.Game.GameTools;
using BotlyOrbit.Game.Interfaces;
using BotlyOrbit.Game.Objects;
using BotlyOrbit.Game.Other;
using System;
using System.ComponentModel;
using System.Linq;

namespace BotlyOrbit.Game.Entities
{
    internal class Entity : Updatable, IMeasurable
    {
        public int Id { get; set; }
        public EntityType EntityType { get; set; }
        public string AssetName { get; set; }
        public double DistanceToPlayer { get; set; }
        public Location2D Location { get; set; } = new Location2D();
        public SWFList TraitsList { get; set; } = new SWFList();
        public Trait Clickable { get; set; } = new Trait();
        public int IsNpc { get; set; }
        public int Visible { get; set; }
        public IntPtr Container { get; set; } = new IntPtr();


        public override void update(IntPtr address)
        {
            base.update(address);

            Id = MemoryManager.ReadInt(Address + 55);
            IsNpc = MemoryManager.ReadInt(Address + 111);
            Visible = MemoryManager.ReadInt(Address + 115);
            Container = MemoryManager.ReadPointer(Address + 95);
            Location.update(Address + 63);
            TraitsList.update(MemoryManager.ReadPointer(Address + 47) + 48);

            GetValidTrait();
            AssetName = GetAssetString();
        }

        public new bool IsInValid(IntPtr mapAddr)
        {
            base.IsValid();
            Id = MemoryManager.ReadInt(Address + 55);
            Container = MemoryManager.ReadPointer(Address + 95);
            return Container != mapAddr;

        }
        public bool IsShip()
        {
            int id = MemoryManager.ReadInt(Address + 55);
            int c = MemoryManager.ReadInt(Address + 119);
            int d = MemoryManager.ReadInt(Address + 123);

            return id > 0 && (IsNpc == 1 || IsNpc == 0) &&
                    (Visible == 1 || Visible == 0) && (c == 1 || c == 0) && d == 0;
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

        public double GetDistance(Location2D baseLocation)
        {
            double x = baseLocation.XPos - Location.XPos;
            double y = baseLocation.YPos - Location.YPos;
            return Math.Sqrt((x * x) + (y * y));
        }
    }
}
