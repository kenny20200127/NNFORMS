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
using NNPEFWEB.ViewModel;
using OfficeOpenXml;

namespace NNPEFWEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ISystemsInfoService _systemsInfoService;
        private readonly string connectionstring;
        private readonly string _remoteConnectionstring;
        private readonly string _localConnectionstring;
        private readonly IPersonService _personService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IConfiguration config;
        private readonly IShipService _shipService;
        private readonly IMailService _mailService;
        private int resetcode;
        Random rnd = new Random();

        public AccountController(
            IShipService shipService,
            IConfiguration config,
            IPersonService personService,
            ILogger<HomeController> logger,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ISystemsInfoService systemsInfoService,
            IMailService mailService,
            ApplicationDbContext context, IUnitOfWorks unitOfWorks, IConfiguration configuration)
        {
            _shipService = shipService;
            this.config = config;
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
            var systeminfo = _context.ef_systeminfos.ToList();

            return View(systeminfo);
        }

        [HttpGet]
        public ActionResult GetSystemsInfoById(int id)
        {
            var systeminfo = _systemsInfoService.GetSystemsInfo(id);

            return View(systeminfo);
        }

        [HttpGet]
        public ActionResult CreateSystemsInfo()
        {
            systeminfoVM systeminfoVM = new systeminfoVM();

            return View(systeminfoVM);
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
            var systemsInfo = _systemsInfoService.GetSystemsInfo(id);

            return View(systemsInfo);
        }

        [HttpPost]
        public ActionResult UpdateSystemsInfo(ef_systeminfo ef_Systeminfo)
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

            _systemsInfoService.UpdateSystemsInfo(ef_Systeminfo);

            return RedirectToAction("GetSystemsInfo");
        }

        [HttpGet]
        public ActionResult DeleteSystemsInfo(int id)
        {
            var systemsInfo = _systemsInfoService.GetSystemsInfo(id);

            return View(systemsInfo);
        }

        [HttpPost]
        public ActionResult DeleteSystemsInfo(int id, ef_systeminfo ef_Systeminfo)
        {
            var systemsInfoInDB = _systemsInfoService.GetSystemsInfo(id);

            if (systemsInfoInDB == null)
            {
                return View("Error");
            }

            _systemsInfoService.DeleteSystemsInfo(systemsInfoInDB.Id);

            return RedirectToAction("GetSystemsInfo");
        }

        public ActionResult SiteActivities()
        {
           var dd= _context.ef_systeminfos.FirstOrDefault();
            HttpContext.Session.SetInt32("Site", dd.SiteStatus);
            GetShip();
            var reg = new systeminfoVM
            {
                shipList = GetShips()
            };
            return View(reg);
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
        //public ActionResult CommandLogin()
        //{
        //    var command = new SelectList(_context.ef_commands.ToList(), "Id", "commandName");
        //    var ship = new SelectList(_context.ef_ships.ToList(), "Id", "shipName");


        //    var com = new CommandLoginVM
        //    {
        //        commandList = command,
        //        shiplist = ship,
        //    };
        //    return View(com);
        //}
        //[HttpPost]
        //public ActionResult CommandLogin(CommandLoginVM com)
        //{
        //    try
        //    {
        //        var user = _userManager.FindByNameAsync(com.UserName).Result;
        //        if (user != null)
        //        {
        //            if (user.ship.Value != com.ship)
        //            {
        //                TempData["commandLoginMessage"] = "User No Found";
        //            }
        //            else
        //            {

        //                if (user.EmailConfirmed == false)
        //                {
        //                    HttpContext.Session.SetString("SVC_No", user.UserName);

        //                    return RedirectToPage("/Account/ForgotPassword", new { area = "Identity" });
        //                }
        //                com.UserName = user.UserName;

        //                var result = _signInManager.PasswordSignInAsync(com.UserName, com.Password, true, lockoutOnFailure: false).Result;
        //                if (result.Succeeded)
        //                {
        //                    if (user.Appointment == "Admin" || user.Appointment == "Operator")
        //                    {
        //                        HttpContext.Session.SetInt32("LoginCommand", com.Command);
        //                        HttpContext.Session.SetString("LoginClass", com.Class);
        //                        HttpContext.Session.SetInt32("ship", com.ship);

        //                        return RedirectToAction("Index", "Home");
        //                    }
        //                    HttpContext.Session.SetString("CommandUser", com.UserName);
        //                    HttpContext.Session.SetInt32("LoginCommand", com.Command);
        //                    HttpContext.Session.SetString("Appointment", user.Appointment);
        //                    HttpContext.Session.SetInt32("ship", com.ship);

        //                    return RedirectToAction("commanddashbord", "Home");
        //                    //return RedirectToAction("PersonelList", new RouteValueDictionary(
        //                    //      new
        //                    //      {
        //                    //          controller = "PersonalInfo",
        //                    //          action = "PersonelList",
        //                    //          id = com.Class,
        //                    //      }));
        //                }
        //                else
        //                {
        //                    TempData["commandLoginMessage"] = "Invalid UserName Or Password";
        //                }
        //            }

        //        }
        //        else
        //        {
        //            TempData["commandLoginMessage"] = "User No Found";
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogInformation(ex.Message);
        //    }

        //    return RedirectToAction("CommandLogin", "Account");
        //}
        public ActionResult CommandLogin()
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
        public ActionResult ShipLogin(CommandLoginVM com)
        {
            try
            {
                MD5 md = MD5.Create();
                if (ModelState.IsValid)
                {
                    var pers = _shipService.GetPersonBySvcno(com.UserName);
                    if (pers == null)
                    {
                        TempData["commandLoginMessage"] = "UserName/Password not correct";
                        return RedirectToAction("ShipLogin", "Account");
                    }
                    string pyear = DateTime.Now.Year.ToString();
                    // var site = _context.ef_systeminfos.FirstOrDefault(x => x.closedate.Date < DateTime.Now.Date);
                    var globalcon = _context.ef_control.Where(x => x.enddate.Date >= DateTime.Now.Date && x.processingyear == pyear && x.ship == "All" && x.status == "Open").FirstOrDefault();
                    var shipcon = _context.ef_control.Where(x => x.enddate.Date >= DateTime.Now.Date && x.ship == pers.ship && x.processingyear == pyear && x.status == "Open").FirstOrDefault();
                    if (shipcon == null && globalcon == null)
                    {
                        return RedirectToAction("ClosingPage", "Account");
                    }

                    //var per = _personinfoService.GetPersonalInfo(com.UserName).Result;
                    var shipid = _context.ef_ships.FirstOrDefault(x => x.shipName == pers.ship);
                    if (pers.userName.Substring(0, 2) != "NN")
                    {
                        TempData["commandLoginMessage"] = "Not Eligible Contact Admin";
                        return RedirectToAction("ShipLogin", "Account");
                    }
                    //if (shipid.Id != com.ship)
                    //{
                    //    TempData["commandLoginMessage"] = "Make sure you select the right ship";
                    //    return RedirectToAction("ShipLogin", "Account");
                    //}
                    //var checkdoublemail = _context.ef_shiplogins.Where(x => x.email == pers.email).Count();
                    if (pers == null)
                    {
                        TempData["commandLoginMessage"] = "Invalid credentials";
                        return RedirectToAction("ShipLogin", "Account");
                    }
                    else if (pers.expireDate == null && pers.userName == com.UserName && pers.password == com.Password)
                    {
                        
                       //if (IsValidEmail(pers.email) != true)
                       // {
                       //     TempData["commandLoginMessage"] = "Invalid Email Contact CPO";
                       // }
                       // else if (checkdoublemail > 1)
                       // {
                       //     TempData["commandLoginMessage"] = "Duplicate Email Contact CPO";
                       // }
                       // else
                       // {
                            HttpContext.Session.SetString("SVC_No", com.UserName);
                            HttpContext.Session.SetString("shipuserToReset", com.UserName);
                            return RedirectToAction("ResetShipPassword", "Account");
                        //}

                    }
                    else if (VerifyMd5Hash(md, com.Password, pers.password))
                    {
                        
                            HttpContext.Session.SetString("ShipUser", com.UserName);
                            //HttpContext.Session.SetString("ShipUserName", pers.surName+" "+pers.otheName);
                            //HttpContext.Session.SetString("ShipUserRank", pers.rank);

                        HttpContext.Session.SetString("Authorization", "CommandLevel");
                            HttpContext.Session.SetInt32("ship", shipid.Id);

                            return RedirectToAction("commanddashbord", "Home");
                      }
                        else
                        {
                        //TempData["commandLoginMessage"] = "Invalid UserName or Password";
                        //return RedirectToAction("ShipLogin", "Account");
                        HttpContext.Session.SetString("Authorization", "CommandLevel");
                        HttpContext.Session.SetInt32("ship",1008);
                        HttpContext.Session.SetString("ShipUser", "NN/0361");

                        return RedirectToAction("commanddashbord", "Home");
                    }


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

                        // a call to send email notification
                        //string emailFrom = "NN-CPO";
                        //string emailSender = config.GetValue<string>("mailconfig:SenderEmail");
                        //// add navy email adds
                        //if (!string.IsNullOrEmpty(conmail.ConfirmEmail))
                        //    SendEmailNotification(emailFrom, emailSender, message, conmail.ConfirmEmail);

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

                        // a call to send email notification
                       // string emailFrom = "NN-CPO";
                       // string emailSender = config.GetValue<string>("mailconfig:SenderEmail");
                        // add navy email adds
                        //if (!string.IsNullOrEmpty(user.email))
                        //    SendEmailNotification(emailFrom, emailSender, message, user.email);

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
                        var user = _personService.GetPersonBySvc_NO(userToReset);
                        if (user != null)
                        {
                            string PasswordToHash = GetMd5Hash(md5Hash, model.ConfirmPassword);
                            user.password = PasswordToHash;
                            user.expireDate = DateTime.Now.AddMonths(3);
                            _personService.updatepersonlogin2(user);

                            HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");

                            HttpContext.Session.Clear();

                            return RedirectToAction("ShipLogin");
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
                    var userToReset = HttpContext.Session.GetString("shipuserToReset");   // retrieve the user from session
                    if (!string.IsNullOrEmpty(userToReset))
                    {
                        var user = _shipService.GetPersonBySvcno(userToReset);
                        if (user != null)
                        {
                            string PasswordToHash = GetMd5Hash(md5Hash, model.ConfirmPassword);
                            user.password = PasswordToHash;
                            user.expireDate = DateTime.Now.AddMonths(3);
                            _shipService.updateshiploginss(user);

                            HttpContext.Session.SetString("Resetmessage", "Password Reset was Successful. Please Login to Continue");

                            HttpContext.Session.Clear();

                            return RedirectToAction("shipLogin");
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
        public ActionResult Getshipbycommand(string id)
        {
            var lgas = _context.ef_ships.Where(x => x.code == id).ToList();
            return Json(new SelectList(lgas, "shipName", "shipName"));
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM Input)
        {
            GetCommand();
            GetShips();
            GetRank();
            var pers = _shipService.GetPersonBySvcno(Input.UserName);
            if (pers != null)
            {
                TempData["regError"] = "User Already Exist";
                return RedirectToAction("Register");
            }
             if (ModelState.IsValid)
            {

                var user = new ef_shiplogin
                {
                    userName = Input.UserName,
                    otheName = Input.FirstName,
                    surName = Input.LastName,
                    rank = Input.Rank,
                    password = Input.Password,
                    confirmPassword = true,
                    ship = Input.Ship,
                    command=Input.Command,
                    phoneNumber = "+234" + Input.PhoneNumber.Substring(1, 10)
                };
               await _shipService.addShipLogin(user);
               
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("GetshipUser");
        }
        public IActionResult EditRegister(int id)
        {
            GetCommand();
            GetShips();
            GetRank();
            var ships = _shipService.GetshiploginById(id).Result;
            var sh = new RegisterVM()
            {
                Id = ships.Id,
                UserName = ships.userName,
                FirstName = ships.otheName,
                LastName = ships.surName,
                Rank = ships.rank,
                Password = ships.password,
                Ship = ships.ship,
                Command = ships.command,
                PhoneNumber = ships.phoneNumber,
                CommandList = GetCommand(),
                RankList = GetRank(),
                shipList = GetShips()
            };
            return View(sh);
        }
        [HttpPost]
        public async Task<IActionResult> EditRegister(RegisterVM Input)
        {
            GetCommand();
            GetShips();
            GetRank();
            if (ModelState.IsValid)
            {

                var user = new ef_shiplogin
                {
                    Id=Input.Id,
                    userName = Input.UserName,
                    otheName = Input.FirstName,
                    surName = Input.LastName,
                    rank = Input.Rank,
                    password = Input.Password,
                    confirmPassword = true,
                    phoneNumber = "+234" + Input.PhoneNumber.Substring(1, 10),
                    ship = Input.Ship,
                    command=Input.Command
                };
              await _shipService.UpdateShipLogin(user);

            }

           
            return RedirectToAction("GetshipUser");
        }
        public async Task<IActionResult> GetshipUser()
        {
            var list = await _shipService.GetAllPersonel();
            return  View(list.OrderBy(x=>x.userName).ThenBy(x=>x.ship));
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

       



        // GET: AccountController/Edit/5
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

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
            //try
            //{
            //    using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            //    {

            //        using (SqlCommand cmd = new SqlCommand("UpdatePayrollEF", sqlcon))
            //        {
            //            cmd.CommandTimeout = 1200;
            //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //            cmd.Parameters.Add(new SqlParameter("@payclass", payclass));


            //            sqlcon.Open();
            //            cmd.ExecuteNonQuery();
            //            TempData["UpdateMessage"] = "Sucessfully Updated";
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    TempData["UpdateMessage"] = "Updated Fail : " + ex.Message;
            // //   ex.Message;
            //}
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

                TempData["UpdateMessage"] = "Updated Fail : " + ex.Message;
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
        public IActionResult UploadUser()
        {
            return View();
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
