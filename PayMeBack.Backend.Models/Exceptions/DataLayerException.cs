using System;

namespace PayMeBack.Backend.Models.Exceptions
{
    public class DataLayerException : Exception
    {
        public DataLayerException(string message) : base(message)
        {

        }
    }
}
