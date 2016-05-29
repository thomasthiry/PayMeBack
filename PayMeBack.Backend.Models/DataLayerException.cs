using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayMeBack.Backend.Models
{
    public class DataLayerException : Exception
    {
        public DataLayerException(string message) : base(message)
        {

        }
    }
}
