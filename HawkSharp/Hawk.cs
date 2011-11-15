using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HawkSharp
{
    public class Hawk
    {
        protected const string HAWK_DLL_PATH = "C:/hawk.dll";

        public const int TYPE_MOTOR = 1;

        private static bool init = false;
        private static int count = 0;

        [DllImport(HAWK_DLL_PATH)]
        private static extern int Sys_Initialise();

        [DllImport(HAWK_DLL_PATH)]
        private static extern int Sys_CloseAllDevices();

        [DllImport(HAWK_DLL_PATH)]
        protected static extern int Motor_SetType(int board, int type);

        /**
         * Constructor initialises the board if it has not done so already
         */
        public Hawk()
        {
            Hawk.count++;
            if (!Hawk.init)
            {
                Sys_Initialise();
                Hawk.init = true;
            }
        }

        /**
         * Destructor closes the device if needed
         */
        ~Hawk()
        {
            Hawk.count--;
            if (Hawk.init && Hawk.count < 1)
            {
                Sys_CloseAllDevices();
                Hawk.init = false;
            }
        }

        /*
        static void Main(string[] args)
        {
            Sys_Initialise();
                       
            int count = Sys_GetMotorHawkCount();
            Console.WriteLine(count.ToString() + " MotorHawk Found");
            if (count != 1) {
                Console.WriteLine("Need 1 MotorHawk");
                return;
            }

            int settype = Motor_SetType(1, 1);
            Console.WriteLine("Set type result " + settype.ToString());
            if (settype != 0)
            {
                Console.WriteLine("Needs to set motor correctly");
                return;
            }

            Console.WriteLine("Off");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 0, 0);

            Console.WriteLine("1 Forwards");
            Console.ReadLine();
            Motor_SetDCMotors(1, 250, 1, 0, 0);

            Console.WriteLine("Off");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 0, 0);

            Console.WriteLine("2 Forwards");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 250, 1);

            Console.WriteLine("Off");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 0, 0);

            Console.WriteLine("1 Reverse");
            Console.ReadLine();
            Motor_SetDCMotors(1, 250, 2, 0, 0);

            Console.WriteLine("Off");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 0, 0);

            Console.WriteLine("2 Reverse");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 250, 2);

            Console.WriteLine("Off");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 0, 0);

            Console.WriteLine("Forwards");
            Console.ReadLine();
            Motor_SetDCMotors(1, 250, 1, 250, 1);

            Console.WriteLine("Off");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 0, 0);

            Console.WriteLine("Reverse");
            Console.ReadLine();
            Motor_SetDCMotors(1, 250, 2, 250, 2);

            Console.WriteLine("Off");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 0, 0);

            Console.WriteLine("Left Turn");
            Console.ReadLine();
            Motor_SetDCMotors(1, 250, 1, 250, 2);

            Console.WriteLine("Off");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 0, 0);

            Console.WriteLine("Right Turn");
            Console.ReadLine();
            Motor_SetDCMotors(1, 250, 2, 250, 1);

            Console.WriteLine("Off");
            Console.ReadLine();
            Motor_SetDCMotors(1, 0, 0, 0, 0);

            Console.WriteLine("Exiting");
            Sys_CloseAllDevices();
        */
    }
}
