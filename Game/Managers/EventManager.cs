using BotlyOrbit.Game.Objects;
using BotlyOrbit.Game.Other;
using System;
using System.Threading.Tasks;

namespace BotlyOrbit.Game.Managers
{
    internal class EventManager : Updatable
    {
        public double XClick { get; set; }
        public double YClick { get; set; }
        public double Xdest { get; set; }
        public double Ydest { get; set; }
        public bool IsRunning = false;
        public IntPtr LocationPtr { get; set; }
        public override void update(IntPtr address)
        {
            base.update(address);

            LocationPtr = MemoryManager.ReadPointer(Address+64);

            Xdest = MemoryManager.ReadDouble(LocationPtr + 32);
            Ydest = MemoryManager.ReadDouble(LocationPtr + 40);
        }
        public void setPos(Location2D loc)
        {
            Task.Factory.StartNew(() =>
            {
                while (true) 
                {
                    if (IsRunning)
                    {
                        MemoryManager.WriteDouble(LocationPtr + 32, XClick);
                        MemoryManager.WriteDouble(LocationPtr + 40, YClick);
                    } 
                }
            });
            
        }
    }
}
