using BotlyOrbit.Game.Other;
using System;
using System.Windows;

namespace BotlyOrbit.Game.Managers
{
    internal class InitManager
    {
        MainApplicationManager MainApplicationManager { get; set; } = new MainApplicationManager();
        ScreenManager ScreenManager { get; set; } = new ScreenManager();
        GuiManager GuiManager { get; set; } = new GuiManager();

        public InitManager(int procId)
        {
            new MemoryManager(procId);
            Init();

        }
        public bool Init()
        {
            try
            {
                var initPtr = MemoryManager.QueryMemory(MemoryPatters.bytesToMainApplication);
                if (initPtr.Count > 1)
                    return false;

                var MainPtr = IntPtr.Subtract(initPtr[0], 228);
                MainApplicationManager.update(MainPtr + 1344);
                ScreenManager.update(MainApplicationManager.Address + 504);
                GuiManager.update(MainApplicationManager.Address + 512);

                return true;
            }
            catch (System.Exception e)
            {
                MessageBox.Show($"Cant initialize bot: {e.Message}");
                return false;
            }
            
        }
    }
}
