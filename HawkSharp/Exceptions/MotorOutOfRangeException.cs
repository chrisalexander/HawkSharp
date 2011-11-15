using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HawkSharp.Exceptions
{
    class MotorOutOfRangeException: MotorHawkException
    {
        public MotorOutOfRangeException(string message) : base(message) { }
    }
}
