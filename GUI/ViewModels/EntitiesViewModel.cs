using BotlyOrbit.Game.Objects;

namespace BotlyOrbit.GUI.ViewModels
{
    internal class EntitiesViewModel
    {
        public EntityList EntityList { get; set; }
        public EntitiesViewModel(EntityList entities)
        {
            EntityList = entities;
        }
    }
}
