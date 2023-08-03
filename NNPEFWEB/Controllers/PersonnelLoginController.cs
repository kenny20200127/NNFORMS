using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using NETCore.MailKit.Core;
using NNPEFWEB.Data;
using NNPEFWEB.Enum;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.Services;
using NNPEFWEB.ViewModel;
using OfficeOpenXml;
using Wkhtmltopdf.NetCore;


namespace NNPEFWEB.Controllers
{
    public class PersonnelLoginController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IPersonService personService;
        private readonly IPersonInfoService personInfoService;
        private readonly IEmailService _EmailService;
        private readonly IUnitOfWorks unitOfWorks;
        private readonly IConfiguration config;
        private readonly IMailService _mailService;
        private readonly IEmailSenderService _senderService;
        private readonly IAuthService authenticationService;
        private readonly IUserService userService;
        private readonly IPensionService service;
        private readonly IDeathService deathService;
        private readonly UserManager<User> userManager;
        private readonly IGeneratePayslip generatePayslip;
        private readonly SignInManager<User> signInManager;
        private readonly IGeneratePdf generatePdf;
        private int resetcode;
        Random rnd = new Random();
        private readonly ILogger<PersonnelLoginController> _logger;

        public PersonnelLoginController
         (
            SignInManager<User> signInManager,
            IGeneratePdf generatePdf,
            IGeneratePayslip generatePayslip,
            UserManager<User> userManager,
            ILogger<PersonnelLoginController> logger,
            ApplicationDbContext context,IPersonInfoService personInfoService,
            IPersonService personService, IUnitOfWorks unitOfWorks, IConfiguration config,
            IMailService mailService,
            IEmailSenderService senderService,
            IAuthService authenticationService,
            IUserService userService,IPensionService service,IDeathService deathService
            ) : base(userService)

        {
            this.signInManager = signInManager;
            this.generatePdf = generatePdf;
            this.generatePayslip = generatePayslip;
            this.userManager = userManager;
            _logger = logger;
            this.personService = personService;
            this.unitOfWorks = unitOfWorks;
            this.config = config;
            this.personInfoService = personInfoService;
            _context = context;
            _mailService = mailService;
            _senderService = senderService;
            this.authenticationService = authenticationService;
            this.userService = userService;
            this.service = service;
            this.deathService = deathService;
        }


        

        public IActionResult Index()
        {
            UserViewModels userView = new();
            userView.roleRecords = personService.GetRoles().ToList();
            return View(userView);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUsers(EditUserViewModels model)
        {
            var result = await personService.UpdateUser(model);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] int Id)
        {
            var result = await personService.GetUserById(Id);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser([FromQuery] int Id)
        {
            var result = await personService.DeleteUser(Id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers(UserViewModels model2)
        {
            var hasher = new PasswordHasher<User>();
            var defaultPass = RandomString(10);
            var passwordHash = hasher.HashPassword(null, defaultPass);
            model2.PasswordHash = passwordHash;

            var result = await personService.AddUser(model2);

            return Ok(result);
        }

        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(personLoginVM login)
        {
            try
            {
                var userlogin = await userService.GetUserByUserName(login.userName);

                if (userlogin == null)
                {
                    TempData["ErrorMessage"] = "User Not Found";
                    return RedirectToAction("login2", "PersonnelLogin");
                }
                if (ModelState.IsValid)
                {
                    if (userlogin.ResetPasswordCode != null)
                    {
                        if (string.IsNullOrWhiteSpace(login.password))
                        {
                            TempData["errorMessage"] = "Please enter you password";
                            return RedirectToAction("Login2", "PersonnelLogin");
                        }
                        else
                        {
                            if (userlogin.personnel == true)
                            {
                                var auth2 = await authenticationService.SignInUserAsync(login.userName, login.password, "false");

                                if (!auth2.Success)
                                {
                                    TempData["ErrorMessage"] = auth2.Message;
                                    return new RedirectResult(RefererUrl());
                                }

                                //get user with role
                                var userloginwithrole = personService.GetUserWithRole(userlogin.Id);

                                //create a userclaim

                                TempData["username"] = userlogin.UserName;
                                TempData["userRole2"] = userloginwithrole.RoleName;
                                HttpContext.Session.SetString("userRole", userloginwithrole.RoleName);
                                HttpContext.Session.SetString("PersonnelName", userlogin.LastName + " " + userlogin.FirstName);

                                if (userloginwithrole.RoleName.ToLower() == "systemadmin")
                                {
                                    return RedirectToAction("AdminIndex", "PersonnelLogin");
                                }
                                else
                                {
                                    return RedirectToAction("PensionGratuity", "PersonnelLogin");
                                }
                            }
                            else
                            {
                                TempData["errorMessage"] = "Invalid credentials";
                                return RedirectToAction("Login2", "PersonnelLogin");
                            }
                        }
                        
                    }
                    else
                    {

                        HttpContext.Session.SetString("SVC_No", userlogin.UserName);
                        return RedirectToAction("ResetPasswordServiceNumber");
                    }

                }
                ModelState.AddModelError(string.Empty, "Invalid UserName Or Password");
                return ValidationProblem(instance: "104", modelStateDictionary: ModelState);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                TempData["errorMessage"] = "Unable to login contact admin";
                return View("Login");
            }
        }
        public async Task<IActionResult> GetAdminUser(int ?pageNumber,string searchString)
        {
            UserView model = new UserView();
            UserViewModels models= new UserViewModels();
            models.roleRecords = personService.GetRoles().ToList();
            var listOfusers = await personService.GetUsers();

            if (!String.IsNullOrEmpty(searchString))
            {
                listOfusers = listOfusers.Where(s => s.UserName.ToLower()==searchString.ToLower());
            }


            int pageSize = 3;
            model.UserViewModels = models;
            var zz = await PaginatedList<UserViewModels>.CreateAsync(listOfusers.AsQueryable(), pageNumber ?? 1, pageSize);
            model.Users= zz;
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewRole()
        {
            var roles = personService.GetRoles().ToList();
            return Ok(roles) ;
        }

        public IActionResult PensionGratuity()
        {

            Pension pension = new();
            pension.pensionDto = service.GetPensionCount();
            pension.deathDto= deathService.GetDeathCount();

            return View(pension);
        }

        public async Task<IActionResult> PensionInitiation(int ? pageNumber,string searchString)
        {
            PensionModel model = new();
            PensionViewModel model2 = new PensionViewModel();
            string role = TempData["userRole2"] as string;

            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            
            var InitPensionList =  filterPensionByRole(service.GetPensionInit(),role);
            if (!String.IsNullOrEmpty(searchString))
            {
                InitPensionList = InitPensionList.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }
            int pageSize = 3;
            model.Pensions = await PaginatedList<PensionViewModel>.CreateAsync(InitPensionList.AsQueryable(), pageNumber ?? 1, pageSize);
            model2.shipDetails = GetShip();
            model2.rankDetails = GetRank();
            model.pension = model2;

            TempData["role"] = role;
            return View(model);
        }

        public async Task<IActionResult> PensionReviews(int ? pageNumber,string searchString)
        {
            PensionModel model = new PensionModel();
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            PensionViewModel model2 = new PensionViewModel();
            var pensionForReview = filterPensionByRole(service.GetPensionByStatus(ApplicationConstant.Initiation),role);
            if (!String.IsNullOrEmpty(searchString))
            {
                pensionForReview = pensionForReview.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }
            var pp = pensionForReview.ToList();
            model2.shipDetails = personService.GetShip();
            model.pension = model2;
            int pageSize = 3;
            model.Pensions= await PaginatedList<PensionViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize);
            TempData["role"] = role;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePensionInitiation(PensionModel model)
        {

            string createdBy = TempData["username"] as string;
            PensionViewModel viewModel = new PensionViewModel();
            viewModel = model.pension;
            viewModel.createdBy = createdBy;
            viewModel.status = ApplicationConstant.Initiation;
           // model.serviceDetails = TempData["serviceDetails"] as List<SelectListItem>;
            var result = await service.UpdatePension(viewModel);
            TempData["messageInit"] = result.responseMessage;
            TempData["user"] = createdBy;
            ModelState.Clear();
            return RedirectToAction("PensionInitiation", "PersonnelLogin");
        }


        [HttpGet]
        public async Task<IActionResult> PensionUpdate(int? pageNumber,string searchString)
        {
            PensionModel model = new PensionModel();
            PensionViewModel model2 = new PensionViewModel();

            var pensionByInitation =  service.GetPensionByStatus(ApplicationConstant.Initiation);
            //model.Pensions = pp;
            if (!String.IsNullOrEmpty(searchString))
            {
                pensionByInitation = pensionByInitation.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            model2.shipDetails=personService.GetShip();
            model.pension = model2;
            int pageSize = 3;
            model.Pensions=await PaginatedList<PensionViewModel>.CreateAsync(pensionByInitation.AsQueryable(), pageNumber ?? 1, pageSize);

            return View(model);
        }

        public async Task<IActionResult> PensionUpdateReview(int ? pageNumber, string searchString)
        {
            PensionModel model = new PensionModel();
            PensionViewModel mm=new PensionViewModel();
            mm.shipDetails = personService.GetShip();
            var pensionUpdateForReview = service.GetPensionByStatus(ApplicationConstant.Update);
            

            if (!String.IsNullOrEmpty(searchString))
            {
                pensionUpdateForReview = pensionUpdateForReview.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            var pp = pensionUpdateForReview.ToList();
            //model.Pensions = pp;
            int pageSize = 3;
            model.Pensions= await PaginatedList<PensionViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize);
            return View(model);
         
        }

        [HttpPost]
        public async Task<IActionResult> CreatePensionUpdate(PensionModel model)
        {
            PensionViewModel viewModel = new PensionViewModel();
            viewModel = model.pension;
            string createdBy = TempData["username"] as string;
            viewModel.createdBy = createdBy;
            viewModel.status = ApplicationConstant.Update;
            

            var result = await service.UpdatePensionForCPO(viewModel);
            return Ok(result);
        }


        public async Task<IActionResult> ApprovePension(int? pageNumber,string searchString)
        {
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            var pensionForApproval = filterPensionByRole(service.GetPensionByStatus(ApplicationConstant.Update),role);
            if (!String.IsNullOrEmpty(searchString))
            {
                pensionForApproval = pensionForApproval.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            int pageSize = 3;
            TempData["role"] = role;
            return View(await PaginatedList<PensionViewModel>.CreateAsync(pensionForApproval.AsQueryable(), pageNumber ?? 1, pageSize));    
        }

        public async Task<IActionResult> PensionPayment(int? pageNumber)
        {
            var pensionForApproval = service.GetPensionByStatus(ApplicationConstant.Approved);
            int pageSize = 3;
            return View(await PaginatedList<PensionViewModel>.CreateAsync(pensionForApproval.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        [Route("PersonnelLogin/CreatePensionPayment")]
        public async Task<IActionResult> CreatePensionPayment([FromQuery] int PersonID)
        {
            string createdBy = TempData["username"] as string;
            ApprovePensionViewModel model = new();
            model.personID = PersonID;
            model.status = ApplicationConstant.Payment;
            model.createdBy = createdBy;
            var pp = await service.UpdatePensionPayment(model);

            return Ok(pp);
        }

        public async Task<IActionResult> AdminIndex()
        {
            userCountModel model = new();
            var countUser= await personService.GetUserCount();
            model.count1 = countUser.count1;
            model.count2 = countUser.count2;
            return View(model);
        }

        //PersonnelLogin/ApprovePension
        [HttpPost]
        [Route("PersonnelLogin/PensionApproved")]
        public async Task<IActionResult> PensionApproved([FromQuery] int PersonID)
        {
            string createdBy = TempData["username"] as string;
            ApprovePensionViewModel model = new();
            model.personID = PersonID;
            model.status = ApplicationConstant.Approved;
            model.createdBy = createdBy;
            var pp = await service.ApprovePension(model);

            return Ok(pp);
        }

        public IActionResult PensionEnquery(string svcNo)
        {
            if (!string.IsNullOrWhiteSpace(svcNo))
            {
                PensionViewModel searChPensionBySvc= service.GetPensionBySvcNo(svcNo);
                return View(searChPensionBySvc);

            }
            else
            {
                return View();
            }
            
        }

        public IActionResult UploadPensionRecord()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPensionRecord(IFormFile formfile, CancellationToken cancellationToken)
        {
            string createdBy = TempData["username"] as string;
            StringBuilder errorOutput = new StringBuilder();
            var unSuccessfulRecord = new List<PensionViewDto>();
            int rowCount = 1;
            int z = 0;
            try
            {
                if (formfile == null || formfile.Length <= 0)
                {
                    errorOutput.Append("No file for Upload");
                }
                else if (!Path.GetExtension(formfile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    errorOutput.Append("File not an Excel Format");
                }
                else
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    var stream = new MemoryStream();
                    await formfile.CopyToAsync(stream);

                    var reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    DataSet ds = new DataSet();
                    ds = reader.AsDataSet();
                    reader.Close();

                    rowCount = ds.Tables[0].Rows.Count;
                    DataTable serviceDetails = ds.Tables[0];

                    int k = 0;
                    if (ds != null && ds.Tables.Count > 0)
                    {

                        string svcNo = serviceDetails.Rows[0][0].ToString();
                        string firstname = serviceDetails.Rows[0][1].ToString();
                        string lastname = serviceDetails.Rows[0][2].ToString();
                        string middlename = serviceDetails.Rows[0][3].ToString();
                        string title = serviceDetails.Rows[0][4].ToString();



                        if (svcNo != "ServiceNo" || firstname != "FirstName" || lastname != "LastName" || middlename != "MiddleName" || title != "Title")
                        {
                            errorOutput.Append("File not in the Right format");
                        }
                        else
                        {
                            for (int row = 1; row < serviceDetails.Rows.Count; row++)
                            {
                                string SVCNO = string.IsNullOrEmpty(serviceDetails.Rows[row][0].ToString()) ? "" : serviceDetails.Rows[row][0].ToString().Trim();
                                string FIRSTNAME = string.IsNullOrEmpty(serviceDetails.Rows[row][1].ToString()) ? "" : serviceDetails.Rows[row][1].ToString().Trim();
                                string LASTNAME = string.IsNullOrEmpty(serviceDetails.Rows[row][2].ToString()) ? "" : serviceDetails.Rows[row][2].ToString().Trim();
                                string MIDDLENAME = string.IsNullOrEmpty(serviceDetails.Rows[row][3].ToString()) ? "" : serviceDetails.Rows[row][3].ToString().Trim();
                                string TITLE = string.IsNullOrEmpty(serviceDetails.Rows[row][4].ToString()) ? "" : serviceDetails.Rows[row][4].ToString().Trim();

                                var pp = new PensionViewModel()
                                {
                                    Title = TITLE,
                                    SVC_NO = SVCNO,
                                    FirstName = FIRSTNAME,
                                    LastName = LASTNAME,
                                    MiddleName = MIDDLENAME,
                                    createdBy=createdBy
                                };
                               
                                var response = await service.AddPension(pp);
                                if (response.responseCode != "00")
                                {
                                    unSuccessfulRecord.Add(new PensionViewDto()
                                    {
                                        Title = pp.Title,
                                        FirstName = pp.FirstName,
                                        LastName = pp.LastName,
                                        MiddleName = pp.MiddleName,
                                        ServiceNo = pp.SVC_NO,
                                        Message = response.responseMessage
                                    });
                                }
                                else
                                {
                                    z++;
                                }


                            }

                        }

                    }
                }

                if (unSuccessfulRecord.Count > 0)
                {

                    var stream2 = new MemoryStream();

                    using (var package2 = new ExcelPackage(stream2))
                    {
                        var workSheet = package2.Workbook.Worksheets.Add("Sheet1");
                        workSheet.Cells.LoadFromCollection(unSuccessfulRecord, true);
                        package2.Save();
                    }
                    stream2.Position = 0;
                    string excelName = $"FormB-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";


                    return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
                if (z == rowCount - 1 && string.IsNullOrWhiteSpace(errorOutput.ToString()))
                {
                    errorOutput.Append("All record successfully inserted");
                }

                TempData["messageExcel"] = errorOutput.ToString();
                return View();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception ==>{ex.Message}");
                return null;
            }
        }

        public async Task<IActionResult> PensionReview(int? pageNumber)
        {
            PensionModel model = new PensionModel();
            var pensionForReview = service.GetPensionByStatus(ApplicationConstant.Update);
            //var pp = pensionForReview.ToList();
            int pageSize = 3;
            model.Pensions = await PaginatedList<PensionViewModel>.CreateAsync(pensionForReview.AsQueryable(), pageNumber ?? 1, pageSize);

            return View(model);
        }
       
        public List<SelectListItem> GetRank()
        {
            var ranksList = (from rk in _context.ef_ranks
                             select new SelectListItem()
                             {
                                 Text = rk.rankName,
                                 Value = rk.rankName.ToString(),
                             }).ToList();

            ranksList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return ranksList;
        }
        public List<SelectListItem> GetShip()
        {
            var shipList = (from rk in _context.ef_ships
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
       
        public async Task<IActionResult> Initiation(int? pageNumber, string searchString)
        {
            DeathModel model = new();
            DeathViewModel model2 = new();
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            var initList = filterDeathByRole(deathService.GetDeathInit(),role);
            model2.shipDetails = personService.GetShip();
            if (!String.IsNullOrEmpty(searchString))
            {
                initList = initList.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            model.death = model2;

            int pageSize = 3;
            TempData["role"] = role;
            
            model.deaths = await PaginatedList<DeathViewModel>.CreateAsync(initList.AsQueryable(), pageNumber ?? 1, pageSize);
            model.death.rankDetails = GetRank();
            return View(model);
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateInitiation(DeathModel model)
        {
            DeathViewModel model2 = new DeathViewModel();
            model2 = model.death;
            string createdBy = TempData["username"] as string;
            model2.createdby = createdBy;
            model2.status = ApplicationConstant.Initiation;
            model2.shipDetails = personService.GetShip();
            // model.serviceDetails = TempData["serviceDetails"] as List<SelectListItem>;
            var result = await deathService.UpdateDeath(model2);
           
            ModelState.Clear();
            return RedirectToAction("Initiation","PersonnelLogin");
        }

        public async Task<IActionResult> DeathReviews(int? pageNumber,string searchString)
        {
            DeathModel model = new();
            DeathViewModel model2 = new DeathViewModel();
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            var deathForReview = filterDeathByRole(deathService.GetDeathByStatus(ApplicationConstant.Initiation),role);
            var pp = deathForReview.ToList();
            model2.shipDetails= personService.GetShip();
            model.death = model2;

            if (!String.IsNullOrEmpty(searchString))
            {
                deathForReview = deathForReview.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            TempData["role"] = role;
            int pageSize = 3;
            model.deaths  = await PaginatedList<DeathViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize);
            return View(model);
        }

        public async Task<IActionResult> DeathUpdateReview(int? pageNumber,string searchString)
        {
            DeathModel model = new();
            DeathViewModel model2 = new();
            model2.shipDetails = personService.GetShip();
            var deathUpdateForReview = deathService.GetDeathByStatus(ApplicationConstant.Update);
            var pp = deathUpdateForReview.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                deathUpdateForReview = deathUpdateForReview.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            model.death = model2;
            int pageSize = 3;   
            model.deaths = await PaginatedList<DeathViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize); ;
            return View(model);

        }
        public async Task<IActionResult> DeathUpdate(int? pageNumber, string searchString)
        {
            DeathModel model = new DeathModel();
            DeathViewModel viewModel = new DeathViewModel();
            viewModel.shipDetails = personService.GetShip();
            var deathByInitation = deathService.GetDeathByStatus(ApplicationConstant.Initiation);

            if (!String.IsNullOrEmpty(searchString))
            {
                deathByInitation = deathByInitation.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }
            var pp = deathByInitation.ToList();
            model.death = viewModel;
            int pageSize = 3;
            model.deaths = await PaginatedList<DeathViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeathUpdate(DeathModel model)
        {

            string createdBy = TempData["username"] as string;
            model.death.createdby = createdBy;
            model.death.status = ApplicationConstant.Update;

            var result = await deathService.UpdateDeathForCPO(model.death);
            return Ok(result);
        }


        public async Task<IActionResult> ApproveDeath(int ? pageNumber, string searchString)
        {
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            var deathGratuityForApproval = filterDeathByRole(deathService.GetDeathByStatus(ApplicationConstant.Update), role);
            int pageSize = 3;
            if (!String.IsNullOrEmpty(searchString))
            {
                deathGratuityForApproval = deathGratuityForApproval.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }
            TempData["role"] = role;

            return View(await PaginatedList<DeathViewModel>.CreateAsync(deathGratuityForApproval.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> DeathPayment(int? pageNumber, string searchString)
        {
            var pensionForApproval = deathService.GetDeathByStatus(ApplicationConstant.Approved);
            if (!String.IsNullOrEmpty(searchString))
            {
                pensionForApproval = pensionForApproval.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }
            int pageSize = 3;
            return View(await PaginatedList<DeathViewModel>.CreateAsync(pensionForApproval.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        [Route("PersonnelLogin/CreateDeathPayment")]
        public async Task<IActionResult> CreateDeathPayment([FromQuery] int PersonID)
        {
            string createdBy = TempData["username"] as string;
            ApproveDeathViewModel model = new();
            model.personID = PersonID;
            model.status = ApplicationConstant.Payment;
            model.createdBy = createdBy;
            var pp = await deathService.ApproveDeathPayment(model);

            return Ok(pp);
        }



        //PersonnelLogin/ApprovePension
        [HttpPost]
        [Route("PersonnelLogin/DeathApproved")]
        public async Task<IActionResult> DeathApproved([FromQuery] int PersonID)
        {
            string createdBy = TempData["username"] as string;
            ApproveDeathViewModel model = new();
            model.personID = PersonID;
            model.status = ApplicationConstant.Approved;
            model.createdBy = createdBy;
            var pp = await deathService.ApproveDeath(model);

            return Ok(pp);
        }

        public IActionResult DeathEnquery(string svcNo)
        {
            if (!string.IsNullOrWhiteSpace(svcNo))
            {
                DeathViewModel searChDeathBySvc = deathService.GetDeathBySvcNo(svcNo);
                return View(searChDeathBySvc);

            }
            else
            {
                return View();
            }

        }

        public IActionResult UploadDeathRecord()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadDeathRecord(IFormFile formfile, CancellationToken cancellationToken)
        {
            string createdBy = TempData["username"] as string;
            StringBuilder errorOutput = new StringBuilder();
            var unSuccessfulRecord = new List<PensionViewDto>();
            int rowCount = 1;
            int z = 0;
            try
            {
                if (formfile == null || formfile.Length <= 0)
                {
                    errorOutput.Append("No file for Upload");
                }
                else if (!Path.GetExtension(formfile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    errorOutput.Append("File not an Excel Format");
                }
                else
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    var stream = new MemoryStream();
                    await formfile.CopyToAsync(stream);

                    var reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    DataSet ds = new DataSet();
                    ds = reader.AsDataSet();
                    reader.Close();

                    rowCount = ds.Tables[0].Rows.Count;
                    DataTable serviceDetails = ds.Tables[0];

                    int k = 0;
                    if (ds != null && ds.Tables.Count > 0)
                    {

                        string svcNo = serviceDetails.Rows[0][0].ToString();
                        string firstname = serviceDetails.Rows[0][1].ToString();
                        string lastname = serviceDetails.Rows[0][2].ToString();
                        string middlename = serviceDetails.Rows[0][3].ToString();
                        string title = serviceDetails.Rows[0][4].ToString();



                        if (svcNo != "ServiceNo" || firstname != "FirstName" || lastname != "LastName" || middlename != "MiddleName" || title != "Title")
                        {
                            errorOutput.Append("File not in the Right format");
                        }
                        else
                        {
                            for (int row = 1; row < serviceDetails.Rows.Count; row++)
                            {
                                string SVCNO = string.IsNullOrEmpty(serviceDetails.Rows[row][0].ToString()) ? "" : serviceDetails.Rows[row][0].ToString().Trim();
                                string FIRSTNAME = string.IsNullOrEmpty(serviceDetails.Rows[row][1].ToString()) ? "" : serviceDetails.Rows[row][1].ToString().Trim();
                                string LASTNAME = string.IsNullOrEmpty(serviceDetails.Rows[row][2].ToString()) ? "" : serviceDetails.Rows[row][2].ToString().Trim();
                                string MIDDLENAME = string.IsNullOrEmpty(serviceDetails.Rows[row][3].ToString()) ? "" : serviceDetails.Rows[row][3].ToString().Trim();
                                string TITLE = string.IsNullOrEmpty(serviceDetails.Rows[row][4].ToString()) ? "" : serviceDetails.Rows[row][4].ToString().Trim();

                                var pp = new DeathViewModel()
                                {
                                    Title = TITLE,
                                    SVC_NO = SVCNO,
                                    FirstName = FIRSTNAME,
                                    LastName = LASTNAME,
                                    MiddleName = MIDDLENAME,
                                    createdby = createdBy
                                };

                                var response = await deathService.AddDeath(pp);
                                if (response.responseCode != "00")
                                {
                                    unSuccessfulRecord.Add(new PensionViewDto()
                                    {
                                        Title = pp.Title,
                                        FirstName = pp.FirstName,
                                        LastName = pp.LastName,
                                        MiddleName = pp.MiddleName,
                                        ServiceNo = pp.SVC_NO,
                                        Message = response.responseMessage
                                    });
                                }
                                else
                                {
                                    z++;
                                }


                            }

                        }

                    }
                }

                if (unSuccessfulRecord.Count > 0)
                {

                    var stream2 = new MemoryStream();

                    using (var package2 = new ExcelPackage(stream2))
                    {
                        var workSheet = package2.Workbook.Worksheets.Add("Sheet1");
                        workSheet.Cells.LoadFromCollection(unSuccessfulRecord, true);
                        package2.Save();
                    }
                    stream2.Position = 0;
                    string excelName = $"FormC-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";


                    return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
                if (z == rowCount - 1 && string.IsNullOrWhiteSpace(errorOutput.ToString()))
                {
                    errorOutput.Append("All record successfully inserted");
                }

                TempData["messageDExcel"] = errorOutput.ToString();
                return View();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception ==>{ex.Message}");
                return null;
            }
        }

        public async Task<IActionResult> DeathReview(int ? pageNumber)
        {
            DeathModel model = new();
            DeathViewModel model2 = new DeathViewModel();
            var deathForReview = deathService.GetDeathByStatus(ApplicationConstant.Update);
            var pp = deathForReview.ToList();
            model2.shipDetails = personService.GetShip();

            int pageSize = 3;
            model.deaths = await PaginatedList<DeathViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize); ;

            return View(model);
        }

        public IActionResult DeathGratuity()
        {
            
            return View();

        }

        public async Task<FileResult> DownloadBForm()
        {
            string fileName = "ny_nn9bRecord.xlsx";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(bytes, "application/octet", fileName);

        }

        public async Task<FileResult> DownloadCForm()
        {
            string fileName = "ny_nn9cRecord.xlsx";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(bytes, "application/octet", fileName);

        }
        public IActionResult PersonnelEmolument()
        {
            try
            {
                    var userid = User.Identity.Name;
                   
                    var pers = _context.ef_personalInfos.Where(x => x.serviceNumber == userid).SingleOrDefault();
                if (pers != null) { 
                    string pyear = DateTime.Now.Year.ToString();
                    var globalcon = _context.ef_control.Where(x => x.enddate.Date >= DateTime.Now.Date && x.processingyear == pyear && x.ship == "All" && x.status == "Open").FirstOrDefault();
                    var shipcon = _context.ef_control.Where(x => x.enddate.Date >= DateTime.Now.Date && x.ship == pers.ship && x.processingyear == pyear && x.status == "Open").FirstOrDefault();
                    if (shipcon == null && globalcon == null)
                    {
                        return RedirectToAction("ClosingPage", "Account");
                    }
                    else
                    {

                        if (pers.payrollclass == "1")
                        {
                            HttpContext.Session.SetString("personnel", pers.serviceNumber);
                            return RedirectToAction("OfficerRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "OfficerRecord"
                                //  id = pers.password
                            }));
                        }
                        else if (pers.payrollclass == "2")
                        {
                            HttpContext.Session.SetString("personnel", pers.serviceNumber);
                            return RedirectToAction("RatingRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "RatingRecord"
                                // id = pers.password
                            }));
                        }
                        else if (pers.payrollclass == "3")
                        {
                            HttpContext.Session.SetString("personnel", pers.serviceNumber);
                            return RedirectToAction("RatingRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "RatingRecord"
                                // id = pers.password
                            }));
                        }
                        else if (pers.payrollclass == "4")
                        {
                            HttpContext.Session.SetString("personnel", pers.serviceNumber);
                            return RedirectToAction("RatingRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "RatingRecord"
                                //id = pers.password
                            }));
                        }
                        else if (pers.payrollclass == "5")
                        {
                            HttpContext.Session.SetString("personnel", pers.serviceNumber);
                            return RedirectToAction("RatingRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "RatingRecord",
                                // id = pers.password
                            }));
                        }
                        else if (pers.payrollclass == "6")
                        {
                            HttpContext.Session.SetString("personnel", pers.serviceNumber);
                            return RedirectToAction("TrainingRecord", new RouteValueDictionary(
                            new
                            {
                                controller = "PersonalInfo",
                                action = "TrainingRecord",
                                // id = pers.password
                            }));
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
            }
            return View();
        }
        [HttpPost]
        //public IActionResult Login2(personLoginVM value)
        //{
        //    try
        //    {
        //        MD5 md = MD5.Create();
        //        if (ModelState.IsValid)
        //        {
        //            var user = authenticationService.FindUser(value.userName).Result;
        //            if (user == null)
        //            {
        //                TempData["ErrorMessage"] = "User Not Found";
        //                return RedirectToAction("login", "Authentication");
        //            }
        //            var pers = _context.ef_PersonnelLogins.Where(x => x.userName == value.userName).SingleOrDefault();
        //            string pyear = DateTime.Now.Year.ToString();
        //            // var site = _context.ef_systeminfos.FirstOrDefault(x => x.closedate.Date < DateTime.Now.Date);
        //            var globalcon = _context.ef_control.Where(x => x.enddate.Date >= DateTime.Now.Date && x.processingyear == pyear && x.ship == "All" && x.status == "Open").FirstOrDefault();
        //            var shipcon = _context.ef_control.Where(x => x.enddate.Date >= DateTime.Now.Date && x.ship == pers.ship && x.processingyear == pyear && x.status == "Open").FirstOrDefault();
        //            if (shipcon == null && globalcon == null)
        //            {
        //                return RedirectToAction("ClosingPage", "Account");
        //            }
        //            //else if (globalcon == null && shipcon != null)
        //            //{
        //            //    return RedirectToAction("ClosingPage", "Account");
        //            //}

        //            // var pers = personService.GetPersonBySvc_NO(value.userName);

        //            var checkdoublemail = _context.ef_PersonnelLogins.Where(x => x.email == pers.email).Count();
        //            if (pers == null)
        //            {
        //                TempData["errorMessage"] = "Invalid credentials";
        //                return RedirectToAction("Login", "PersonnelLogin");
        //            }
        //            else if (pers.expireDate == null && pers.userName == value.userName && pers.password == value.password)
        //            {
        //                HttpContext.Session.SetString("SVC_No", value.userName);
        //                return RedirectToAction("ResetPasswordServiceNumber");
        //            }
        //            else if (VerifyMd5Hash(md, value.password, pers.password))
        //            {

        //                if (pers.payClass == "1")
        //                {
        //                    HttpContext.Session.SetString("personnel", pers.userName);
        //                    return RedirectToAction("OfficerRecord", new RouteValueDictionary(
        //                    new
        //                    {
        //                        controller = "PersonalInfo",
        //                        action = "OfficerRecord",
        //                        id = pers.password
        //                    }));
        //                }
        //                else if (pers.payClass == "2")
        //                {
        //                    HttpContext.Session.SetString("personnel", pers.userName);
        //                    return RedirectToAction("RatingRecord", new RouteValueDictionary(
        //                    new
        //                    {
        //                        controller = "PersonalInfo",
        //                        action = "RatingRecord",
        //                        id = pers.password
        //                    }));
        //                }
        //                else if (pers.payClass == "3")
        //                {
        //                    HttpContext.Session.SetString("personnel", pers.userName);
        //                    return RedirectToAction("RatingRecord", new RouteValueDictionary(
        //                    new
        //                    {
        //                        controller = "PersonalInfo",
        //                        action = "RatingRecord",
        //                        id = pers.password
        //                    }));
        //                }
        //                else if (pers.payClass == "4")
        //                {
        //                    HttpContext.Session.SetString("personnel", pers.userName);
        //                    return RedirectToAction("RatingRecord", new RouteValueDictionary(
        //                    new
        //                    {
        //                        controller = "PersonalInfo",
        //                        action = "RatingRecord",
        //                        id = pers.password
        //                    }));
        //                }
        //                else if (pers.payClass == "5")
        //                {
        //                    HttpContext.Session.SetString("personnel", pers.userName);
        //                    return RedirectToAction("RatingRecord", new RouteValueDictionary(
        //                    new
        //                    {
        //                        controller = "PersonalInfo",
        //                        action = "RatingRecord",
        //                        id = pers.password
        //                    }));
        //                }
        //                else if (pers.payClass == "6")
        //                {
        //                    HttpContext.Session.SetString("personnel", pers.userName);
        //                    return RedirectToAction("TrainingRecord", new RouteValueDictionary(
        //                    new
        //                    {
        //                        controller = "PersonalInfo",
        //                        action = "TrainingRecord",
        //                        id = pers.password
        //                    }));
        //                }

        //            }
        //            else
        //            {

        //                TempData["errorMessage"] = "Invalid UserName/Password";
        //                return View();
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogInformation(ex.Message);
        //    }
        //    return View();
        //}
        public bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
        [HttpPost]
        public IActionResult SendApplicantMessage(string Email)
        {
            try
            {
                Guid k = Guid.NewGuid();
              
                string confirmationUrl = Url.Action("ConfirmEmail", "Authentication", new { email = Email, token = k.ToString() },
                    protocol: HttpContext.Request.Scheme);
                string confirm = "Please confirm your account by clicking this link: <a href=\""
                                         + confirmationUrl + "\">link</a>";
                _EmailService.Send(Email, "Applicant confirmation message", confirm, true);

                return Ok(new { responseCode = 200, responseDescription = "successful" });
            }
            catch
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }


        [NonAction]
        public void SendMessage(string emailTo)
        {
            Guid k = Guid.NewGuid();
            
            string confirmationUrl = Url.Action("ConfirmEmail", "Authentication", new { email = emailTo, token = k.ToString() },
                protocol: HttpContext.Request.Scheme);
            string confirm = "Please confirm your account by clicking this link: <a href=\""
                                     + confirmationUrl + "\">link</a>";
            _EmailService.Send(emailTo, "Applicant confirmation message", confirm, true);
        }

        public ActionResult VerifyCode()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyCode([Required(ErrorMessage = "This Field is Required")] string Code)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string SessionCode = HttpContext.Session.GetString("verificationCode").ToString();
                    if (SessionCode != null)
                    {
                        if (SessionCode != Code)
                        {
                            ModelState.AddModelError("", "Invalid Verification Code");
                        }
                        else
                        {
                            return RedirectToAction("ResetPassword");
                        }
                    }
                }
                catch (Exception ex)
                {
                    string log = ex.Message;
                }

            }
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.NewPassword != model.ConfirmPassword)
                    {
                        TempData["messageUpd"] = "the password does not match";
                        return RedirectToAction("ResetPassword", "PersonnelLogin");
                    }
                    else
                    {
                        MD5 md5Hash = MD5.Create();
                        var userToReset = HttpContext.Session.GetString("userToReset");   // retrieve the user from session
                        if (!string.IsNullOrEmpty(userToReset))
                        {
                            var userpersonnel = userService.GetUsers().Where(x => x.UserName == userToReset && x.personnel == true).FirstOrDefault();
                            if (userpersonnel != null)
                            {
                                var user = authenticationService.FindUserById(userpersonnel.Id.ToString()).Result;

                                if (user != null)
                                {
                                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                                    var resetpass = await userManager.ResetPasswordAsync(user, token, model.ConfirmPassword);
                                    if (resetpass.Succeeded)
                                    {
                                        authenticationService.updateUserlogins(user);
                                        HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");
                                        return RedirectToAction("Login2");
                                    }

                                }
                                HttpContext.Session.Clear();

                                return RedirectToAction("Login");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string log = ex.Message;
                }
            }
            ModelState.AddModelError("", "Unable to Reset Password. Please Contact your Admin");
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ResetPasswordServiceNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult ResetPasswordServiceNumber(ComfirmEmail conmail)
        {

            try
            {
                if (conmail.Email != conmail.ConfirmEmail)
                {
                    TempData["verifymessage"] = "Email Not Match";
                }
                if (conmail.ConfirmEmail != null)
                {
                    string username=HttpContext.Session.GetString("SVC_No");
                    //var user = personInfoService.GetPersonBySVC_No(conmail.UserName);
                    var user2 = _context.Users.Where(x => x.UserName == username).FirstOrDefault();
                    if (user2!=null)
                    {
                        resetcode = rnd.Next(100000, 200000);   // generate random number and send to user mail and phone
                        string message2 = "Your Verification Code is:" + resetcode;
                        HttpContext.Session.SetString("userToReset", user2.UserName);
                        HttpContext.Session.SetString("verificationCode", resetcode.ToString());

                        var mail = new MailClass();
                        mail.bodyText= "Your Verification Code is:" + resetcode;
                        mail.fromName= "NN-CPO";
                        mail.to = conmail.ConfirmEmail;
                        mail.subject = "NNCPO E-Emolument Form Verification";
                        mail.code = resetcode.ToString();

                        if (conmail.messageType == "sms")
                        {
                            var sms = new smxmodel()
                            {
                                phoneNumber = user2.PhoneNumber,
                                message = message2,
                                code = resetcode.ToString()
                            };

                            _mailService.sendsmsTermii(sms);
                        }
                        else
                        {
                            _mailService.SendEmailTermii(mail);

                        }
                        //_mailService.sendsms(sms);

                        // _mailService.sendemail(mail);
                        // _mailService.SendEmail(mail);
                        TempData["verifymessage"]= "Notification Was sent to your Email/Phone address.";
                        return RedirectToAction("VerifyCode");
                      
                    }
                    else
                    {
                        TempData["verifymessage"]= "Invalid Service Number. Please Contact your Admin";
                    }

                }
            }
            catch (Exception ex)
           {
                _logger.LogInformation(ex.Message);
                TempData["errorMessage"] = "Unable To Send Mail";
                return RedirectToAction("Login");
            }
            return View();
        }
        public void SendEmailNotification(string emailFrom, string sender, string messageToSend, string receipient)
        {

            try
            {

            var UserName = config.GetValue<string>("mailconfig:SenderEmail");
            var Password = config.GetValue<string>("mailconfig:Password");
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(receipient));
            message.From = new MailAddress(sender, emailFrom);
            message.Subject = "Password Verification Code";
            message.Body = string.Format(body, emailFrom, sender, messageToSend);
            message.IsBodyHtml = true;

   

            string host = config.GetValue<string>("mailconfig:Server");
            int port = config.GetValue<int>("mailconfig:Port");
            var enableSSL =Convert.ToBoolean("True");// config.GetValue<bool>("true");

            //using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            //{
            //    smtp.Credentials = new NetworkCredential("cpoemolumentform@gmail.com", "HicadServer@cpo");
            //    smtp.EnableSsl = true;
            //    smtp.Send(message);
            //}
            SmtpClient SmtpServer = new SmtpClient(host);

            var smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = enableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(UserName, Password)
            };
            smtp.Port = port; // Also Add the port number to send it, its default for Gmail
            smtp.Credentials = new NetworkCredential(UserName, Password);
            smtp.EnableSsl = enableSSL;
            smtp.Timeout = 200000; // Add Timeout property
            smtp.Send(message);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GetRole(List<UserWIthRoleViewModel> pp)
        {
            StringBuilder builder = new StringBuilder();
            foreach(var j in pp)
            {
                builder.Append(j.RoleName+",");
            }

            return builder.ToString();
        }
        public void SendEmail(string to, string subject, string bodyText, string bodyHtml, string from, string fromName)
        {
            try
            {
                var apikey = config.GetValue<string>("mailconfig:Apikey");
            var UserName = config.GetValue<string>("mailconfig:hostmails");
            WebClient client = new WebClient();
            NameValueCollection values = new NameValueCollection();
            values.Add("username", UserName);
            values.Add("api_key", apikey);
            values.Add("from", from);
            values.Add("from_name", fromName);
            values.Add("subject", subject);
            if (bodyHtml != null)
                values.Add("body_html", bodyText);
            if (bodyText != null)
                values.Add("body_text", bodyText);
            values.Add("to", to);

            byte[] response = client.UploadValues("https://api.elasticemail.com/mailer/send", values);
            //return Encoding.UTF8.GetString(response);
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message, "Error");
            }
        }
        static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        public IEnumerable<PensionViewModel> filterPensionByRole(IEnumerable<PensionViewModel> pensionList,string role)
        {
            var filterByRole = new List<PensionViewModel>();
            if (role.ToLower() == "navsec")
            {
                var unFilteredRecord = pensionList.Where(x => x.SVC_NO.Substring(0, 2).ToLower() == "nn");
                filterByRole = unFilteredRecord.ToList();
            }

            if(role.ToLower() == "cnd")
            {
                var unFilteredRecord2 = pensionList.Where(x => x.SVC_NO.Substring(0, 2).ToLower() != "nn");
                filterByRole = unFilteredRecord2.ToList();
            }

            return filterByRole;
        }

        public IEnumerable<DeathViewModel> filterDeathByRole(IEnumerable<DeathViewModel> deathList, string role)
        {
            var filterByRole = new List<DeathViewModel>();
            if (role.ToLower() == "navsec")
            {
                var unFilteredRecord = deathList.Where(x => x.SVC_NO.Substring(0, 2).ToLower() == "nn");
                filterByRole = unFilteredRecord.ToList();
            }

            if (role.ToLower() == "cnd")
            {
                var unFilteredRecord2 = deathList.Where(x => x.SVC_NO.Substring(0, 2).ToLower() != "nn");
                filterByRole = unFilteredRecord2.ToList();
            }

            return filterByRole;
        }
    }
}
