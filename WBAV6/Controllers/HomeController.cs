using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WBAV6.Models;
using WBAV6.Helpers;
using Microsoft.EntityFrameworkCore;

namespace WBAV6.Controllers
{
    [ValidationHelper]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WBAV6Context _context;

        public HomeController(ILogger<HomeController> logger, WBAV6Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var profiles = _context.Profiles.Include(m => m.User);
            return View(await profiles.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
