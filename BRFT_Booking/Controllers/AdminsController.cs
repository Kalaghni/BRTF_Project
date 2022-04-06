using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRTF_Booking.Data;
using BRTF_Booking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using BRTF_Booking.Utilities;

namespace BRTF_Booking.Controllers
{
    [Authorize(Roles="Top-Level Admin")]
    public class AdminsController : Controller
    {
        private readonly BRTFContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminsController(BRTFContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);
            return View(await _context.Admins.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewDataReturnURL();

            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FirstOrDefaultAsync(m => m.ID == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            ViewDataReturnURL();
            //List<string> roles = new List<string>();
            //roles.Add("Admin");
            //roles.Add("Top-Level Admin");

            //ViewData["Role"] = new SelectList(roles);
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FName,LName,Email,Role")] Admin admin, string? password)
        {
            ViewDataReturnURL();

            try
            {
                if (ModelState.IsValid)
                {
                    string roleString = "Admin";
                    if (admin.Role != null)
                    {
                        roleString = admin.Role;
                    }
                    if (_userManager.FindByEmailAsync(admin.Email).Result == null)
                    {
                        IdentityUser user = new IdentityUser
                        {
                            UserName = admin.Email,
                            Email = admin.Email
                        };

                        IdentityResult result = _userManager.CreateAsync(user, password).Result;

                        if (result.Succeeded)
                        {
                            _userManager.AddToRoleAsync(user, roleString).Wait();
                        }
                    }
                    _context.Add(admin);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                {
                    ModelState.AddModelError("Email", "Email already exists. Please try a different one!");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewDataReturnURL();

            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            ViewDataReturnURL();

            var adminToUpdate = await _context.Admins.FindAsync(id);
            if (adminToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Admin>(adminToUpdate, "", a => a.FName, a => a.LName, a => a.Email, a => a.Role))
            {
                try
                {
                    if (adminToUpdate.Role == "Top-Level Admin")
                    {
                        IdentityUser user = _userManager.FindByEmailAsync(adminToUpdate.Email).Result;
                        await _userManager.RemoveFromRoleAsync(user, "Admin");
                        await _userManager.AddToRoleAsync(user, "Top-Level Admin");
                        _context.Update(adminToUpdate);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(adminToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(adminToUpdate);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewDataReturnURL();

            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.ID == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewDataReturnURL();

            var admin = await _context.Admins.FindAsync(id);
            try
            {
                await _userManager.DeleteAsync(_userManager.FindByEmailAsync(admin.Email).Result);
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to delete record. Try again, and if the problem persists see your system administrator.");
            }
            return View(admin);
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.ID == id);
        }
    }
}
