using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class Facility
    {
        public Facility()
        {
            PropertyFacilities = new HashSet<PropertyFacility>();
        }

        public Guid Id { get; set; }
        public string Facility1 { get; set; }

        public virtual ICollection<PropertyFacility> PropertyFacilities { get; set; }
    }
}
