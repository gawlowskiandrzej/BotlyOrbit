using BotlyOrbit.GUI.Models;

namespace BotlyOrbit.GUI.ViewModels
{
    internal class StatsViewModel
    {
        public StatsModel StatModelObj { get; set; }
        public StatsViewModel()
        {
            StatModelObj = new StatsModel();
        }

    }
}
