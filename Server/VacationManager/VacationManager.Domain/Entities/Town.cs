using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class Town
    {
        public Town()
        {
            Properties = new HashSet<Property>();
        }

        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string Name { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
