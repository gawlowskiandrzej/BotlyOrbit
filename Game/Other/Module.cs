using BotlyOrbit.Game.Interfaces;
using BotlyOrbit.Game.Managers;

namespace BotlyOrbit.Game.Other
{
    abstract class Module : IModule
    {
        public InitManager InitManager { get; set; }
        public virtual void Install(InitManager initObj)
        {
            InitManager = initObj;
        }

        public virtual void UnInstall()
        {
            InitManager = null;
        }
    }
}
