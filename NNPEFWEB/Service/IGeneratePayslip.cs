using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NNPEFWEB.ViewModel;
using System.Collections.Generic;

namespace NNPEFWEB.Service
{
    public interface IGeneratePayslip
    {
        PayslipModel ProcessPayslip(string svcNo, string month, string year);
        List<PayslipViewModel> GetpaySlip(string month, string svcNo, string year);
        List<Payslip2ViewModel> GetpayslipMonth();
        List<SelectListItem> GetFilterMonth(List<Payslip2ViewModel> listModel);
        List<SelectListItem> GetFilterYear(List<Payslip2ViewModel> listModel);

    }
}
