using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class Country
    {
        public Country()
        {
            Towns = new HashSet<Town>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
