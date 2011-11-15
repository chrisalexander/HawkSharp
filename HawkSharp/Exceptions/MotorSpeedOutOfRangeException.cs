using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HawkSharp.Exceptions
{
    class MotorSpeedOutOfRangeException : MotorHawkException
    {
        public MotorSpeedOutOfRangeException(string message) : base(message) { }
    }
}
