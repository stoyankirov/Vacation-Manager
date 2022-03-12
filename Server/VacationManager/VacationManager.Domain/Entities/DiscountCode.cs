using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class DiscountCode
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public virtual Property Property { get; set; }
    }
}
