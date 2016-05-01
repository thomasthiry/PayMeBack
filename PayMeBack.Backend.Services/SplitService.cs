using System;
using System.Collections.Generic;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Contracts;

namespace PayMeBack.Backend.Services
{
    public class SplitService : ISplitService
    {
        private IGenericRepository<Split> _splitRepository;

        public SplitService(IGenericRepository<Split> splitRepository)
        {
            _splitRepository = splitRepository;
        }

        public Split Get(int id)
        {
            return _splitRepository.GetByID(id);
        }

        public IEnumerable<Split> List()
        {
            return _splitRepository.Get();
        }

        public Split Create(string name, DateTime created)
        {
            var split = new Split { Name = name, Created = created };
            return _splitRepository.Insert(split);
        }
    }
}
