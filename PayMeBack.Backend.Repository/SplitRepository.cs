using System;
using System.Collections.Generic;
using PayMeBack.Backend.Contracts.Repositories;
using PayMeBack.Backend.Models;

namespace PayMeBack.Backend.Repository
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class SplitRepository : ISplitRepository
    {
        //private IList<SplitDto> _splits = new List<SplitDto> { new SplitDto { Id = 1, Name = "Today" },  new SplitDto { Id = 2, Name = "Tomorrow" } };

        public SplitRepository()
        {
        }

        public Split Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Split> List()
        {
            throw new NotImplementedException();
        }

        public Split Create(string name, DateTime created)
        {
            throw new NotImplementedException();
        }

        //public SplitDto Create(SplitCreationDto splitCreationDto)
        //{
        //    var splitDto = new SplitDto { Name = splitCreationDto.Name, Created = splitCreationDto.Created };
        //    splitDto.Id = GetNextSplitId();

        //    _splits.Add(splitDto);

        //    return splitDto;
        //}

        //private int GetNextSplitId()
        //{
        //    return _splits.Max(s => s.Id) + 1;
        //}
    }
}
