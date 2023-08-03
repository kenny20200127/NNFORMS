using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NNPEFWEB.ViewModel;
using System.Collections.Generic;

namespace NNPEFWEB.Models
{
    public class UserViewModels
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string UserName { get; set; }
        public string Fullname { get; set; }
        public string PhoneNo { get; set; }
        public int roleId { get; set; }
        public string RoleName { get; set; } 
        public List<SelectListItem> roleRecords { get; set; }
        public string PasswordHash { get; set; }
    }

    public class EditUserViewModels
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string UserName { get; set; }
        public string PhoneNo { get; set; }
        public int roleId { get; set; }
        public string RoleName { get; set; }
        public List<SelectListItem> roleRecords { get; set; }
       
    }

    public class UserView
    {
        public UserViewModels UserViewModels { get; set; }
        public PaginatedList<UserViewModels> Users { get; set; }
    }
}
