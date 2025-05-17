using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Documents;

internal class MemoryManager
{
    private IntPtr processHandle { get; set; }
    public int procId;

    // Rights for memory regions
    private const uint PROCESS_ALL_ACCESS = 0x1F0FFF;
    private const uint MEM_COMMIT = 0x1000;
    private const uint PAGE_READWRITE = 0x04;
    private const uint PAGE_GUARD = 0x100;
    private const uint PAGE_NOACCESS = 0x01;
    private const uint MEM_PRIVATE = 0x20000;

    public List<IntPtr> QueryMemory(byte[] pattern)
    {
        List<IntPtr> results = new List<IntPtr>();
        IntPtr currentAddress = IntPtr.Zero;

        while (VirtualQueryEx(processHandle, currentAddress, out MEMORY_BASIC_INFORMATION mbi, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))))
        {
            bool isCommitted = mbi.State == MEM_COMMIT;
            bool isPrivate = mbi.Type == MEM_PRIVATE;
            bool isReadableWritable = (mbi.Protect & PAGE_READWRITE) != 0;
            bool isNotNoAccess = (mbi.Protect & PAGE_NOACCESS) == 0;
            bool isNotGuard = (mbi.Protect & PAGE_GUARD) == 0;

            if (isCommitted && isPrivate && isReadableWritable && isNotNoAccess && isNotGuard && mbi.BaseAddress.ToInt64() > 0x100000000)
            {
                byte[] buffer = new byte[(int)mbi.RegionSize];
                if (ReadProcessMemory(processHandle, mbi.BaseAddress, buffer, buffer.Length, out int bytesRead))
                {
                    for (int i = 0; i < bytesRead - pattern.Length; i++)
                    {
                        bool match = true;
                        for (int j = 0; j < pattern.Length; j++)
                        {
                            if (buffer[i + j] != pattern[j])
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)
                        {
                            results.Add(new IntPtr(mbi.BaseAddress.ToInt64() + i));
                        }
                    }
                }
            }

            currentAddress = new IntPtr(currentAddress.ToInt64() + mbi.RegionSize.ToInt64());
        }

        return results;
    }


    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(
    IntPtr hProcess,
    IntPtr lpBaseAddress,
    [Out] byte[] lpBuffer,
    int dwSize,
    out int lpNumberOfBytesRead
);
    [DllImport("kernel32.dll")]
    static extern bool VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

    [StructLayout(LayoutKind.Sequential)]
    struct MEMORY_BASIC_INFORMATION
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public uint AllocationProtect;
        public IntPtr RegionSize;
        public uint State;
        public uint Protect;
        public uint Type;
    }

    [DllImport("kernel32.dll")]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

    public MemoryManager(int processId)
    {
        procId = processId;
        processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, procId);
    }

    public byte[] ReadBytes(IntPtr address, int size)
    {
        byte[] buffer = new byte[size];
        ReadProcessMemory(processHandle, address, buffer, size, out int bytesRead);
        return buffer;
    }

    public int ReadInt(IntPtr address) => BitConverter.ToInt32(ReadBytes(address, 4), 0);
    public long ReadLong(IntPtr address) => BitConverter.ToInt64(ReadBytes(address, 8), 0);
    public double ReadDouble(IntPtr address) => BitConverter.ToDouble(ReadBytes(address, 8), 0);
    public float ReadFloat(IntPtr address) => BitConverter.ToSingle(ReadBytes(address, 4), 0);
    public bool ReadBoolean(IntPtr address) => BitConverter.ToBoolean(ReadBytes(address, 1), 0);
    public IntPtr ReadPointer(IntPtr address) => new IntPtr(ReadLong(address));

    public bool WriteBytes(IntPtr address, byte[] data)
    {
        return WriteProcessMemory(processHandle, address, data, data.Length, out int bytesWritten) && bytesWritten == data.Length;
    }

    public bool WriteInt(IntPtr address, int value)
    {
        return WriteBytes(address, BitConverter.GetBytes(value));
    }

    public bool WriteLong(IntPtr address, long value)
    {
        return WriteBytes(address, BitConverter.GetBytes(value));
    }

    public bool WriteDouble(IntPtr address, double value)
    {
        return WriteBytes(address, BitConverter.GetBytes(value));
    }

    public bool WriteFloat(IntPtr address, float value)
    {
        return WriteBytes(address, BitConverter.GetBytes(value));
    }

    public bool WriteBoolean(IntPtr address, bool value)
    {
        return WriteBytes(address, BitConverter.GetBytes(value));
    }

}
