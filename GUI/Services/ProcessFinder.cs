using System.Diagnostics;
using System;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;

namespace BotlyOrbit.GUI.Services
{
    internal class ProcessFinder
    {
        public string ProcessName { get; set; }
        List<int> ProcessIdList { get; set; } = new List<int>();
        public ProcessFinder(string processName = "CefSharp.BrowserSubprocess")
        {
            ProcessName = processName;
        }
        public int FindPid()
        {
            // TODO: Make precise search for module
            // Search processes and find if it has flash player
            var processes = Process.GetProcessesByName(ProcessName);

            foreach (var proc in processes)
            {
                try
                {
                    string cmdLine = GetCommandLine(proc);
                    if (cmdLine.Contains("--type=ppapi"))
                    {
                        ProcessIdList.Add(proc.Id);           
                    }
                }
                catch
                {
                    return -1;
                }
            }

            return ProcessIdList.LastOrDefault();
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
