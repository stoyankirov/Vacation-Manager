using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class Property
    {
        public Property()
        {
            DiscountCodes = new HashSet<DiscountCode>();
            Images = new HashSet<Image>();
            PropertyFacilities = new HashSet<PropertyFacility>();
            PropertyUnits = new HashSet<PropertyUnit>();
            Reviews = new HashSet<Review>();
        }

        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public Guid TownId { get; set; }
        public Guid PropertyTypeId { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? OpenFrom { get; set; }
        public DateTime? OpenTo { get; set; }
        public bool IsOpenedAllYear { get; set; }

        public virtual User Owner { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual Town Town { get; set; }
        public virtual ICollection<DiscountCode> DiscountCodes { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<PropertyFacility> PropertyFacilities { get; set; }
        public virtual ICollection<PropertyUnit> PropertyUnits { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
