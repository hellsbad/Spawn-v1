using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Spawnv1
{
  public class Mem
  {
    public static int m_iNumberOfBytesRead;
    public static int m_iNumberOfBytesWritten;
    public static Process m_Process;
    public static IntPtr m_pProcessHandle;
    public static int BaseAddress;
    private const int PROCESS_VM_OPERATION = 8;
    private const int PROCESS_VM_READ = 16;
    private const int PROCESS_VM_WRITE = 32;

    public static void Initialize(Process m_process, IntPtr baseAddr)
    {
      Mem.m_Process = m_process;
      Mem.BaseAddress = baseAddr.ToInt32();
      Mem.m_pProcessHandle = Mem.OpenProcess(56, false, Mem.m_Process.Id);
    }

    public static T ReadMemory<T>(int Adress) where T : struct
    {
      byte[] numArray = new byte[Marshal.SizeOf(typeof (T))];
      Mem.ReadProcessMemory((int) Mem.m_pProcessHandle, Adress, numArray, numArray.Length, ref Mem.m_iNumberOfBytesRead);
      return Mem.ByteArrayToStructure<T>(numArray);
    }

    public static void WriteMemory<T>(int Adress, object Value) where T : struct
    {
      byte[] byteArray = Mem.StructureToByteArray(Value);
      Mem.WriteProcessMemory((int) Mem.m_pProcessHandle, Adress, byteArray, byteArray.Length, out Mem.m_iNumberOfBytesWritten);
    }

    private static byte[] StructureToByteArray(object obj)
    {
      int length = Marshal.SizeOf(obj);
      byte[] destination = new byte[length];
      IntPtr num = Marshal.AllocHGlobal(length);
      Marshal.StructureToPtr(obj, num, true);
      Marshal.Copy(num, destination, 0, length);
      Marshal.FreeHGlobal(num);
      return destination;
    }

    private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
    {
      GCHandle gcHandle = GCHandle.Alloc((object) bytes, GCHandleType.Pinned);
      try
      {
        return (T) Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof (T));
      }
      finally
      {
        gcHandle.Free();
      }
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, out int lpNumberOfBytesWritten);
  }
}
