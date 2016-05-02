using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;

namespace PayMeBack.Backend.Contracts.Services
{
    public interface IContactService
    {
        IEnumerable<Contact> ListBySplitId(int splitId);
    }
}
