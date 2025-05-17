using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Objects
{
    internal class Durability : Updatable
    {
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Hull { get; set; }
        public int MaxHull { get; set; }
        public int Shield { get; set; }
        public int MaxShield { get; set; }

        public override void update(IntPtr address)
        {
            base.update(address);

            Hp = readIntFromDerefrenced(48);
            MaxHp = readIntFromDerefrenced(56);
            Hull = readIntFromDerefrenced(64);
            MaxHull = readIntFromDerefrenced(72);
            Shield = readIntFromDerefrenced(80);
            MaxShield = readIntFromDerefrenced(88);
        }
    }
}
