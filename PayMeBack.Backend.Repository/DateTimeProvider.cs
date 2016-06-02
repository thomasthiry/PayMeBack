using System;
using PayMeBack.Backend.Contracts.Services;

namespace PayMeBack.Backend.Repository
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
