using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HawkSharp.Motor;
using HawkIO;
using System.Net;
using System.Threading;

namespace HawkServer
{
    class Server
    {
        // The motor hawk to use
        private MotorHawk hawk;

        // The input thread
        protected InputThread i;
        protected Thread iThread;

        public Server(string host, int port)
        {
            hawk = new MotorHawk(1);
            hawk.stop();

            i = new InputThread(host, port);
            iThread = new Thread(new ThreadStart(i.process));

            i.Input += new InputThread.InputHandler(handler);

            // Start up the thread
            iThread.Start();
        }

        /**
         * Stops the server
         */
        public void stop()
        {
            iThread.Abort();
            iThread.Join();
        }

        private void handler(string input)
        {
            switch (input)
            {
                case "f":
                    hawk.forwards();
                    break;
                case "b":
                    hawk.backwards();
                    break;
                case "l":
                    hawk.left();
                    break;
                case "r":
                    hawk.right();
                    break;
                case "s":
                default:
                    hawk.stop();
                    break;
            }
        }
    }
}
