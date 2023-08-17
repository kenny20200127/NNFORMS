using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Enum;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public class DeathService : IDeathService
    {
        private readonly IUnitOfWorks unitOfWork;
        private readonly string connectionString;
        private readonly IDapper dapper;
        private readonly ILogger<DeathService> logger;
        public DeathService(ILogger<DeathService> logger, IDapper dapper)
        {
            this.dapper = dapper;
            this.logger = logger;
        }
        public async Task<BaseResponse> AddDeath(DeathViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 7);
                param.Add("@SVC_NO", model.SVC_NO);
                param.Add("@FirstName", model.FirstName);
                param.Add("@MiddleName", model.MiddleName);
                param.Add("@LastName", model.LastName);
                param.Add("@Title", model.Title);
               

                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<BaseResponse> ApproveDeath(ApproveDeathViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 6);
                param.Add("@PersonID", model.personID);
                param.Add("@createdby3", model.createdBy);
                param.Add("@status", model.status);


                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
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

        public DeathDto GetDeathCount()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 8);

                var respq = dapper.Get<DeathDto>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new DeathDto();
            }
        }

        public IEnumerable<SelectListItem> getOfficerdetails()
        {
            var serviceSelectListItem = new List<SelectListItem>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@status", 4);

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

        public DeathViewModel GetDeathByPersonID(int PersonID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 5);
                param.Add("@PersonID", PersonID);


                var respq = dapper.Get<DeathViewModel>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new DeathViewModel();
            }
        }

        public IEnumerable<DeathViewModel> GetDeathByStatus(string status)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 3);
                param.Add("@status", status);


                var respq = dapper.GetAll<DeathViewModel>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new List<DeathViewModel>();
            }
        }

        public IEnumerable<DeathViewModel> GetDeathInit()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput",10);
             
                var respq = dapper.GetAll<DeathViewModel>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new List<DeathViewModel>();
            }
        }

        public DeathViewModel GetDeathBySvcNo(string svcNo)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 4);
                param.Add("@SVC_NO", svcNo);


                var respq = dapper.Get<DeathViewModel>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new DeathViewModel();
            }
        }

        public PersonelDetailsViewModel GetPersonnelBySvcNo(string svcNo)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("@status", 6);
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
                param.Add("@status", 5);

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

        public async Task<BaseResponse> UpdateDeath(DeathViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 1);
                param.Add("@SVC_NO", model.SVC_NO);
                param.Add("FirstName", model.FirstName);
                param.Add("MiddleName", model.MiddleName);
                param.Add("@LastName", model.LastName);
                param.Add("@Title", model.Title);
                param.Add("@ShipEstab", model.ShipEstab);
                param.Add("@BirthDate", model.BirthDate);
                param.Add("@senioritydate", model.senioritydate);
                param.Add("@Prvsrv_from", model.Prevsrv_from);
                param.Add("@Prvsrv_to", model.Prevsrv_To);
                param.Add("@PrvEstab", model.PrvEstab);
                param.Add("@PrvAuthor", model.PrvAuthor);
                param.Add("@PrvEnListDate", model.PrvEnListDate);
                param.Add("@Deathdate", model.Deathdate);
                param.Add("@DeathReason", model.DeathReason);
                param.Add("@DeathConfirm", model.DeathConfirm);
                param.Add("@TradeClass", model.TradeClass);
                param.Add("@Prvwar_from", model.Postwar_from);
                param.Add("@Prvwar_to", model.Postwar_to);
                param.Add("@Warsrv_from", model.Warsrv_from);
                param.Add("@Warsrv_to", model.Warsrv_to);
                param.Add("@Postwar_from", model.Postwar_from);
                param.Add("@Postwar_to", model.Postwar_to);
                param.Add("@Nonrec_Service", model.Nonrec_Service);
                param.Add("@Totalrec_Service", model.Totalrec_Service);
                param.Add("@SubTreasury", model.SubTreasury);
                param.Add("@NOKName", model.NOKName);
                param.Add("@NOKAdress", model.NOKAdress);
                param.Add("@Totalgratuity", model.Totalgratuity);
                param.Add("@status", model.status);
                param.Add("@createdby", model.createdby);

                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
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
        public async Task<BaseResponse> CreateDeath(DeathViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 1);
                param.Add("@SVC_NO", model.SVC_NO);
                param.Add("FirstName", model.FirstName);
                param.Add("MiddleName", model.MiddleName);
                param.Add("@LastName", model.LastName);
                param.Add("@Title", model.Title);
                param.Add("@ShipEstab", model.ShipEstab);
                param.Add("@BirthDate", model.BirthDate);
                param.Add("@senioritydate", model.senioritydate);
                param.Add("@Prvsrv_from", model.Prevsrv_from);
                param.Add("@Prvsrv_to", model.Prevsrv_To);
                param.Add("@PrvEstab", model.PrvEstab);
                param.Add("@PrvAuthor", model.PrvAuthor);
                param.Add("@PrvEnListDate", model.PrvEnListDate);
                param.Add("@Deathdate", model.Deathdate);
                param.Add("@DeathReason", model.DeathReason);
                param.Add("@DeathConfirm", model.DeathConfirm);
                param.Add("@TradeClass", model.TradeClass);
                param.Add("@Prvwar_from", model.Postwar_from);
                param.Add("@Prvwar_to", model.Postwar_to);
                param.Add("@Warsrv_from", model.Warsrv_from);
                param.Add("@Warsrv_to", model.Warsrv_to);
                param.Add("@Postwar_from", model.Postwar_from);
                param.Add("@Postwar_to", model.Postwar_to);
                param.Add("@Nonrec_Service", model.Nonrec_Service);
                param.Add("@Totalrec_Service", model.Totalrec_Service);
                param.Add("@SubTreasury", model.SubTreasury);
                param.Add("@NOKName", model.NOKName);
                param.Add("@NOKAdress", model.NOKAdress);
                param.Add("@Totalgratuity", model.Totalgratuity);
                param.Add("@status", model.status);
                param.Add("@createdby", model.createdby);

                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.CreateDeathGratuity, param, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<BaseResponse> UpdateDeathForCPO(DeathViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 2);
                param.Add("@SVC_NO", model.SVC_NO);
                param.Add("@PresentBasicSal", model.PresentBasicSal);
                param.Add("@AnnualDiving", model.AnnualDiving);
                param.Add("@AnnualGCB", model.AnnualGCB);
                param.Add("@AnnualLodging", model.AnnualLodging);
                param.Add("@AnnualDMeal", model.AnnualDMeal);
                param.Add("@AnnualEntertain", model.AnnualEntertain);
                param.Add("@AnnualPersonal", model.AnnualPersonal);
                param.Add("@TotAnnualGross", model.TotAnnualGross);
                param.Add("@CashAdvance", model.CashAdvance);
                param.Add("@MotorVehicle", model.MotorVehicle);
                param.Add("@ReducedInterest", model.ReducedInterest);
                param.Add("@HousingLoan", model.HousingLoan);
                param.Add("@ReducedInterest2", model.ReducedInterest2);
                param.Add("@OtherSubCharge", model.OtherSubCharge);
                param.Add("@status", model.status);
                param.Add("@createdby2", model.createdby);

                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<BaseResponse> ApproveDeathPayment(ApproveDeathViewModel model)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput",9);
                param.Add("@PersonID", model.personID);
                param.Add("@createdby4", model.createdBy);
                param.Add("@status", model.status);


                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<BaseResponse> RemoveDeath(int personID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 13);
                param.Add("@personID",personID);
               
                var respq = await dapper.GetAsync<BaseResponse>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<IEnumerable<DeathReportModel>> getDeathpaidReport(DateTime start, DateTime end)
        {
            try
            {

                string newStartDate = reformatDate(start);
                string newEndDate = reformatDate(end);
                var param = new DynamicParameters();
                param.Add("@statusInput", 12);
                param.Add("@startdate", newStartDate);
                param.Add("@enddate", newEndDate);


                var respq = await dapper.GetAllAsync<DeathReportModel>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new List<DeathReportModel>();
            }
        }

        public async Task<IEnumerable<DeathReportModel>> getDeathStatusReport(string role)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 11);
                param.Add("@filterValue", role.ToLower());


                var respq = await dapper.GetAllAsync<DeathReportModel>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new List<DeathReportModel>();
            }
        }
        public async Task<DeathReportFormModel> getDeathFormsReport(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@statusInput", 14);
                param.Add("@PersonID", id);


                var respq = await dapper.GetAsync<DeathReportFormModel>(ApplicationConstant.Sp_Death, param, commandType: System.Data.CommandType.StoredProcedure);
                return respq;

            }
            catch (Exception ex)
            {
                logger.LogError($"Exception ===> {ex.Message}");
                return new DeathReportFormModel();
            }
        }

        public string reformatDate(DateTime date)
        {
            string mnth = date.Month.ToString();
            string day = date.Day.ToString();
            string year = date.Year.ToString();

            if (mnth.Length == 1)
                mnth = "0" + mnth;
            if (day.Length == 1)
                day = "0" + day;

            string output = year + "-" + mnth + "-" + day;
            return output;
        }
    }
}
