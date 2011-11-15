using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HawkSharp.Exceptions
{
    class MotorHawkException : HawkException
    {
        public MotorHawkException(string message) : base(message) { }
    }
}
