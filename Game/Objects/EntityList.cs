using BotlyOrbit.Game.Entities;
using BotlyOrbit.Game.Other;
using System;
using System.Collections.Generic;

namespace BotlyOrbit.Game.Objects
{
    internal class EntityList : Updatable
    {

        public List<Box> Boxes { get; set; }
        public List<Ship> Ships { get; set; }
        public List<Entity> Entities { get; set; }
        public IntPtr mapManaAddr = IntPtr.Zero;
        public int Size { get; set; }
        public override void update(IntPtr address)
        {
            Size = MemoryManager.ReadInt(address + 8);
            base.update(address);

            Entities = new List<Entity>(Size);
            Boxes = new List<Box>();
            Ships = new List<Ship>();
            Address += 16;

            for (int i = 0; i < Size; i++)
            {

                Entity ent = new Entity();
                IntPtr entAddr = Address + (i * 8);
                ent.update(entAddr);
                if (!ent.IsInValid(mapManaAddr))
                {
                    Entity createdEnt = EntityFactory.Create(entAddr);
                    Entities.Add(createdEnt);
                    switch (createdEnt.EntityType)
                    {
                        case EntityType.Box: Boxes.Add((Box)createdEnt);
                            break;
                        case EntityType.Loot: Boxes.Add((Box)createdEnt);
                            break;
                        case EntityType.Ship: Ships.Add((Ship)createdEnt);
                            break;
                    }

                }
            }

        }
    }
}
