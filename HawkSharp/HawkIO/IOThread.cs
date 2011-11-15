using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HawkIO
{
    public abstract class IOThread
    {
        // host and port to listen on
        protected string host;
        protected int port;

        /**
         * Constructs with the host and port for this thread
         */
        public IOThread(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        /**
         * For the thread process action
         */
        public abstract void process();
    }
}
