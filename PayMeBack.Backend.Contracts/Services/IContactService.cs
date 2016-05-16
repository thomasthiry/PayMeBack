using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;

namespace PayMeBack.Backend.Contracts.Services
{
    public interface IContactService
    {
        Contact GetContactById(int id);

        IEnumerable<SplitContact> ListSplitContactsBySplitId(int splitId);

        Contact CreateIfNeededAndAddToSplit(int splitId, string email);

        SplitContact GetSplitContactById(int id);

        void UpdateSplitContact(int splitContactId, decimal owes, decimal paid, string iban, string address, string comments);
    }
}
