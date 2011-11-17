using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Threading;

namespace MobileHawk
{
    public class Output
    {
        // Connection details
        private string host;
        private int port;

        // Connection stuff
        protected Socket sendSocket;
        protected EndPoint sendEndPoint;

        public Output(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        /**
         * Send a command
         */
        public void send(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);

            sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sendEndPoint = new IPEndPoint(IPAddress.Parse(this.host), this.port);

            // Construct the args
            var args = new SocketAsyncEventArgs();
            args.RemoteEndPoint = sendEndPoint;
            args.Completed += SocketAsyncEventArgs_Completed;
            args.SetBuffer(buffer, 0, buffer.Length);

            // connect socket
            bool completesAsynchronously = sendSocket.ConnectAsync(args);

            // check if the completed event will be raised.
            if (!completesAsynchronously)
            {
                SocketAsyncEventArgs_Completed(args.ConnectSocket, args);
            }
        }

        private void SocketAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            // check for errors
            if (e.SocketError != SocketError.Success)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Comms error"));
            }
        }
    }
}
