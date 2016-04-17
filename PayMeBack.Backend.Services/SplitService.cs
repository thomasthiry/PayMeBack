using System;
using System.Collections.Generic;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Contracts.Repositories;

namespace PayMeBack.Backend.Services
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class SplitService : ISplitService
    {
        private ISplitRepository _splitRepository;

        public SplitService(ISplitRepository splitRepository)
        {
            _splitRepository = splitRepository;
        }

        public Split Get(int id)
        {
            return _splitRepository.Get(id);
        }

        public IEnumerable<Split> List()
        {
            return _splitRepository.List();
        }

        public Split Create(string name, DateTime created)
        {
            return _splitRepository.Create(name, created);
        }
    }
}
