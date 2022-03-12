using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class Review
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid UserId { get; set; }
        public decimal OveralRating { get; set; }
        public decimal? LocationRating { get; set; }
        public decimal? ComfortRating { get; set; }
        public decimal? CleanlinessRating { get; set; }
        public string Comment { get; set; }

        public virtual Property Property { get; set; }
        public virtual User User { get; set; }
    }
}
