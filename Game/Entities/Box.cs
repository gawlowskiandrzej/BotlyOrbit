using System;

namespace BotlyOrbit.Game.Entities
{
    internal class Box : Entity
    {
        public Box(IntPtr address) => update(address);
        public override void update(IntPtr address)
        {
            base.update(address);

        }
    }
}
