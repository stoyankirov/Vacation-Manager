using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class PropertyUnitType
    {
        public PropertyUnitType()
        {
            PropertyUnits = new HashSet<PropertyUnit>();
        }

        public Guid Id { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<PropertyUnit> PropertyUnits { get; set; }
    }
}
