using BotlyOrbit.Game.Objects;
using System;

namespace BotlyOrbit.Game.Entities
{
    internal class Ship : Entity
    {
        public string Name { get; set; } = "Ship";
        public PlayerInfo PlayerInfo { get; set; } = new PlayerInfo();
        public ShipInfo ShipInfo { get; set; } = new ShipInfo();
        public Durability Durability { get; set; } = new Durability();

        public Ship(IntPtr address)
        {
            update(address);
        }
        public override void update(IntPtr address)
        {
            base.update(address);

            PlayerInfo.update(Address + 247);
            ShipInfo.update(Address + 231);
            Durability.update(Address + 183);

        }


    }
}
