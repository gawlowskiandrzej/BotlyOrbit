using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

internal class MemoryManager
{
    private static IntPtr processHandle { get; set; }
    public int procId;

    // Rights for memory regions
    private const uint PROCESS_ALL_ACCESS = 0x1F0FFF;
    private const uint MEM_COMMIT = 0x1000;
    private const uint PAGE_READWRITE = 0x04;
    private const uint PAGE_GUARD = 0x100;
    private const uint PAGE_NOACCESS = 0x01;
    private const uint MEM_PRIVATE = 0x20000;

    public static List<IntPtr> QueryMemory(byte[] pattern)
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

    public static List<IntPtr> QueryMemoryInt(int value)
    {
        List<IntPtr> results = new List<IntPtr>();
        IntPtr currentAddress = IntPtr.Zero;

        byte[] targetBytes = BitConverter.GetBytes(value);

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
                    for (int i = 0; i <= bytesRead - 4; i++)
                    {
                        if (buffer[i] == targetBytes[0] &&
                            buffer[i + 1] == targetBytes[1] &&
                            buffer[i + 2] == targetBytes[2] &&
                            buffer[i + 3] == targetBytes[3])
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

    public static byte[] ReadBytes(IntPtr address, int size)
    {
        byte[] buffer = new byte[size];
        ReadProcessMemory(processHandle, address, buffer, size, out int bytesRead);
        return buffer;
    }

    public static int ReadInt(IntPtr address) => BitConverter.ToInt32(ReadBytes(address, 4), 0);
    public static long ReadLong(IntPtr address) => BitConverter.ToInt64(ReadBytes(address, 8), 0);
    public static double ReadDouble(IntPtr address) => BitConverter.ToDouble(ReadBytes(address, 8), 0);
    public static float ReadFloat(IntPtr address) => BitConverter.ToSingle(ReadBytes(address, 4), 0);
    public static bool ReadBoolean(IntPtr address) => BitConverter.ToBoolean(ReadBytes(address, 1), 0);
    public static IntPtr ReadPointer(IntPtr address) => new IntPtr(ReadLong(address));
    public static string ReadString(IntPtr address)
    {
        if (address == IntPtr.Zero)
            return "ERROR";

        int flags = ReadInt(address + 36);
        int width = (flags & 0x00000001);
        int size = ReadInt(address + 32) << width;
        int type = (flags & 0x00000006) >> 1;

        if (size > 512 || size < 0)
            return "ERROR";

        byte[] bytes;

        if (type == 2)
            bytes = ReadBytes(ReadPointer(ReadPointer(address + 24) + 16) + ReadInt(address + 16), size);
        else
            bytes = ReadBytes(ReadPointer(address + 16), size);

        return width == 0 ? Encoding.Default.GetString(bytes) : System.Text.Encoding.Default.GetString(bytes);
    }

    public bool WriteString(IntPtr address, string text, Encoding encoding = null, bool nullTerminated = true)
    {
        encoding = Encoding.UTF8;
        byte[] stringBytes = encoding.GetBytes(text);

        if (nullTerminated)
        {
            byte[] terminated = new byte[stringBytes.Length + 1];
            Array.Copy(stringBytes, terminated, stringBytes.Length);
            return WriteBytes(address, terminated);
        }
        else
        {
            return WriteBytes(address, stringBytes);
        }
    }

    public static bool WriteBytes(IntPtr address, byte[] data) => WriteProcessMemory(processHandle, address, data, data.Length, out int bytesWritten) && bytesWritten == data.Length;

    public static bool WriteInt(IntPtr address, int value) => WriteBytes(address, BitConverter.GetBytes(value));

    public static bool WriteLong(IntPtr address, long value) => WriteBytes(address, BitConverter.GetBytes(value));
    
    public static bool WriteDouble(IntPtr address, double value) => WriteBytes(address, BitConverter.GetBytes(value));
    
    public static bool WriteFloat(IntPtr address, float value) => WriteBytes(address, BitConverter.GetBytes(value));
    
    public static bool WriteBoolean(IntPtr address, bool value) => WriteBytes(address, BitConverter.GetBytes(value));
   
}
