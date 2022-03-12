using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class PropertyFacility
    {
        public Guid PropertyId { get; set; }
        public Guid FacilityId { get; set; }

        public virtual Facility Facility { get; set; }
        public virtual Property Property { get; set; }
    }
}
