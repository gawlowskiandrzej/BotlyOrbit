using BotlyOrbit.Game.Objects;

namespace BotlyOrbit.Game.Interfaces
{
    internal interface IMeasurable
    {
        double GetDistance(Location2D baseLocation);
    }
}
