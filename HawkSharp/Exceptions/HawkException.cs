using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HawkSharp.Exceptions
{
    class HawkException : Exception
    {
        public HawkException(string message) : base(message) {}
    }
}
