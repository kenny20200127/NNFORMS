using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    public class NLoginController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IPersonService personService;
        private readonly IEmailService _EmailService;
        private readonly IUnitOfWorks unitOfWorks;
        private readonly IConfiguration config;
        private readonly IMailService _mailService;
        private readonly IAuthService authenticationService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        private int resetcode;
        Random rnd = new Random();
        private readonly ILogger<NLoginController> _logger;

        public NLoginController
         (
            UserManager<User> userManager,
            ILogger<NLoginController> logger,
            ApplicationDbContext context,
            IPersonService personService, 
            IUnitOfWorks unitOfWorks,
            IConfiguration config,
            IMailService mailService,
            IAuthService authenticationService,
            IUserService userService
            ) : base(userService)

        {

            this.userManager = userManager;
            _logger = logger;
            this.personService = personService;
            this.unitOfWorks = unitOfWorks;
            this.config = config;
            _context = context;
            _mailService = mailService;
            this.authenticationService = authenticationService;
            this.userService = userService;
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
                    return RedirectToAction("login", "NLogin");
                }
                if (ModelState.IsValid)
                {
                    if (userlogin.ResetPasswordCode != null)
                    {
                        if (string.IsNullOrWhiteSpace(login.password))
                        {
                            TempData["errorMessage"] = "Please enter you password";
                            return RedirectToAction("Login", "NLogin");
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
                                    return RedirectToAction("AdminIndex", "NLogin");
                                }
                                else
                                {
                                    return RedirectToAction("PensionGratuity", "PensionForms");
                                }
                            }
                            else
                            {
                                TempData["errorMessage"] = "Invalid credentials";
                                return RedirectToAction("Login", "NLogin");
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
        public IActionResult Index()
        {
            string role = HttpContext.Session.GetString("userRole").ToString();

            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
  
            UserViewModels userView = new();
            userView.roleRecords = personService.GetRoles(role).ToList();
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

        public async Task<IActionResult> GetAdminUser(int ?pageNumber,string searchString)
        {
            string role = HttpContext.Session.GetString("userRole").ToString();

            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            UserView model = new UserView();
            UserViewModels models= new UserViewModels();
            models.roleRecords = personService.GetRoles(role).ToList();
            var listOfusers = await personService.GetUsers(role);

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
            string role = HttpContext.Session.GetString("userRole").ToString();

            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            var roles = personService.GetRoles(role).ToList();
            return Ok(roles) ;
        }

       
        public async Task<IActionResult> AdminIndex()
        {
            userCountModel model = new();
            var countUser= await personService.GetUserCount();
            model.count1 = countUser.count1;
            model.count2 = countUser.count2;
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
                        return RedirectToAction("ResetPassword", "NLogin");
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
                                        return RedirectToAction("Login");
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

        
    }
}
