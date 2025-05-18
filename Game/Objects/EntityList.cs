using BotlyOrbit.Game.Entities;
using BotlyOrbit.Game.Other;
using System;
using System.Collections.Generic;

namespace BotlyOrbit.Game.Objects
{
    internal class EntityList : Updatable
    {
        public List<Entity> Entities { get; set; }
        public int Size { get; set; }
        public override void update(IntPtr address)
        {
            Size = MemoryManager.ReadInt(address + 8);
            base.update(address);

            Entities = new List<Entity>(Size);

            for (int i = 0; i < Size; i++) 
            {

                Entity ent = new Entity();
                IntPtr entAddr = Address + (i * 8);
                ent.update(entAddr);
                Entities.Add(EntityFactory.Create(entAddr));
            }
        }
    }
}
