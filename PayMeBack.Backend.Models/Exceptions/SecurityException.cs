using System;

namespace PayMeBack.Backend.Models
{
    public class SecurityException : Exception
    {
        public SecurityException(string message) : base(message)
        {

        }
    }
}
