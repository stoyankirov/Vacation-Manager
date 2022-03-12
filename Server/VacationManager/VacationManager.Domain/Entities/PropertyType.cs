using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class PropertyType
    {
        public PropertyType()
        {
            Properties = new HashSet<Property>();
        }

        public Guid Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
