using PayMeBack.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayMeBack.Backend.IntegrationTests
{
    internal class ControllerWithContext<T>
    {
        public T Controller { get; set; }
        public PayMeBackContext Context { get; set; }
    }
}
