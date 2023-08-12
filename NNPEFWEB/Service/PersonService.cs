using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Data;
using NNPEFWEB.Enum;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWorks unitOfWork;
        private readonly string connectionString;
        private readonly IDapper dapper;
        private readonly ILogger<PersonService> logger;
        private readonly ApplicationDbContext context;
        public PersonService(ApplicationDbContext _context, IConfiguration configuration, ILogger<PersonService> logger, IUnitOfWorks unitOfWork, IDapper dapper)
        {
            this.unitOfWork = unitOfWork;
            connectionString = configuration.GetConnectionString("DefaultConnection");
            this.dapper = dapper;
            this.logger = logger;
            context = _context;
        }
        public Task<ef_shiplogin> GetUserByShip(string person)
        {
            return unitOfWork.PersonLogin.GetUserByShip(person);
        }
        public ef_personnelLogin GetPersonBySvc_NO(string person)
        {
            return unitOfWork.PersonLogin.GetPersonBySVC_No(x => x.svcNo == person);
        }

        public Task<ef_personnelLogin> GetPersonel(string svcno)
        {
            return unitOfWork.PersonLogin.GetPersonnel(svcno);
        }
        public Task<ef_personnelLogin> GetPersonelByPassword(string password, string per)
        {
            return unitOfWork.PersonLogin.GetPersonnelBypassword(password, per);
        }
        public async Task<bool> updatepersonlogin(ef_personnelLogin values)
        {
            unitOfWork.PersonLogin.Update(values);
            return await unitOfWork.Done();
        }

        public List<SelectListItem> GetShip()
        {
            var shipList = (from rk in context.ef_ships
                            select new SelectListItem()
                            {
                                Text = rk.shipName,
                                Value = rk.shipName
                            }).ToList();

            shipList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return shipList;
        }

        public ef_personnelLogin personnelLogin(string username, string password)
        {
            return unitOfWork.PersonLogin.GetPersonnelLogin(username, password);
        }

        public async Task<List<ef_personnelLogin>> GetAllPersonel()
        {
            return await unitOfWork.PersonLogin.GetAllPersonnel();
        }
        public Task<ef_personnelLogin> GetPersonelByMail(string email)
        {
            return unitOfWork.PersonLogin.GetPersonnelByMail(email);
        }
        public void updatepersonlogin2(ef_personnelLogin values)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("ResetPassword", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@username", values.userName));
                        cmd.Parameters.Add(new SqlParameter("@password", values.password));
                        cmd.Parameters.Add(new SqlParameter("@expiredate", values.expireDate));

                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {

            }
        }
        public void updateshiplogin(ef_shiplogin values)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("ResetShipPassword", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@username", values.userName));
                        cmd.Parameters.Add(new SqlParameter("@password", values.password));
                        cmd.Parameters.Add(new SqlParameter("@expiredate", values.expireDate));

                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {

            }
        }

        public UserWIthRoleViewModel GetUserWithRole(int userId)
        {
            var result = new UserWIthRoleViewModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("@userId", userId);

                result = dapper.Get<UserWIthRoleViewModel>(ApplicationConstant.sp_GetUserWithRoles, param, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<SelectListItem> GetRoles(string name)
        {
            var roleListItems = new List<SelectListItem>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@status", 1);
               // param.Add("@role", name);
                var roleList = dapper.GetAll<Rolevm>(ApplicationConstant.sp_users, param, commandType: System.Data.CommandType.StoredProcedure);
                roleList.ForEach(x =>
                {
                    roleListItems.Add(new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    });
                });


                return roleListItems;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserViewModels>> GetUsers(string name)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("@status", 3);
                //param.Add("@role", name);
                var userlist = await dapper.GetAllAsync<UserViewModels>(ApplicationConstant.sp_users, param, commandType: System.Data.CommandType.StoredProcedure);


                return userlist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserViewModels> GetUserById(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@status", 4);
                param.Add("@Id", id);
                var userlist = await dapper.GetAsync<UserViewModels>(ApplicationConstant.sp_users, param, commandType: System.Data.CommandType.StoredProcedure);


                return userlist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BaseResponse> DeleteUser(int Id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@status", 6);
                param.Add("@Id", Id);

                var resp = await dapper.GetAsync<BaseResponse>(ApplicationConstant.sp_users, param, commandType: System.Data.CommandType.StoredProcedure);

                return resp;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception occurs with message==> {ex.Message}");
                return null;
            }
        }

        public async Task<BaseResponse> UpdateUser(EditUserViewModels payload)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("@status", 5);
                param.Add("@firstname", payload.FirstName);
                param.Add("@lastname", payload.LastName);
                param.Add("@username", payload.UserName);
                param.Add("@email", payload.email);
                param.Add("@roleId", payload.roleId);
                param.Add("@phoneNo", payload.PhoneNo);
                param.Add("@Id", payload.Id);

                var resp = await dapper.GetAsync<BaseResponse>(ApplicationConstant.sp_users, param, commandType: System.Data.CommandType.StoredProcedure);

                return resp;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception occurs with message==> {ex.Message}");
                return null;
            }
        }

        public async Task<BaseResponse> AddUser(UserViewModels payload)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("@status", 2);
                param.Add("@firstname", payload.FirstName);
                param.Add("@lastname", payload.LastName);
                param.Add("@username", payload.UserName);
                param.Add("@email", payload.email);
                param.Add("@roleId", payload.roleId);
                param.Add("@phoneNo", payload.PhoneNo);
                param.Add("@PasswordHash", payload.PasswordHash);

                var resp = await dapper.GetAsync<BaseResponse>(ApplicationConstant.sp_users, param, commandType: System.Data.CommandType.StoredProcedure);

                return resp;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception occurs with message==> {ex.Message}");
                return null;
            }
        }

        public async Task<userCountModel> GetUserCount()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@status", 7);


                var resp = await dapper.GetAsync<userCountModel>(ApplicationConstant.sp_users, param, commandType: System.Data.CommandType.StoredProcedure);

                return resp;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception occurs with message==> {ex.Message}");
                return null;
            }
        }

        public MailClass SendActivationMailMessage(string to, string link)
        {
            var mail = new MailClass();
            mail.bodyText = "kindly click this link to activate your mail\n:" + link;
            mail.fromName = "NN-CPO";
            mail.to = to;
            mail.subject = "NNCPO Activation Mail";


            return mail;
        }
    }
}