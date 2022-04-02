using BRTF_Booking.Data;
using BRTF_Booking.Models;
using BRTF_Booking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BRTFContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager, ILogger<HomeController> logger, BRTFContext context)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var users = _context.Users
                .Include(b => b.Bookings)
                .ThenInclude(b => b.Room);
            return View(users);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Top-Level Admin")]
        public IActionResult Settings()
        {
            return View();
        }

        [Authorize(Roles = "Top-Level Admin")]
        [HttpGet]
        public JsonResult GetSettings()
        {
            return Json(_context.SettingsViewModels.First());
        }

        [Authorize(Roles = "Top-Level Admin")]
        [HttpPost]
        public ActionResult PostSettings(SettingsViewModel settings)
        {
            if (settings != null)
            {
                if (ModelState.IsValid)
                {
                    if (_context.SettingsViewModels.First().EmailExtension != settings.EmailExtension)
                    {

                        foreach (User user in _context.Users)
                        {
                            string newEmail = user.Email.Substring(0, user.Email.Length - _context.SettingsViewModels.First().EmailExtension.Length) + settings.EmailExtension;
                            _userManager.FindByNameAsync(user.Email).Result.Email = newEmail;
                            _userManager.FindByNameAsync(user.Email).Result.UserName = newEmail;
                            _userManager.UpdateAsync(_userManager.FindByNameAsync(user.Email).Result);
                            user.Email = newEmail;
                            _context.Users.Update(user);
                            _context.SaveChanges();
                        }

                    }

                    _context.SettingsViewModels.Remove(_context.SettingsViewModels.First());
                    _context.SettingsViewModels.Add(settings);
                    _context.SaveChanges();
                    return Json("Success");
                } 

               else
                {
                    return Json("Model is invalid");
                }
            }
            else
            {
                return Json("An error has occured");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
