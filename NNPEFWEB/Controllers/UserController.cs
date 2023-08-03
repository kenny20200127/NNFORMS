
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NNPEFWEB.Data;
using NNPEFWEB.Extention;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NNPEFWEB.Controllers
{

    public class UserController : BaseController
    {
        private readonly IMapper mapper;

        public ApplicationDbContext _context { get; }

        private readonly IUserService userService;
        private readonly IRoleService roleService;
        public UserController(ApplicationDbContext context, IUserService userService, IMapper mapper,
            IRoleService roleService):base(userService)
        {
            this.roleService = roleService;
            this.userService = userService;
            this.mapper = mapper;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(new UsersViewModel
            {
                Users = userService.GetUsers().Where(x=>x.IsActive==true).OrderBy(x=>x.UserName).ToList(),
                Roles = roleService.GetActiveRoles()
            });
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
        public ActionResult Update(int id)
        {
            GetRank();
            var updatedUser = userService.GetUserById(id).Result;
            var Roles = updatedUser.UserRoles?.Select(x => x.RoleId).ToArray();

            var u = new UserViewModel()
            {
                FirstName = updatedUser.FirstName,
                Lastname = updatedUser.LastName,
                Email = updatedUser.Email,
                UserName = updatedUser.UserName,
                Appointment = updatedUser.Appointment,
                RankList = GetRank(),
                UpdatedOn = DateTime.Now,
                PhoneNumber=updatedUser.PhoneNumber,
                Users = userService.GetUsers().ToList(),
                Roles = roleService.GetActiveRoles()
            };
            return View(u);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel user)
        {
            if (user.Id > 0)
            {
                var updatedUser = await userService.GetUserById(user.Id);
                updatedUser.FirstName = user.FirstName;
                updatedUser.LastName = user.Lastname;
                updatedUser.UpdatedOn = DateTime.Now;
                updatedUser.Email = user.Email;
                updatedUser.UserName = user.UserName;
                updatedUser.Appointment = user.Appointment;
                updatedUser.PhoneNumber = "+234" + user.PhoneNumber.Substring(1, 10);

                var response = await userService.UpdateUserInfo(updatedUser, user.Rolesid, user.Device);
                if (response.Succeeded)
                {
                    TempData["SuccessMessage"] = "Successfully Updated User";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = response.Errors.First().Description;
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Cannot update new user";
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Create(UserViewModel user)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (user.UserName.Contains(item))
                {
                    TempData["ErrorMessage"] = "Special Character not allow for UserName";
                    return RedirectToAction(nameof(Index));
                };
            }
            var user2 = userService.GetUserByUserName(user.UserName).Result;
            if (user2 == null)
            {
                TempData["ErrorMessage"] = "User Already Exist";
                return RedirectToAction(nameof(Index));
            }
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.Lastname,                
                UpdatedOn = DateTime.Now,
                Email = user.Email,
                CreatedOn = DateTime.Now,
                UserName = user.UserName,
                Appointment=user.Appointment,
                PhoneNumber = "+234" + user.PhoneNumber.Substring(1, 10),

                IsActive = true,
            };

            var createdUser = await userService.CreateUser(newUser, user.Rolesid, user.Password, user.Device);
            if (createdUser.Success)
            {
                TempData["SuccessMessage"] = "Successfully created User";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Unable To Create User, Contact Hicad";
                return RedirectToAction(nameof(Index));
            }
                
            
        }

        [HttpGet]
        [Route("/user/{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await userService.GetUserRolesDevices(id);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Period does not exist";
                return RedirectToAction(nameof(Index));
            }
            var Roles = user.UserRoles?.Select(x => x.RoleId).ToArray();
            
            return Json(new
            {
                User = user,
                Roles = user.UserRoles?.Select(x => x.RoleId).ToArray()
            }.ToResponse(null, true));
        }

        [Route("/user/togglestatus/{id:int}")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var user = await userService.GetUserById(id);

            user.IsActive = !user.IsActive;

            var toggled = await userService.UpdateUser(user);

            if (!toggled.Succeeded)
            {
                TempData["ErrorMessage"] = toggled.Errors.ToList().First().Description;
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = $"Status Change Successful";
            return RedirectToAction(nameof(Index));
        }

    public async Task<IActionResult> EditUser(int id)
    {
        var user = await userService.GetUserById(id);

        user.IsActive = !user.IsActive;

        var toggled = await userService.UpdateUser(user);

        if (!toggled.Succeeded)
        {
            TempData["ErrorMessage"] = toggled.Errors.ToList().First().Description;
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = $"Status Change Successful";
        return RedirectToAction(nameof(Index));
    }
        public ActionResult DeleteUser(int id)
        {
            userService.DeleteUser(id);
            TempData["SuccessMessage"] = $"Remove Successful";
            return RedirectToAction(nameof(Index));

        }
}
}