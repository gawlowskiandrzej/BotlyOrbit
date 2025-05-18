using BotlyOrbit.Game.Managers;

namespace BotlyOrbit.Game.Interfaces
{
    internal interface IModule
    {
        void Install(InitManager initObj);
        void UnInstall();
    }
}
