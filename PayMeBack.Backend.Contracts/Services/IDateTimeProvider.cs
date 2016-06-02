using System;

namespace PayMeBack.Backend.Contracts.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
