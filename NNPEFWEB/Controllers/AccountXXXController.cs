﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.Service;
using NNPEFWEB.Services;
using NNPEFWEB.ViewModel;
using OfficeOpenXml;

namespace NNPEFWEB.Controllers
{
    public class AccountXXXController : Controller
    {
        private readonly IAuthService _authenticationService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ISystemsInfoService _systemsInfoService;
        private readonly string connectionstring;
        private readonly string _remoteConnectionstring;
        private readonly string _localConnectionstring;
        private IPersonInfoService _personinfoService;
        private readonly IPersonService _personService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountXXXController> _logger;
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IConfiguration config;
        private readonly IShipService _shipService;
        private readonly IMailService _mailService;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
       

        private int resetcode;
        Random rnd = new Random();

        public AccountXXXController(
            IShipService shipService,
            IConfiguration config,
            IPersonInfoService personinfoService,
            IPersonService personService,
            ILogger<AccountXXXController> logger,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ISystemsInfoService systemsInfoService,
            IMailService mailService,
            ApplicationDbContext context, IUnitOfWorks unitOfWorks, IConfiguration configuration,
            IUserService userService,
            IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
            this.userService = userService;
            _shipService = shipService;
            this.config = config;
            _personinfoService = personinfoService;
            _personService = personService;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _systemsInfoService = systemsInfoService;
            _context = context;
            _unitOfWorks = unitOfWorks;
            _mailService = mailService;
            connectionstring = configuration.GetConnectionString("DefaultConnection");
            _remoteConnectionstring = configuration.GetConnectionString("RemoteConnection");
            _localConnectionstring = configuration.GetConnectionString("LocalConnection");


        }

        [HttpGet]
        public ActionResult GetSystemsInfo()
        {
            try
            {
                var systeminfo = _context.ef_systeminfos.ToList();

                return View(systeminfo);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }

        [HttpGet]
        public ActionResult GetSystemsInfoById(int id)
        {
            try { 
            var systeminfo = _systemsInfoService.GetSystemsInfo(id);

            return View(systeminfo);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }

        [HttpGet]
        public ActionResult CreateSystemsInfo()
        {
            try { 
            systeminfoVM systeminfoVM = new systeminfoVM();

            return View(systeminfoVM);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }

        [HttpPost]
        public ActionResult CreateSystemsInfo(systeminfoVM ef_Systeminfo)
        {
            try
            {
                if (ef_Systeminfo.company_image != null)
                {
                    IFormFile file = Request.Form.Files.Where(x => x.Name == "company_image").FirstOrDefault();

                    using (var dataStream = new MemoryStream())
                    {
                        file.CopyToAsync(dataStream);

                        ef_Systeminfo.company_image = dataStream.ToArray();

                    }
                }

                _systemsInfoService.CreateSystemsInfo(ef_Systeminfo);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }
            return RedirectToAction("GetSystemsInfo");

        }

        [HttpGet]
        public ActionResult UpdateSystemsInfo(int id)
        {
            try { 
            var systemsInfo = _systemsInfoService.GetSystemsInfo(id);

            return View(systemsInfo);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }

        [HttpPost]
        public ActionResult UpdateSystemsInfo(ef_systeminfo ef_Systeminfo)
        {
            try { 
            if (ef_Systeminfo.company_image != null)
            {
                IFormFile file = Request.Form.Files.Where(x => x.Name == "company_image").FirstOrDefault();

                using (var dataStream = new MemoryStream())
                {
                    file.CopyToAsync(dataStream);

                    ef_Systeminfo.company_image = dataStream.ToArray();

                }
            }

            _systemsInfoService.UpdateSystemsInfo(ef_Systeminfo);

            return RedirectToAction("GetSystemsInfo");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }

        [HttpGet]
        public ActionResult DeleteSystemsInfo(int id)
        {
            try { 
            var systemsInfo = _systemsInfoService.GetSystemsInfo(id);

            return View(systemsInfo);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }

        [HttpPost]
        public ActionResult DeleteSystemsInfo(int id, ef_systeminfo ef_Systeminfo)
        {
            try { 
            var systemsInfoInDB = _systemsInfoService.GetSystemsInfo(id);

            if (systemsInfoInDB == null)
            {
                return View("Error");
            }

            _systemsInfoService.DeleteSystemsInfo(systemsInfoInDB.Id);

            return RedirectToAction("GetSystemsInfo");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }

        public ActionResult SiteActivities()
        {
            try { 
           var dd= _context.ef_systeminfos.FirstOrDefault();
            HttpContext.Session.SetInt32("Site", dd.SiteStatus);
            GetShip();
            var reg = new systeminfoVM
            {
                shipList = GetShips()
            };
            return View(reg);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }
        [HttpPost]
        public ActionResult SiteActivities(systeminfoVM  status)
        {
            try
            {
            string user = User.Identity.Name;
           
            using(SqlConnection sqlcon=new SqlConnection(connectionstring))
            {
                using(SqlCommand cmd=new SqlCommand("OpenCloseForm", sqlcon))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@opendate", status.opendate));
                    cmd.Parameters.Add(new SqlParameter("@closedate", status.closedate));
                    cmd.Parameters.Add(new SqlParameter("@status", status.SiteStatus));
                    cmd.Parameters.Add(new SqlParameter("@ship", status.ship));
                    cmd.Parameters.Add(new SqlParameter("@user", user));
              
                    sqlcon.Open();
                    cmd.ExecuteNonQuery();
                        TempData["SiteMessage"] = "Successful";
                }
               
            }

            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
            }
            return View();
        }
        public ActionResult CommandLogin()
        {
            try { 
            var command = new SelectList(_context.ef_commands.ToList(), "Id", "commandName");
            var ship = new SelectList(_context.ef_ships.ToList(), "Id", "shipName");


            var com = new CommandLoginVM
            {
                commandList = command,
                shiplist = ship,
            };
            return View(com);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }
        [HttpPost]
        public ActionResult CommandLogin(CommandLoginVM com)
        {
            try
            {
                MD5 md = MD5.Create();
                if (ModelState.IsValid)
                {
                    var site = _context.ef_systeminfos.FirstOrDefault(x => x.closedate.Date < DateTime.Now.Date);
                    if (site != null)
                    {
                        return RedirectToAction("ClosingPage", "Account");
                    }

                    var pers = _personService.GetPersonBySvc_NO(com.UserName);
                    var per = _personinfoService.GetPersonalInfo(com.UserName).Result;
                    if (pers.svcNo.Substring(0,2) != "NN")
                    {
                        TempData["commandLoginMessage"] = "Not Eligible Contact Admin";
                        return RedirectToAction("CommandLogin", "Account");
                    }
                    var checkdoublemail = _context.ef_PersonnelLogins.Where(x => x.email == pers.email).Count();
                    if (pers == null)
                    {
                        TempData["commandLoginMessage"] = "Invalid credentials";
                        return RedirectToAction("CommandLogin", "Account");
                    }
                    else if (pers.expireDate == null && pers.userName == com.UserName && pers.password == com.Password)
                    {
                        if (pers.email == null)
                        {
                            TempData["commandLoginMessage"] = "Email Not Avialable Contact CPO";
                        }
                        else if (IsValidEmail(pers.email) != true)
                        {
                            TempData["commandLoginMessage"] = "Invalid Email Contact CPO";
                        }
                        else if (checkdoublemail > 1)
                        {
                            TempData["commandLoginMessage"] = "Duplicate Email Contact CPO";
                        }
                        else
                        {
                            HttpContext.Session.SetString("SVC_No", com.UserName);
                            return RedirectToAction("ResetPasswordServiceNumber", new RouteValueDictionary(
                               new
                               {
                                   controller = "Account",
                                   action = "ResetPasswordServiceNumber",
                                   email = pers.userName
                               }));
                        }

                    }
                    else if (VerifyMd5Hash(md, com.Password, pers.password))
                    {
                        //if (pers.Appointment == null || pers.Appointment == "")
                        //{
                        //    TempData["commandLoginMessage"] = "You Are Not Eligable To Login, Contact Admin";
                        //    return RedirectToAction("Login", "Account");
                        //}
                        if (pers.payClass == "1")
                        {
                            HttpContext.Session.SetString("CommandUser", com.UserName);
                            HttpContext.Session.SetString("Appointment", per.appointment);
                            HttpContext.Session.SetString("Authorization", "CommandLevel");
                            HttpContext.Session.SetInt32("ship", com.ship);

                            return RedirectToAction("commanddashbord", "Home");
                        }
                        else
                        {
                            TempData["commandLoginMessage"] = "You Are Not Eligable To Login";
                            return RedirectToAction("CommandLogin", "Account");
                        }


                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
            }
            return View();
       
        }
        public ActionResult ShipLogin()
        {
            var command = new SelectList(_context.ef_commands.ToList(), "Id", "commandName");
            var ship = new SelectList(_context.ef_ships.ToList(), "Id", "shipName");


            var com = new CommandLoginVM
            {
                commandList = command,
                shiplist = ship,
            };
            return View(com);
        }
        [HttpPost]
        public async Task<IActionResult> ShipLogin(CommandLoginVM com)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userlogin = userService.GetUsers().Where(x => x.UserName == com.UserName && x.shipcheck == true).FirstOrDefault();
                    var pers = _authenticationService.FindUserById(userlogin.Id.ToString()).Result;
                    if (pers == null)
                    {
                        TempData["commandLoginMessage"] = "UserName/Password not correct";
                        return RedirectToAction("ShipLogin", "Account");
                    }
                    string pyear = DateTime.Now.Year.ToString();
                    var globalcon = _context.ef_control.Where(x => x.enddate.Date >= DateTime.Now.Date && x.processingyear == pyear && x.ship == "All" && x.status == "Open").FirstOrDefault();
                    var shipcon = _context.ef_control.Where(x => x.enddate.Date >= DateTime.Now.Date && x.ship == pers.ship && x.processingyear == pyear && x.status == "Open").FirstOrDefault();
                    if (shipcon == null && globalcon == null)
                     {
                        return RedirectToAction("ClosingPage", "Account");
                    }

                    var per = _personinfoService.GetPersonalInfo(com.UserName).Result;
                    var shipid = _context.ef_ships.FirstOrDefault(x => x.shipName == pers.ship);
                    if (pers.UserName.Substring(0, 2) != "NN")
                    {
                        TempData["commandLoginMessage"] = "Not Eligible Contact Admin";
                        return RedirectToAction("ShipLogin", "Account");
                    }
                    if (pers == null)
                    {
                        TempData["commandLoginMessage"] = "Invalid credentials";
                        return RedirectToAction("ShipLogin", "Account");
                    }
                    if (pers.ResetPasswordCode == null)
                    {
                            //HttpContext.Session.SetString("SVC_No", com.UserName);
                            HttpContext.Session.SetString("userToReset", com.UserName);
                            return RedirectToAction("ResetShipPassword", "Account");

                    }
                    var auth = await _authenticationService.SignInUserAsync(com.UserName, com.Password, "false");

                    if (!auth.Success)
                    {
                        TempData["commandLoginMessage"] = "Invalid UserName or Password";
                        return RedirectToAction("ShipLogin", "Account");
                    }
                       HttpContext.Session.SetString("ShipUser", com.UserName);

                        HttpContext.Session.SetString("Authorization", "CommandLevel");
                        HttpContext.Session.SetInt32("ship", shipid.Id);

                        return RedirectToAction("commanddashbord", "Home");
                     
                    }
             
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
            }
            return View();

        }

        public bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgetPassword(ComfirmEmail conmail)
        {

            try
            {
                if (conmail.Email != conmail.ConfirmEmail)
                {
                    TempData["verifymessage"] = "Email Not Match";
                }
                if (conmail.ConfirmEmail != null)
                {
                    string username = HttpContext.Session.GetString("SVC_No");
                    var user = _shipService.GetPersonBySvcno(conmail.UserName);
                   
                    if (user != null)
                    {
                        resetcode = rnd.Next(100000, 200000);   // generate random number and send to user mail and phone
                        //string message = "Your Verification Code is:" + resetcode;
                        HttpContext.Session.SetString("shipuserToReset", user.userName);
                        HttpContext.Session.SetString("verificationCode", resetcode.ToString());

                        var mail = new MailClass();
                        mail.message = "Your Verification Code is:" + resetcode;
                        mail.fromName = "NN-CPO";
                        mail.to = conmail.ConfirmEmail;
                        mail.subject = "NNCPO E-Emolument Form User Verification";
                        mail.code = resetcode.ToString();

                        if (conmail.messageType == "sms")
                        {
                            var sms = new smxmodel()
                            {
                                phoneNumber = user.phoneNumber,
                                message = mail.message,
                                code = resetcode.ToString()
                            };

                            _mailService.sendsmsTermii(sms);
                        }
                        else
                        {
                            _mailService.SendEmailTermii(mail);

                        }
                        // _mailService.SendEmail(mail);

                        TempData["verifymessage"] = "A Notification Was Sent To Your Email/Phone.";
                        return RedirectToAction("VerifyCode");

                    }
                    else
                    {
                        TempData["verifymessage"] = "Invalid Service Number. Please Contact your Admin";
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                TempData["errorMessage"] = "Unable To Send Mail";
                return RedirectToAction("ShipLogin");
            }
            return RedirectToAction("ForgetPassword");
        }


        public IActionResult ResetPasswordServiceNumber(string email)
        {

            try
            {
                if (email != null)
                {
                    var user = _personService.GetPersonBySvc_NO(email);
                    if (user != null)
                    {
                        resetcode = rnd.Next(100000, 200000);   // generate random number and send to user mail and phone
                        //string message = "Your Verification Code is:" + resetcode;
                        HttpContext.Session.SetString("shipuserToReset", user.userName);
                        HttpContext.Session.SetString("verificationCode", resetcode.ToString());


                        var mail = new MailClass();
                        mail.bodyText = "Your Verification Code is:" + resetcode;
                        mail.fromName = "NN-CPO";
                        mail.to = user.email;
                        mail.subject = "NNCPO E-Emolument Form User Verification";
                        var sms = new smxmodel()
                        {
                            phoneNumber = user.phoneNumber,
                            message = mail.bodyText
                        };
                        _mailService.sendsms(sms);
                      //  _mailService.SendEmail(mail);

                        TempData["verifymessage"] = "An Email Notification Was sent to your Email address.";
                        return RedirectToAction("VerifyCode");

                    }
                    else
                    {
                        TempData["verifymessage"] = "Invalid Service Number. Please Contact your Admin";
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
            var enableSSL = Convert.ToBoolean("True");

            ///SmtpClient SmtpServer = new SmtpClient(host);

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
                            return RedirectToAction("ResetShipPassword");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message, "Error");
                    return default;
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
        public async Task<ActionResult> ResetPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MD5 md5Hash = MD5.Create();
                    var userToReset = HttpContext.Session.GetString("userToReset");   // retrieve the user from session
                    if (!string.IsNullOrEmpty(userToReset))
                    {
                        var userpersonnel = userService.GetUsers().Where(x => x.UserName == userToReset && x.personnel == true).FirstOrDefault();
                        if (userpersonnel != null)
                        {
                            var user = _authenticationService.FindUserById(userpersonnel.Id.ToString()).Result;

                            if(user != null)
                            {
                                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                                var resetpass = await _userManager.ResetPasswordAsync(user, token, model.ConfirmPassword);
                                if (resetpass.Succeeded)
                                {
                                    _authenticationService.updateUserlogins(user);
                                    HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");
                                    return RedirectToAction("Login");
                                }

                            }
                            HttpContext.Session.Clear();

                            return RedirectToAction("Login");
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
        public ActionResult ResetShipPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetShipPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MD5 md5Hash = MD5.Create();
                    var userToReset = HttpContext.Session.GetString("userToReset");   // retrieve the user from session
                    if (!string.IsNullOrEmpty(userToReset))
                    {
                        var userpersonnel = userService.GetUsers().Where(x => x.UserName == userToReset && x.shipcheck == true).FirstOrDefault();
                        if (userpersonnel != null)
                        {
                            var user = _authenticationService.FindUserById(userpersonnel.Id.ToString()).Result;

                            if (user != null)
                            {
                                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                                var resetpass = await _userManager.ResetPasswordAsync(user, token, model.ConfirmPassword);
                                if (resetpass.Succeeded)
                                {
                                    _authenticationService.updateUserlogins(user);
                                    HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");
                                    return RedirectToAction("ShipLogin");
                                }

                            }
                            HttpContext.Session.Clear();

                            return RedirectToAction("Login");
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
        public List<SelectListItem> GetCommand()
        {
            var commandList = (from rk in _context.ef_commands
                               select new SelectListItem()
                               {
                                   Text = rk.commandName,
                                   Value = rk.code.ToString(),
                               }).ToList();

            commandList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return commandList;
        }
        public List<ef_ship> GetShip2(int commandId)
        {
            var lgaList = (from rk in _context.ef_ships
                           where (rk.commandid == commandId)
                           select new ef_ship()
                           {
                               shipName = rk.shipName,
                               Id = rk.Id,
                           }).ToList();


            return lgaList;
        }
        public IActionResult getships(int id)
        {
            var shipss = new List<ef_ship>();
            shipss = getshipsFromDatabaseByCommandId(id); //call repository
            return Json(shipss);
        }
        public List<ef_ship> getshipsFromDatabaseByCommandId(int id)
        {
            return _context.ef_ships.Where(c => c.commandid == id).ToList();
        }

        public List<SelectListItem> GetShip()
        {
            var lgaList = (from rk in _context.ef_ships
                           select new SelectListItem()
                           {
                               Text = rk.shipName,
                               Value = rk.Id.ToString(),
                           }).ToList();


            return lgaList;
        }
        public ActionResult Getshipbycommand(string id)
        {
            var lgas = _context.ef_ships.Where(x => x.code == id).ToList();
            return Json(new SelectList(lgas, "shipName", "shipName"));
        }
        public ActionResult Register()
        {
            var reg = new RegisterVM
            {
            CommandList= GetCommand(),
            RankList = GetRank(),
               shipList = GetShips()
            };
            return View(reg);
        }
       
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM Input)
        {
            try { 
            GetCommand();
            GetShips();
            GetRank();
            var pers = userService.GetUsers().Where(x => x.UserName == Input.UserName && x.shipcheck == true).FirstOrDefault();
            if (pers != null)
            {
                TempData["regError"] = "User Already Exist";
                return RedirectToAction("Register");
            }
             if (ModelState.IsValid)
            {

                var user = new User
                {
                    UserName = Input.UserName,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Rank = Input.Rank,
                    ship = Input.Ship,
                    command = Input.Command,
                    PhoneNumber = "+234" + Input.PhoneNumber.Substring(1, 10),
                    shipcheck = true
                };

                var createdUser = await userService.CreateUser(user,Input.Rolesid, Input.Password, 0);
                if (createdUser.Success)
                {
                    TempData["SuccessMessage"] = "Successfully created User";
                    return RedirectToAction(nameof(GetshipUser));
                }
                else
                {
                    TempData["ErrorMessage"] = "Unable To Create User, Contact Hicad";
                    return RedirectToAction(nameof(GetshipUser));
                }

            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Register");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }
        public IActionResult EditRegister(int id)
        {
            try { 
            GetCommand();
            GetShips();
            GetRank();
            var ships = userService.GetUserById(id).Result;
            var Roles = ships.UserRoles?.Select(x => x.RoleId).ToArray();

            var sh = new RegisterVM()
            {
                Id = ships.Id,
                UserName = ships.UserName,
                FirstName = ships.FirstName,
                LastName = ships.LastName,
                Rank = ships.Rank,
                Ship = ships.ship,
                Command = ships.command,
                PhoneNumber = ships.PhoneNumber,
                CommandList = GetCommand(),
                RankList = GetRank(),
                shipList = GetShips()
            };
            return View(sh);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditRegister(RegisterVM Input)
        {
            try { 
            GetCommand();
            GetShips();
            GetRank();
            if (ModelState.IsValid)
            {
                var user = await userService.GetUserById(Input.Id);
                user.Id = Input.Id;
                user.UserName = Input.UserName;
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.Rank = Input.Rank;
                user.PhoneNumber = "+234" + Input.PhoneNumber.Substring(1, 10);
                user.ship = Input.Ship;
                user.command = Input.Command;
                user.shipcheck = true;


                var response = await userService.UpdateUserInfo(user, Input.Rolesid, 0);
                if (response.Succeeded)
                {
                    TempData["SuccessMessage"] = "Successfully Updated User";
                    return RedirectToAction(nameof(GetshipUser));
                }
                TempData["ErrorMessage"] = response.Errors.First().Description;
                return RedirectToAction(nameof(GetshipUser));
            }
            TempData["ErrorMessage"] = "Cannot update new user";
            return RedirectToAction(nameof(GetshipUser));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }
        public async Task<IActionResult> GetshipUser()
        {

            try
            {

            var list = userService.GetUsers().Where(x=>x.shipcheck==true);
            return  View(list.OrderBy(x=>x.UserName).ThenBy(x=>x.ship));

            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
        }
        public IActionResult DeleteShipUser(int id)
        {
            
            _shipService.DeleteUser(id);
            return RedirectToAction("GetshipUser");
        }
        public List<SelectListItem> GetRank()
        {
            var RankList = (from rk in _context.ef_ranks
                            select new SelectListItem()
                            {
                                Text = rk.rankName,
                                Value = rk.rankName.ToString(),
                            }).ToList();

            RankList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            return RankList;
        }
        public List<SelectListItem> GetShips()
        {
            var shipList = (from rk in _context.ef_ships
                            select new SelectListItem()
                            {
                                Text = rk.shipName,
                                Value = rk.shipName.ToString(),
                            }).ToList();

            shipList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            return shipList;
        }
        public List<ef_command> GetCommand2()
        {
           var CommandList = (from rk in _context.ef_commands
                           select new ef_command()
                           {
                               commandName = rk.commandName,
                               code = rk.code,
                           }).ToList();
            return CommandList;

        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(System.Index));
            }
            catch
            {
                return View();
            }
        }

         public IActionResult UpdatePayrollWithEF()
        {
            ViewBag.commandList = GetCommand();
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePayrollWithEF(string payclass)
        {
            ViewBag.commandList = GetCommand();
            
            try
            {
                List<string> serviceNumbers = new List<string>();
                SqlConnection remoteDB = new SqlConnection(_remoteConnectionstring);
                SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM ef_personalInfos where emolumentform = 'Yes' AND classes = {payclass}", remoteDB);
                remoteDB.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    var serviceNumber = sqlDataReader["serviceNumber"].ToString();

                    using (SqlConnection localDB = new SqlConnection(_localConnectionstring))
                    {
                        using (SqlCommand localCommand = new SqlCommand("UPDATE hr_employees SET emolumentform = 'Yes' WHERE Empl_ID = @serviceNumber", localDB))
                        {
                            localCommand.CommandType = CommandType.Text;
                            localCommand.Parameters.AddWithValue("@serviceNumber", serviceNumber);
                            localDB.Open();
                            int rowsAffected = localCommand.ExecuteNonQuery();
                            localDB.Close();
                        };
                    }

                    serviceNumbers.Add(serviceNumber);
                }

                remoteDB.Close();

                for (int i = 0; i <= serviceNumbers.ToArray().Length - 1; i++)
                {
                    using (SqlConnection remoteUpdateDB = new SqlConnection(_remoteConnectionstring))
                    {
                        using (SqlCommand sqlUpdateRemoteCommand = new SqlCommand("UPDATE ef_personalInfos SET emolumentform = 'Appr' WHERE serviceNumber = @serviceNumber", remoteUpdateDB))
                        {
                            sqlUpdateRemoteCommand.CommandType = CommandType.Text;
                            sqlUpdateRemoteCommand.Parameters.AddWithValue("@serviceNumber", serviceNumbers[i]);
                            remoteUpdateDB.Open();
                            int remoteRowsAffected = sqlUpdateRemoteCommand.ExecuteNonQuery();
                            remoteUpdateDB.Close();
                        };

                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
            }
            return View();
        }
        public IActionResult ClosingPage()
        {
            return View();
        }
        public IActionResult UploadShipUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadShipUser(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {

            if (formFile == null || formFile.Length <= 0)
            {
                TempData["message"] = "No File Uploaded";
                return View();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["message"] = "File not an Excel Format";
                return View();
            }
            var listapplication = new List<personLoginVM>();
            var listapplicationofrecordnotavailable = new List<personLoginVM>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;
                   
                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[1, 1].Value == null)
                            worksheet.Cells[1, 1].Value = "";

                        if (worksheet.Cells[1, 2].Value == null)
                            worksheet.Cells[1, 2].Value = "";

                        if (worksheet.Cells[1, 3].Value == null)
                            worksheet.Cells[1, 3].Value = "";

                        if (worksheet.Cells[1, 4].Value == null)
                            worksheet.Cells[1, 4].Value = "";
                        if (worksheet.Cells[1, 5].Value == null)
                            worksheet.Cells[1, 5].Value = "";
                        if (worksheet.Cells[1, 6].Value == null)
                            worksheet.Cells[1, 6].Value = "";



                        if (worksheet.Cells[row, 1].Value == null)
                            worksheet.Cells[row, 1].Value = "";

                        if (worksheet.Cells[row, 2].Value == null)
                            worksheet.Cells[row, 2].Value = "";

                        if (worksheet.Cells[row, 3].Value == null)
                            worksheet.Cells[row, 3].Value = "";

                        if (worksheet.Cells[row, 4].Value == null)
                            worksheet.Cells[row, 4].Value = "";

                        if (worksheet.Cells[row, 5].Value == null)
                            worksheet.Cells[row, 5].Value = "";
                        if (worksheet.Cells[row, 6].Value == null)
                            worksheet.Cells[row, 6].Value = "";
                        if (worksheet.Cells[row, 7].Value == null)
                            worksheet.Cells[row, 7].Value = "";
                        if (worksheet.Cells[row, 8].Value == null)
                            worksheet.Cells[row, 8].Value = "";
                        if (worksheet.Cells[row, 9].Value == null)
                            worksheet.Cells[row, 9].Value = "";


                            //string command = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                            //string ship = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();

                        string svcno = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        string rank = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                        string name = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                        string initial = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                        string ship = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();
                        string password = String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ? "" : worksheet.Cells[row, 6].Value.ToString().Trim();
                        string command = String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ? "" : worksheet.Cells[row, 7].Value.ToString().Trim();
                        string phonenumber = String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()) ? "" : worksheet.Cells[row, 8].Value.ToString().Trim();
                        string email = String.IsNullOrEmpty(worksheet.Cells[row, 9].Value.ToString()) ? "" : worksheet.Cells[row, 9].Value.ToString().Trim();

                            command = _context.ef_ships.Where(x => x.shipName == ship).FirstOrDefault().code;

                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()))

                        {
                            listapplicationofrecordnotavailable.Add(new personLoginVM
                            {
                                svcNo = svcno,
                                rank = rank,
                                surName = name,
                                otheName=initial,
                                department =command,
                                ship=ship,
                                password=password,
                                phoneNumber="+"+phonenumber,
                                email=email
                            });

                        }
                        else
                        {
                            //check if already in the list -- a possibility
                            listapplication.Add(new personLoginVM
                            {
                                svcNo = svcno,
                                rank = rank,
                                surName = name,
                                otheName=initial,
                                department = command,
                                ship = ship,
                                password=password,
                                phoneNumber = "+" + phonenumber,
                                email = email
                            });
                        }

                    }
                   
                    string userp = User.Identity.Name;

                    ProcesUpload procesUpload2 = new ProcesUpload(null, connectionstring, listapplication, _unitOfWorks, userp);
                    await procesUpload2.processUploadShipUser();
                    TempData["message"] = "Uploaded Successfully";

                }

            }
            if (listapplicationofrecordnotavailable.Count > 0)
            {

                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(listapplicationofrecordnotavailable, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                //return File(stream, "application/octet-stream", excelName);  
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }

            }
            catch (Exception ex)
            {
                _logger.LogError("Upload Error", ex.Message);
                return View("UploadShipUser");
            }
            return View();
        }
        public IActionResult UploadUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadUser(IFormFile formFile, CancellationToken cancellationToken)
        {
            try { 
            if (formFile == null || formFile.Length <= 0)
            {
                TempData["message"] = "No File Uploaded";
                return View();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["message"] = "File not an Excel Format";
                return View();
            }
            var listapplication = new List<personLoginVM>();
            var listapplicationofrecordnotavailable = new List<personLoginVM>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    var rowCount = worksheet.Dimension.Rows;
                    string SVC_NO = String.IsNullOrEmpty(worksheet.Cells[1, 1].ToString()) ? "" : worksheet.Cells[1, 1].Value.ToString().Trim();
                    string RANK = String.IsNullOrEmpty(worksheet.Cells[1, 2].ToString()) ? "" : worksheet.Cells[1, 2].Value.ToString().Trim();
                    string SURNAME = String.IsNullOrEmpty(worksheet.Cells[1, 3].ToString()) ? "" : worksheet.Cells[1, 3].Value.ToString().Trim();
                    string OTHERNAMES = String.IsNullOrEmpty(worksheet.Cells[1, 4].ToString()) ? "" : worksheet.Cells[1, 4].Value.ToString().Trim();
                    string APPOINTMENT = String.IsNullOrEmpty(worksheet.Cells[1, 5].ToString()) ? "" : worksheet.Cells[1, 5].Value.ToString().Trim();
                    string PHONE = String.IsNullOrEmpty(worksheet.Cells[1, 6].ToString()) ? "" : worksheet.Cells[1, 6].Value.ToString().Trim();
                    string EMAIL = String.IsNullOrEmpty(worksheet.Cells[1, 7].ToString()) ? "" : worksheet.Cells[1, 7].Value.ToString().Trim();
                    string DEPARTMENT = String.IsNullOrEmpty(worksheet.Cells[1, 8].ToString()) ? "" : worksheet.Cells[1, 8].Value.ToString().Trim();
                    string SHIP = String.IsNullOrEmpty(worksheet.Cells[1, 9].ToString()) ? "" : worksheet.Cells[1, 9].Value.ToString().Trim();

                    if (SVC_NO != "SVC_NO" || RANK != "RANK" || SURNAME != "SURNAME" || APPOINTMENT != "APPOINTMENT" || EMAIL != "EMAIL"
                        || PHONE != "PHONE")
                    {
                        return BadRequest("File not in the Right format");
                    }
                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[1, 1].Value == null)
                            worksheet.Cells[1, 1].Value = "";

                        if (worksheet.Cells[1, 2].Value == null)
                            worksheet.Cells[1, 2].Value = "";

                        if (worksheet.Cells[1, 3].Value == null)
                            worksheet.Cells[1, 3].Value = "";

                        if (worksheet.Cells[1, 4].Value == null)
                            worksheet.Cells[1, 4].Value = "";
                        if (worksheet.Cells[1, 5].Value == null)
                            worksheet.Cells[1, 5].Value = "";
                        if (worksheet.Cells[1, 6].Value == null)
                            worksheet.Cells[1, 6].Value = "";
                        if (worksheet.Cells[1, 7].Value == null)
                            worksheet.Cells[1, 7].Value = "";
                        if (worksheet.Cells[1, 8].Value == null)
                            worksheet.Cells[1, 8].Value = "";
                        if (worksheet.Cells[1, 9].Value == null)
                            worksheet.Cells[1, 9].Value = "";


                        if (worksheet.Cells[row, 1].Value == null)
                            worksheet.Cells[row, 1].Value = "";

                        if (worksheet.Cells[row, 2].Value == null)
                            worksheet.Cells[row, 2].Value = "";

                        if (worksheet.Cells[row, 3].Value == null)
                            worksheet.Cells[row, 3].Value = "";

                        if (worksheet.Cells[row, 4].Value == null)
                            worksheet.Cells[row, 4].Value = "";

                        if (worksheet.Cells[row, 5].Value == null)
                            worksheet.Cells[row, 5].Value = "";

                        if (worksheet.Cells[row, 6].Value == null)
                            worksheet.Cells[row, 6].Value = "";
                        if (worksheet.Cells[row, 7].Value == null)
                            worksheet.Cells[row, 7].Value = "";
                        if (worksheet.Cells[row, 8].Value == null)
                            worksheet.Cells[row, 8].Value = "";
                        if (worksheet.Cells[row, 9].Value == null)
                            worksheet.Cells[row, 9].Value = "";



                        string svcno = String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ? "" : worksheet.Cells[row, 1].Value.ToString().Trim();
                        string rank = String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ? "" : worksheet.Cells[row, 2].Value.ToString().Trim();
                        string surname = String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()) ? "" : worksheet.Cells[row, 3].Value.ToString().Trim();
                        string othername = String.IsNullOrEmpty(worksheet.Cells[row, 4].Value.ToString()) ? "" : worksheet.Cells[row, 4].Value.ToString().Trim();
                        string appointment = String.IsNullOrEmpty(worksheet.Cells[row, 5].Value.ToString()) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim();
                        string phone = String.IsNullOrEmpty(worksheet.Cells[row, 6].Value.ToString()) ? "" : worksheet.Cells[row, 6].Value.ToString().Trim();
                        string email = String.IsNullOrEmpty(worksheet.Cells[row, 7].Value.ToString()) ? "" : worksheet.Cells[row, 7].Value.ToString().Trim();
                        string department = String.IsNullOrEmpty(worksheet.Cells[row, 8].Value.ToString()) ? "" : worksheet.Cells[row, 8].Value.ToString().Trim();
                        string ship = String.IsNullOrEmpty(worksheet.Cells[row, 9].Value.ToString()) ? "" : worksheet.Cells[row, 9].Value.ToString().Trim();


                        if (String.IsNullOrEmpty(worksheet.Cells[row, 1].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 2].Value.ToString()) ||
                           String.IsNullOrEmpty(worksheet.Cells[row, 3].Value.ToString()))

                        {
                            listapplicationofrecordnotavailable.Add(new personLoginVM
                            {
                                svcNo = svcno,
                                rank = rank,
                                surName = surname,
                                otheName = othername,
                                appointment = appointment,
                                phoneNumber = phone,
                                email = email,
                                department = department,
                                ship = ship
                            });

                        }
                        else
                        {
                            //check if already in the list -- a possibility
                            listapplication.Add(new personLoginVM
                            {
                                svcNo = svcno,
                                rank = rank,
                                surName = surname,
                                otheName = othername,
                                appointment = appointment,
                                phoneNumber = phone,
                                email = email,
                                department = department,
                                ship = ship
                            });
                        }

                    }
                    string userp = User.Identity.Name;

                    ProcesUpload procesUpload2 = new ProcesUpload(null,connectionstring, listapplication, _unitOfWorks, userp);
                    await procesUpload2.processUploadInThread2();
                    TempData["message"] = "Uploaded Successfully";

                }

            }
            if (listapplicationofrecordnotavailable.Count > 0)
            {

                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet2");
                    workSheet.Cells.LoadFromCollection(listapplicationofrecordnotavailable, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                //return File(stream, "application/octet-stream", excelName);  
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            return View();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Error");
                return default;
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
    }
}
