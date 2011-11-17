using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace HawkIO
{
    public class InputThread : IOThread
    {
        // sockets stuff
        protected Socket receiveSocket;
        protected byte[] recBuffer;
        protected int BufferSize = 512;
        protected EndPoint bindEndPoint;

        // The last item received
        protected string lastReceived;

        // Delegate which is the signature for events to subscribe to
        public delegate void InputHandler(string input);

        // Event based on delegate
        public event InputHandler Input;

        /**
         * Pass construction to its parent
         */
        public InputThread(string host, int port) : base(host, port)
        {
            // Init the socket stuff
            this.receiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.bindEndPoint = new IPEndPoint(IPAddress.Parse(this.host), this.port);
            this.recBuffer = new byte[this.BufferSize];
            receiveSocket.Bind(bindEndPoint);
        }

        /**
         * Returns the last received item
         */
        public string getLastReceived()
        {
            return this.lastReceived;
        }

        /**
         * Connect and receive data for the thread
         */
        public override void process()
        {
            receiveSocket.BeginReceiveFrom(recBuffer, 0, recBuffer.Length, SocketFlags.None, ref bindEndPoint, new AsyncCallback(MessageReceivedCallback), (object)this);
        }

        /**
         * Message received callback from socket
         */
        private void MessageReceivedCallback(IAsyncResult result)
        {
            EndPoint remoteEndPoint = new IPEndPoint(0, 0);
            try
            {
                int bytesRead = receiveSocket.EndReceiveFrom(result, ref remoteEndPoint);
                this.lastReceived = System.Text.ASCIIEncoding.ASCII.GetString(recBuffer).Replace('\0', ' ').Trim();
                onInput(this.lastReceived);
            }
            catch (SocketException e)
            {
            }
            recBuffer = new byte[BufferSize];
            this.process();
        }

        /**
         * Triggers the events
         */
        private void onInput(string input)
        {
            if (Input != null)
            {
                // Call the handler if it exists
                Input(input);
            }
        }
    }
}
