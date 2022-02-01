using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRFT_Booking.Data;
using BRFT_Booking.Models;
using BRFT_Booking.Utilities;

namespace BRFT_Booking.Controllers
{
    public class UsersController : Controller
    {
        private readonly BRFTContext _context;

        public UsersController(BRFTContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string SearchPlan, string SearchDescription,
            int? page, int? pageSizeID,
            string actionButton, string sortDirection = "asc", string sortField = "User")
        {
            ViewData["Filtering"] = "btn-outline-secondary";

            string[] sortOptions = new[] { "StudentID", "Full Name", "Start Level", "Term" };

            var users = from u in _context.Users
                        .AsNoTracking()
                        select u;

            if (!String.IsNullOrEmpty(SearchPlan))
            {
                users = users.Where(u => u.AcadPlan.ToUpper().Contains(SearchPlan.ToUpper()));
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchDescription))
            {
                users = users.Where(u => u.Description.ToUpper().Contains(SearchDescription.ToUpper()));
                ViewData["Filtering"] = " show";
            }

            if (!String.IsNullOrEmpty(actionButton))
            {
                page = 1;
                if (actionButton != "Filter")
                {
                    if (actionButton == sortField)
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;
                }
            }
            if (sortField == "StudentID")
            {
                if (sortDirection == "asc")
                {
                    users = users.OrderByDescending(u => u.StudentID);
                }
                else
                {
                    users = users.OrderBy(u => u.StudentID);
                }
            }
            else if (sortField == "Start Level")
            {
                if (sortDirection == "asc")
                {
                    users = users.OrderByDescending(u => u.StrtLevel);
                }
                else
                {
                    users = users.OrderBy(u => u.StrtLevel);
                }
            }
            else if (sortField == "Term")
            {
                if (sortDirection == "asc")
                {
                    users = users.OrderByDescending(u => u.Term);
                }
                else
                {
                    users = users.OrderBy(u => u.Term);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    users = users
                        .OrderBy(u => u.LName)
                        .ThenBy(u => u.FName);
                }
                else
                {
                    users = users
                        .OrderByDescending(u => u.LName)
                        .ThenByDescending(u => u.FName);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<User>.CreateAsync(users.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StudentID,FName,MName,LName,AcadPlan,Description,Email,StrtLevel,LastLevel,Term")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StudentID,FName,MName,LName,AcadPlan,Description,Email,StrtLevel,LastLevel,Term")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
