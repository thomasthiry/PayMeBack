using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;

namespace PayMeBack.Backend.Contracts.Services
{
    public interface IContactService
    {
        Contact GetContactById(int id);

        IEnumerable<Contact> ListBySplitId(int splitId);

        Contact CreateIfNeededAndAddToSplit(int splitId, string email);

        SplitContact GetSplitContactById(int id);
    }
}
