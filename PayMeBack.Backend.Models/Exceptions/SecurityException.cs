using System;

namespace PayMeBack.Backend.Models
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message)
        {

        }
    }
}
