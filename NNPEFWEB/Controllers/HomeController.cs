 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;

namespace NNPEFWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboard _dashboard;
        private readonly ICommandDashboard _commanddashboard;
        private readonly ISectionDashboard _sectiondashboard;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IDashboard dashboard, ICommandDashboard commanddashboard, ISectionDashboard sectiondashboard,ApplicationDbContext context)
        {
            _logger = logger;
            _dashboard = dashboard;
            _context = context;
            _commanddashboard = commanddashboard;
            _sectiondashboard = sectiondashboard;
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Command()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //return RedirectToAction("HomePage","Home");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
