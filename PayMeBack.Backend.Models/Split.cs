using System;

namespace PayMeBack.Backend.Models
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Split : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }
    }
}
