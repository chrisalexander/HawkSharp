using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HawkIO;
using System.Threading;

namespace HawkClient
{
    class Client
    {
        // The output thread
        private OutputThread o;
        private Thread oThread;

        /**
         * Initialises the client
         */
        public Client(string host, int port)
        {
            o = new OutputThread(host, port);
            oThread = new Thread(new ThreadStart(o.process));

            // Start up the output thread
            oThread.Start();
            Console.WriteLine("Waiting for thread to start");
            while (!oThread.IsAlive) ;
            Console.WriteLine("Thread started");
        }

        /**
         * Sends a message on the output thread
         */
        public void send(string message)
        {
            o.send(message);
        }

        /**
         * Stop the client
         */
        public void stop()
        {
            oThread.Abort();
            oThread.Join();
        }  
    }
}
