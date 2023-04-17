using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace OmoriMacro
{
    static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        public const int VK_X = 0x58;
        public const int VK_Z = 0x5A;
        public const int VK_DOWN = 0x28;

        [STAThread]
        static void Main()
        {
            Console.WriteLine("Enable text skip, and set battle text to fast");
            Console.WriteLine("Press 1 for can farming");
            Console.WriteLine("Press 2 for auto recycling");
            Console.WriteLine("Just close the console when you're done using the macro or want to switch to a different one.");
            var key = Console.ReadKey().KeyChar;

            Console.Clear();
            switch (key)
            {
                case '1':
                    AutoFarm();
                    break;
                case '2':
                    AutoRecycle();
                    break;
                default:
                    Console.WriteLine("That wasn't an option you fucking cretin.");
                    Console.WriteLine("Just for that, you now have to relaunch the program yourself.");
                    Console.ReadKey();
                    break;
            }

        }

        static void AutoFarm()
        {
            Console.WriteLine("Go to the junk yard, and initiate combat with an enemy you can escape. (Like Dial-Up)");
            Console.WriteLine("Click the games window after starting the macro, and don't touch anything. Go do some chores or something.");
            Console.WriteLine("The can rate is atrocious, so this will take a while. (I got 21 cans in ~40 minutes)");
            Console.WriteLine("Press any key to start the macro...");
            Console.ReadKey();
            Console.WriteLine("Macro will start in 3 seconds...");
            Thread.Sleep(3000);
            Console.WriteLine("Macro running...");
            while (true)
            {
                PressAndHoldKey(VK_X, 2500);
                PressKey(VK_DOWN);
                Thread.Sleep(500);
                PressKey(VK_Z);
                Thread.Sleep(10500); // takes 13
            }
        }

        static void AutoRecycle()
        {
            Console.WriteLine("Just face the recycling machine.");
            Console.WriteLine("Press any key to start the macro...");
            Console.ReadKey();
            Console.WriteLine("Macro will start in 3 seconds...");
            Thread.Sleep(3000);
            Console.WriteLine("Macro running...");
            while (true)
            {
                Thread.Sleep(10);
                PressKey(VK_Z);
            }
        }

        static void PressKey(byte key)
        {
            keybd_event(key, 0, KEYEVENTF_EXTENDEDKEY, 0);
            Thread.Sleep(50);
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
        }

        static void PressAndHoldKey(byte key, int milliseconds)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                PressKey(key);
            } while (stopwatch.ElapsedMilliseconds < milliseconds);

            stopwatch.Stop();
        }
    }
}
