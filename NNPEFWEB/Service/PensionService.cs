using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Enum;
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public class PensionService : IPensionService
    {
        private readonly IDapper dapper;
        private readonly ILogger<PensionService> logger;


        public async Task<IEnumerable<PensionReportModel>> getPensionStatusReport(string role)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 11);
                param.Add("@filterValue", role.ToLower());


                var respq = await dapper.GetAllAsync<PensionReportModel>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new List<PensionReportModel>();
            }
        }

        public async Task<IEnumerable<PensionReportModel>> getPensionpaidReport(DateTime start, DateTime end)
        {
            try
            {

                string newStartDate = reformatDate(start);
                string newEndDate = reformatDate(end);
                var param = new DynamicParameters();
                param.Add("@statusInput", 12);
                param.Add("@startdate",newStartDate);
                param.Add("@enddate", newEndDate);


                var respq = await dapper.GetAllAsync<PensionReportModel>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new List<PensionReportModel>();
            }
        }
        public async Task<PensionReport> getPensionFormsReport(int id)
        {
            try
            {
                
                var param = new DynamicParameters();
                param.Add("@statusInput", 14);
                param.Add("@PersonID", id);

                var respq = await dapper.GetAsync<PensionReport>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new PensionReport();
            }
        }


        public PensionService(IDapper dapper, ILogger<PensionService> logger)
        {
            this.dapper = dapper;
            this.logger = logger;
        }

        public IEnumerable<SelectListItem> getOfficerdetails()
        {
            var serviceSelectListItem= new List<SelectListItem>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@status", 1);
              
                var personnelList = dapper.GetAll<PersonnelViewModel>(ApplicationConstant.SP_GetSVCDetails, param, commandType: System.Data.CommandType.StoredProcedure);

                serviceSelectListItem.Add(new SelectListItem()
                {
                    Text = "select service No",
                    Value = ""
                });
                personnelList.ForEach(x =>
                {
                    serviceSelectListItem.Add(new SelectListItem()
                    {
                        Text = x.details,
                        Value = x.SVC_NO
                    });
                });

                return serviceSelectListItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PersonelDetailsViewModel GetPersonnelBySvcNo(string svcNo)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@status",3);
                param.Add("@svcNo", svcNo);

                var singlePersonnel = dapper.Get<PersonelDetailsViewModel>(ApplicationConstant.SP_GetSVCDetails, param, commandType: System.Data.CommandType.StoredProcedure);
                return singlePersonnel;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ==>{ex.Message}");
                return new PersonelDetailsViewModel();
            }
        }

        public IEnumerable<SelectListItem> getRatingDetails()
        {
            var serviceSelectListItem = new List<SelectListItem>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@status",2);

                var personnelList = dapper.GetAll<PersonnelViewModel>(ApplicationConstant.SP_GetSVCDetails, param, commandType: System.Data.CommandType.StoredProcedure);

                serviceSelectListItem.Add(new SelectListItem()
                {
                    Text = "select service No",
                    Value = ""
                });

                personnelList.ForEach(x =>
                {
                    serviceSelectListItem.Add(new SelectListItem()
                    {
                        Text = x.details,
                        Value = x.SVC_NO
                    });
                });

                return serviceSelectListItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BaseResponse> UpdatePension(PensionViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput",1);
                param.Add("@SVC_NO", model.SVC_NO);
                param.Add("@FirstName",model.FirstName);
                param.Add("@MiddleName", model.MiddleName);
                param.Add("@LastName", model.LastName);
                param.Add("@Title", model.Title);
                param.Add("@ShipEstab", model.ShipEstab);
                param.Add("@BirthDate", model.BirthDate);
                param.Add("@senioritydate", model.senioritydate);
                param.Add("@Tradecategory", model.Tradecategory);
                param.Add("@TradeCatDate", model.TradeCatDate);
                param.Add("@PrvEstab", model.PrvEstab);
                param.Add("@PrvServdate", model.PrvServdate);
                param.Add("@PrvShipEstab", model.PrvShipEstab);
                param.Add("@Enlistmentdate", model.Enlistmentdate);
                param.Add("@PreServicefrom", model.PreServicefrom);
                param.Add("@PreServiceTo", model.PreServiceTo);
                param.Add("@Warsrv_from", model.Warsrv_from);
                param.Add("@Warsrv_to", model.Warsrv_to);
                param.Add("@TotalService", model.TotalService);
                param.Add("@Nonrec_Service", model.Nonrec_Service);
                param.Add("@NonRecReason", model.NonRecReason);
                param.Add("@TransAuthority", model.TransAuthority);
                param.Add("@FinTotalService", model.FinTotalService);
                param.Add("@PensionDate", model.PensionDate);
                param.Add("@PaymentDept", model.PaymentDept);
                param.Add("@PAddress", model.PAddress);
                param.Add("@CAddress", model.CAddress);
                param.Add("@DisabilityNat", model.DisabilityNat);
                param.Add("@DisabilityDegree", model.DisabilityDegree);
                param.Add("@DisabilityDate", model.DisabilityDate);
                param.Add("@status", model.status);
                param.Add("@createdby", model.createdBy);

                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch(Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new BaseResponse()
                {
                    responseCode = "05",
                    responseMessage = "Exception Occured"
                };
            }
        }
        public async Task<BaseResponse> CreatePension(PensionViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 1);
                param.Add("@SVC_NO", model.SVC_NO);
                param.Add("@FirstName", model.FirstName);
                param.Add("@MiddleName", model.MiddleName);
                param.Add("@LastName", model.LastName);
                param.Add("@Title", model.Title);
                param.Add("@ShipEstab", model.ShipEstab);
                param.Add("@BirthDate", model.BirthDate);
                param.Add("@senioritydate", model.senioritydate);
                param.Add("@Tradecategory", model.Tradecategory);
                param.Add("@TradeCatDate", model.TradeCatDate);
                param.Add("@PrvEstab", model.PrvEstab);
                param.Add("@PrvServdate", model.PrvServdate);
                param.Add("@PrvShipEstab", model.PrvShipEstab);
                param.Add("@Enlistmentdate", model.Enlistmentdate);
                param.Add("@PreServicefrom", model.PreServicefrom);
                param.Add("@PreServiceTo", model.PreServiceTo);
                param.Add("@Warsrv_from", model.Warsrv_from);
                param.Add("@Warsrv_to", model.Warsrv_to);
                param.Add("@TotalService", model.TotalService);
                param.Add("@Nonrec_Service", model.Nonrec_Service);
                param.Add("@NonRecReason", model.NonRecReason);
                param.Add("@TransAuthority", model.TransAuthority);
                param.Add("@FinTotalService", model.FinTotalService);
                param.Add("@PensionDate", model.PensionDate);
                param.Add("@PaymentDept", model.PaymentDept);
                param.Add("@PAddress", model.PAddress);
                param.Add("@CAddress", model.CAddress);
                param.Add("@DisabilityNat", model.DisabilityNat);
                param.Add("@DisabilityDegree", model.DisabilityDegree);
                param.Add("@DisabilityDate", model.DisabilityDate);
                param.Add("@status", model.status);
                param.Add("@createdby", model.createdBy);

                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.CreatePension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new BaseResponse()
                {
                    responseCode = "05",
                    responseMessage = "Exception Occured"
                };
            }
        }

        public async Task<BaseResponse> ApprovePension(ApprovePensionViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput",6);
                param.Add("@PersonID",model.personID);
                param.Add("@createdby3", model.createdBy);
                param.Add("@status",model.status);
               

                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new BaseResponse()
                {
                    responseCode = "05",
                    responseMessage = "Exception Occured"
                };
            }
        }

        public async Task<BaseResponse> AddPension(PensionViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput",7);
                param.Add("@SVC_NO",model.SVC_NO);
                param.Add("@FirstName",model.FirstName);
                param.Add("@MiddleName",model.MiddleName);
                param.Add("@LastName",model.LastName);
                param.Add("@Title",model.Title);

                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new BaseResponse()
                {
                    responseCode = "05",
                    responseMessage = "Exception Occured"
                };
            }
        }

        public async Task<BaseResponse> UpdatePensionForCPO(PensionViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 2);
                param.Add("@SVC_NO", model.SVC_NO);
                param.Add("@AnnualBasic", model.AnnualBasic);
                param.Add("@AnnualTrans", model.AnnualTrans);
                param.Add("@AnnualLodging", model.AnnualLodging);
                param.Add("@AnnualServant", model.AnnualServant);
                param.Add("@AnnualMeal", model.AnnualMeal);
                param.Add("@AnnualEnter", model.AnnualEnter);
                param.Add("@AnnualUtility", model.AnnualUtility);
                param.Add("@AnnualHouse", model.AnnualHouse);
                param.Add("@AnnualUtlity", model.AnnualUtlity);
                param.Add("@AnnualGeneral", model.AnnualGeneral);
                param.Add("@AnnualMedical", model.AnnualMedical);
                param.Add("@AnnualUniform", model.AnnualUniform);
                param.Add("@TotalEmolument", model.TotalEmolument);
                param.Add("@TotalQualify", model.TotalQualify);
                param.Add("@FreeSevcElement", model.FreeSevcElement);
                param.Add("@Housing_Loan", model.Housing_Loan);
                param.Add("@Motor_Loan", model.Motor_Loan);
                param.Add("@MCycle_Loan", model.MCycle_Loan);
                param.Add("@Welfare_Loan", model.Welfare_Loan);
                param.Add("@Coop_Loan", model.Coop_Loan);
                param.Add("@MFinance_Loan", model.MFinance_Loan);
                param.Add("@SalaryOverpayment", model.SalaryOverpayment);
                param.Add("@Other_Overcharge", model.Other_Overcharge);
                param.Add("@status", model.status);
                param.Add("@createdby2",model.createdBy);

                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new BaseResponse()
                {
                    responseCode = "05",
                    responseMessage = "Exception Occured"
                };
            }
        }

        public IEnumerable<PensionViewModel> GetPensionByStatus(string status)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput",3);
                param.Add("@status",status);
              

                var respq = dapper.GetAll<PensionViewModel>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new List<PensionViewModel>();
            }
        }

        public IEnumerable<PensionViewModel> GetPensionInit()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 10);
               

                var respq = dapper.GetAll<PensionViewModel>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new List<PensionViewModel>();
            }
        }

        public async Task<BaseResponse> UpdatePensionPayment(ApprovePensionViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 9);
                param.Add("@PersonID", model.personID);
                param.Add("@createdby4", model.createdBy);
                param.Add("@status", model.status);


                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new BaseResponse()
                {
                    responseCode = "05",
                    responseMessage = "Exception Occured"
                };
            }
        }
        public PensionViewModel GetPensionBySvcNo(string svcNo)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 4);
                param.Add("@SVC_NO", svcNo);


                var respq = dapper.Get<PensionViewModel>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new PensionViewModel();
            }
        }

        public PensionDto GetPensionCount()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 8);
               
                var respq = dapper.Get<PensionDto>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new PensionDto();
            }
        }

        public PensionViewModel GetPensionByPersonID(int PersonID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 5);
                param.Add("@PersonID",PersonID);


                var respq = dapper.Get<PensionViewModel>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new PensionViewModel();
            }
        }

        public async Task<BaseResponse> RemovePension(int PersonID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput",13);
                param.Add("@PersonID", PersonID);
                
                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Pension, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new BaseResponse()
                {
                    responseCode = "05",
                    responseMessage = "Exception Occured"
                };
            }
        }

        public string reformatDate(DateTime date)
        {
            string mnth=date.Month.ToString();
            string day = date.Day.ToString();
            string year = date.Year.ToString();

            if(mnth.Length==1)
                mnth = "0" + mnth;
            if(day.Length==1)
                day = "0" + day;

            string output=year+"-"+mnth+"-"+day;
            return output;
        } 
    }
}
