using Microsoft.AspNetCore.Mvc.Rendering;
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IDeathService
    {
        Task<DeathReportFormModel> getDeathFormsReport(int id);
        Task<IEnumerable<DeathReportModel>> getDeathpaidReport(DateTime start, DateTime end);
        Task<IEnumerable<DeathReportModel>> getDeathStatusReport(string role);
        Task<BaseResponse> ApproveDeathPayment(ApproveDeathViewModel model);
        IEnumerable<DeathViewModel> GetDeathInit();
        DeathDto GetDeathCount();
        Task<BaseResponse> AddDeath(DeathViewModel model);
        Task<BaseResponse> ApproveDeath(ApproveDeathViewModel model);
        IEnumerable<SelectListItem> getOfficerdetails();
        IEnumerable<SelectListItem> getRatingDetails();
        PersonelDetailsViewModel GetPersonnelBySvcNo(string svcNo);
        Task<BaseResponse> UpdateDeath(DeathViewModel model);
        Task<BaseResponse> CreateDeath(DeathViewModel model);
        Task<BaseResponse> UpdateDeathForCPO(DeathViewModel model);
        IEnumerable<DeathViewModel> GetDeathByStatus(string status);
       
        DeathViewModel GetDeathBySvcNo(string svcNo);
        DeathViewModel GetDeathByPersonID(int PersonID);
        Task<BaseResponse> RemoveDeath(int personID);

    }
}
