using BotlyOrbit.Game;

namespace BotlyOrbit.GUI.Models
{
    internal class StatsModel
    {
        public Player MyPlayer { get; set; }
        public StatsModel()
        {
            MyPlayer = new Player();
        }
    }
}
