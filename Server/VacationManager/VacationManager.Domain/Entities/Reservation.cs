using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class Reservation
    {
        public Guid Id { get; set; }
        public Guid PropertyUnitId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Paid { get; set; }
        public decimal DueAmount { get; set; }

        public virtual PropertyUnit PropertyUnit { get; set; }
        public virtual User User { get; set; }
    }
}
