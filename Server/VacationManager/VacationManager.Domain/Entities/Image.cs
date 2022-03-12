using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class Image
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public string SourcePath { get; set; }

        public virtual Property Property { get; set; }
    }
}
