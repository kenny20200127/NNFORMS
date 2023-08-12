using Microsoft.AspNetCore.Mvc.Rendering;
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IPensionService
    {
        Task<IEnumerable<PensionReportModel>> getPensionpaidReport(DateTime start, DateTime end);
        Task<IEnumerable<PensionReportModel>> getPensionStatusReport(string role);
        IEnumerable<PensionViewModel> GetPensionInit();
        Task<BaseResponse> UpdatePensionPayment(ApprovePensionViewModel model);
        PensionDto GetPensionCount();
        Task<BaseResponse> AddPension(PensionViewModel model);
        Task<BaseResponse> ApprovePension(ApprovePensionViewModel model);
        IEnumerable<SelectListItem> getOfficerdetails();
        IEnumerable<SelectListItem> getRatingDetails();
        PersonelDetailsViewModel GetPersonnelBySvcNo(string svcNo);
        Task<BaseResponse> UpdatePension(PensionViewModel model);
        Task<BaseResponse> UpdatePensionForCPO(PensionViewModel model);
        IEnumerable<PensionViewModel> GetPensionByStatus(string status);
        PensionViewModel GetPensionBySvcNo(string svcNo);
        PensionViewModel GetPensionByPersonID(int PersonID);
        Task<BaseResponse> RemovePension(int PersonID);

    }
}
