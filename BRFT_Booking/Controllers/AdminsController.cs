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
            return View(await _context.Admins.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

        // GET: Admins/Create
        public IActionResult Create()
        {
            List<string> roles = new List<string>();
            roles.Add("Admin");
            roles.Add("Top-Level Admin");

            ViewData["Role"] = new SelectList(roles);
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FName,LName,Email,Role")] Admin admin, string? password)
        {
            if (ModelState.IsValid)
            {
                string roleString = "Admin";
                if(admin.Role != null)
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
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,FName,LName,Email,Role")] Admin admin)
        {
            if (id != admin.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (admin.Role == "Top-Level Admin")
                    {
                        IdentityUser user = _userManager.FindByEmailAsync(admin.Email).Result;
                        await _userManager.RemoveFromRoleAsync(user, "Admin");
                        await _userManager.AddToRoleAsync(user, "Top-Level Admin");
                        _context.Update(admin);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Update(admin);
                        await _context.SaveChangesAsync();
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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
            var admin = await _context.Admins.FindAsync(id);
            await _userManager.DeleteAsync(_userManager.FindByEmailAsync(admin.Email).Result);
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.ID == id);
        }
    }
}
