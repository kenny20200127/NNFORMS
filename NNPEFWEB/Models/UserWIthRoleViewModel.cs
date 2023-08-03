using System.Collections;
using System.Collections.Generic;

namespace NNPEFWEB.Models
{

    public class userCountModel
    {
      public int count1 { get; set; }
      public int count2 { get; set; }

      public IEnumerable<UserViewModels> users{get; set; }
    }
    public class UserWIthRoleViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public bool? IsAllowAccess { get; set; }
    }

    public class Rolevm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
