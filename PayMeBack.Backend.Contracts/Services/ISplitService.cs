using PayMeBack.Backend.Models;
using System;
using System.Collections.Generic;

namespace PayMeBack.Backend.Contracts.Services
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public interface ISplitService
    {
        IEnumerable<Split> List();

        Split Get(int id);

        Split Create(string name, DateTime created);

        Settlement Settle(int id);
    }
}
