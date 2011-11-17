using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace HawkIO
{
    public class OutputThread : IOThread
    {
        // Socket to use
        protected Socket sendSocket;
        protected EndPoint sendEndPoint;

        // Messages to be sent
        private string message = "";

        /**
         * Construct and pass to parent
         */
        public OutputThread(string host, int port) : base(host, port)
        {
            // Init the socket
            sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sendEndPoint = new IPEndPoint(IPAddress.Parse(this.host), this.port);
        }

        /**
         * Public function to send a message
         */
        public void send(string output)
        {
            if (output != "")
            {
                this.message = output;
                this.process();
            }
        }

        /**
         * Process method to run the actual delivery
         */
        public override void process()
        {
            if (this.message != "")
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(this.message);
                sendSocket.SendTo(buffer, buffer.Length, SocketFlags.None, sendEndPoint);
            }
        }
    }
}
