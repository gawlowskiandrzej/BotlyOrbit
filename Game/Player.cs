using BotlyOrbit.Game.Entities;

namespace BotlyOrbit.Game
{
    internal class Player : Ship
    {
        public double Cargo { get; set; } = 200;
        public double MaxCargo{ get; set; } = 400;
        public double Hp { get; set; } = 100;
        public double MaxHp { get; set; } = 200;
        public double Shield { get; set; } = 200;
        public double MaxShield { get; set; } = 900;
    }
}
