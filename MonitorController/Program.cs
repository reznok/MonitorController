using System;
using System.Runtime.InteropServices;

namespace MonitorController
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 1)
            {
                DisplayHelp();
                return;
            }

            switch (args[0])
            {
                case "monitor1":
                    MonitorController.InternalDisplay();
                    break;
                case "monitor2":
                    MonitorController.ExternalDisplay();
                    break;
                case "extend":
                    MonitorController.ExtendDisplays();
                    break;
                case "clone":
                    MonitorController.CloneDisplays();
                    break;
                default:
                    DisplayHelp();
                    break;
            }

        }   

        static void DisplayHelp()
        {
            Console.WriteLine("Usage: MonitorController.exe (monitor1|monitor2|extend|clone)");
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }

    static class MonitorController
    {

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern long SetDisplayConfig(uint numPathArrayElements,
        IntPtr pathArray, uint numModeArrayElements, IntPtr modeArray, uint flags);

        static UInt32 SDC_TOPOLOGY_INTERNAL = 0x00000001;
        static UInt32 SDC_TOPOLOGY_CLONE = 0x00000002;
        static UInt32 SDC_TOPOLOGY_EXTEND = 0x00000004;
        static UInt32 SDC_TOPOLOGY_EXTERNAL = 0x00000008;
        static UInt32 SDC_APPLY = 0x00000080;

        static public void CloneDisplays()
        {
            SetDisplayConfig(0, IntPtr.Zero, 0, IntPtr.Zero, (SDC_APPLY | SDC_TOPOLOGY_CLONE));
        }

        static public void ExtendDisplays()
        {
            SetDisplayConfig(0, IntPtr.Zero, 0, IntPtr.Zero, (SDC_APPLY | SDC_TOPOLOGY_EXTEND));
        }

        static public void ExternalDisplay()
        {
            SetDisplayConfig(0, IntPtr.Zero, 0, IntPtr.Zero, (SDC_APPLY | SDC_TOPOLOGY_EXTERNAL));
        }

        static public void InternalDisplay()
        {
            SetDisplayConfig(0, IntPtr.Zero, 0, IntPtr.Zero, (SDC_APPLY | SDC_TOPOLOGY_INTERNAL));
        }
    }
}

