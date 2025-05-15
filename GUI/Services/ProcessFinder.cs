using System.Diagnostics;
using System;

namespace BotlyOrbit.GUI.Services
{
    internal class ProcessFinder
    {
        public string ProcessName { get; set; }
        public ProcessFinder(string processName = "CefSharp.BrowserSubprocess")
        {
            ProcessName = processName;
        }
        public int FindPid()
        {
            var processes = Process.GetProcessesByName(ProcessName);

            foreach (var proc in processes)
            {
                try
                {
                    string cmdLine = GetCommandLine(proc);
                    if (cmdLine.Contains("--type=ppapi"))
                    {
                        return proc.Id;           
                    }
                }
                catch
                {
                    return -1;
                }
            }
            return -1;
        }
        string GetCommandLine(Process process)
        {
            using (var searcher = new System.Management.ManagementObjectSearcher(
                $"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {process.Id}"))
            {
                foreach (var @object in searcher.Get())
                {
                    return @object["CommandLine"]?.ToString() ?? "";
                }
            }
            return "";
        }
    }
}
