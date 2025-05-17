using BotlyOrbit.Game.Other;
using System;

namespace BotlyOrbit.Game.Managers
{
    internal class InitManager
    {
        public MemoryManager MemoryManager{ get; set; }
        public InitManager(int procId)
        {
            MemoryManager = new MemoryManager(procId);
            Init();

        }
        public bool Init()
        {
            try
            {
                var initPtr = MemoryManager.QueryMemory(MemoryPatters.bytesToMainApplication);
                if (initPtr.Count > 1)
                    return false;

                var MainPtr = MemoryManager.ReadPointer(IntPtr.Subtract(initPtr[0], 228));

                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
            
        }
    }
}
