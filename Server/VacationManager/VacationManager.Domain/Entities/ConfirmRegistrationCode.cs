using System;
using System.Collections.Generic;

#nullable disable

namespace VacationManager.Domain.Entities
{
    public partial class ConfirmRegistrationCode
    {
        public ConfirmRegistrationCode()
        {
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
