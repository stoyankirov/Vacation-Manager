using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Properties = new HashSet<Property>();
            Reservations = new HashSet<Reservation>();
            Reviews = new HashSet<Review>();
        }

        public Guid Id { get; set; }
        public Guid ConfirmRegistrationCodeId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int Role { get; set; }
        public bool? IsBanned { get; set; }
        public bool? IsConfirmed { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual ConfirmRegistrationCode ConfirmRegistrationCode { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
