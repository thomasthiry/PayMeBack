using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;

namespace PayMeBack.Backend.Contracts.Services
{
    public interface ISplitService
    {
        IEnumerable<Split> List(int userId);

        Split Get(int id);

        Split Create(string name, DateTime created, int userId);

        Settlement Settle(int id);
    }
}
