using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace NNPEFWEB.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ResetPasswordCode { get; set; }
        public string Rank { get; set; }
        public string ship { get; set; }
        public string command { get; set; }
        public string Appointment { get; set; }
        public bool personnel { get; set; }
        public bool shipcheck { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime expireDate { get; set; }
        
        public DateTime UpdatedOn { get; set; }
        public List<UserRole> UserRoles { get; set; }

        public bool? CanCheckPensionDeathGrantuity { get; set; }

    }
}
