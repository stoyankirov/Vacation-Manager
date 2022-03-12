using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class PropertyUnit
    {
        public PropertyUnit()
        {
            Reservations = new HashSet<Reservation>();
        }

        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid PropertyUnitTypeId { get; set; }
        public decimal NightPrice { get; set; }

        public virtual Property Property { get; set; }
        public virtual PropertyUnitType PropertyUnitType { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
