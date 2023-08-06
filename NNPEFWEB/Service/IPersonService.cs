using Microsoft.AspNetCore.Mvc.Rendering;
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IPersonService
    {
        List<SelectListItem> GetShip();
        MailClass SendActivationMailMessage(string to, string link);
        Task<BaseResponse> UpdateUser(EditUserViewModels payload);
        Task<BaseResponse> DeleteUser(int Id);
        Task<UserViewModels> GetUserById(int id);
        Task<IEnumerable<UserViewModels>> GetUsers(string name);
        ef_personnelLogin GetPersonBySvc_NO(string person);
        ef_personnelLogin personnelLogin(string username, string password);
        Task<ef_personnelLogin> GetPersonel(string svcno);
        Task<List<ef_personnelLogin>> GetAllPersonel();
        Task<bool> updatepersonlogin(ef_personnelLogin values);
        Task<ef_personnelLogin> GetPersonelByPassword(string password, string per);
        Task<ef_personnelLogin> GetPersonelByMail(string email);
        void updatepersonlogin2(ef_personnelLogin values);
        Task<ef_shiplogin> GetUserByShip(string person);
        void updateshiplogin(ef_shiplogin values);
        UserWIthRoleViewModel GetUserWithRole(int userId);
        IEnumerable<SelectListItem> GetRoles(string name);
        Task<BaseResponse> AddUser(UserViewModels payload);
        Task<userCountModel> GetUserCount();
    }
}
