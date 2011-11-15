using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HawkSharp.Exceptions
{
    class InsufficientMotorHawksException : MotorHawkException
    {
        public InsufficientMotorHawksException(string message) : base(message) { }
    }
}
