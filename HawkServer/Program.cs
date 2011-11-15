using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HawkServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init server
            Server s = new Server("192.168.0.12", 6200);

            // Loop
            Console.WriteLine("Enter x to exit");
            string input = "";
            while (input != "x")
            {
                input = Console.ReadLine();
            }

            // Exit
            Console.WriteLine("Exiting");
            s.stop();
        }
    }
}
