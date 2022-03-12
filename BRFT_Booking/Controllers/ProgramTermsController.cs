using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRTF_Booking.Data;
using BRTF_Booking.Models;
using BRTF_Booking.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace BRTF_Booking.Controllers
{
    [Authorize(Roles = "Admin, Top-Level Admin")]
    public class ProgramTermsController : Controller
    {
        private readonly BRTFContext _context;

        public ProgramTermsController(BRTFContext context)
        {
            _context = context;
        }

        // GET: ProgramTerms
        public async Task<IActionResult> Index(string SearchProgram, int? page, int? pageSizeID, string actionButton, string sortDirection = "asc", string sortField = "ProgramTerms")
        {
            ViewData["Filtering"] = "btn-outline-secondary";

            var terms = _context.ProgramTerms
                .Include(p => p.User)
                .Include(p => p.ProgramDetail)
                .AsNoTracking();

            string[] sortOptions = new[] { "Academic Plan", "Program" };

            if (!String.IsNullOrEmpty(SearchProgram))
            {
                terms = terms.Where(t => t.ProgramDetail.Name.ToUpper().Contains(SearchProgram.ToUpper()));
                ViewData["Filtering"] = " show";
            }

            if (!String.IsNullOrEmpty(actionButton))
            {
                if (actionButton != "Filter")
                {
                    if (actionButton == sortField)
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;
                }
            }
            if (sortField == "Academic Plan")
            {
                if (sortDirection == "asc")
                {
                    terms = terms.OrderByDescending(t => t.AcadPlan);
                }
                else
                {
                    terms = terms.OrderBy(t => t.AcadPlan);
                }
            }
            else if (sortField == "Program")
            {
                if (sortDirection == "asc")
                {
                    terms = terms.OrderByDescending(t => t.ProgramDetail.Name);
                }
                else
                {
                    terms = terms.OrderBy(t => t.ProgramDetail.Name);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<ProgramTerm>.CreateAsync(terms.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: ProgramTerms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programTerm = await _context.ProgramTerms
                .Include(p => p.User)
                .Include(p => p.ProgramDetail)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (programTerm == null)
            {
                return NotFound();
            }

            return View(programTerm);
        }

        // GET: ProgramTerms/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: ProgramTerms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AcadPlan,ProgramDetailID,UserID,StrtLevel,LastLevel,Term")] ProgramTerm programTerm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userToUpdate = _context.Users
                        .Where(u => u.ID == programTerm.UserID)
                        .FirstOrDefault();
                    userToUpdate.ProgramTermID = programTerm.ID;

                    _context.Add(programTerm);
                    _context.Update(userToUpdate);
                    await _context.SaveChangesAsync();
                    return Redirect(Url.RouteUrl(new { Controller = "Users", Action = "Details" }) + $"/{userToUpdate.ID}");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            
            PopulateDropDownLists();
            return View(programTerm);
        }

        // GET: ProgramTerms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programTerm = await _context.ProgramTerms.FindAsync(id);
            if (programTerm == null)
            {
                return NotFound();
            }

            PopulateDropDownLists();
            return View(programTerm);
        }

        // POST: ProgramTerms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var programTermToUpdate = await _context.ProgramTerms
                .Include(p => p.ProgramDetail)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (programTermToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<ProgramTerm>(programTermToUpdate, "", p => p.AcadPlan,
                p => p.ProgramDetailID, p => p.UserID, p => p.StrtLevel, p => p.LastLevel, p => p.Term))
            {
                try
                {
                    var userToUpdate = _context.Users.Where(u => u.ID == programTermToUpdate.UserID).FirstOrDefault();
                    userToUpdate.ProgramTermID = programTermToUpdate.ID;
                    _context.Update(userToUpdate);

                    await _context.SaveChangesAsync();
                    return Redirect(Url.RouteUrl(new { Controller = "Users", Action = "Details" }) + $"/{programTermToUpdate.UserID}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramTermExists(programTermToUpdate.ID))
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
            PopulateDropDownLists();
            return View(programTermToUpdate);
        }

        // GET: ProgramTerms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programTerm = await _context.ProgramTerms
                .Include(p => p.User)
                .Include(p => p.ProgramDetail)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (programTerm == null)
            {
                return NotFound();
            }

            return View(programTerm);
        }

        // POST: ProgramTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programTerm = await _context.ProgramTerms.FindAsync(id);

            try
            {
                _context.ProgramTerms.Remove(programTerm);
                await _context.SaveChangesAsync();
                return Redirect(Url.RouteUrl(new { Controller = "Users", Action = "Details" }) + $"/{programTerm.UserID}");
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to Delete ProgramTerm. Remember, you cannot delete a Program with existing Users.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(programTerm);
        }

        private void PopulateDropDownLists()
        {
            ViewData["UserID"] = new SelectList(_context.Users.Where(u => u.ProgramTerm == null), "ID", "FullName");
            ViewData["AcadPlan"] = new SelectList(_context.ProgramTerms, "AcadPlan", "AcadPlan");
            ViewData["ProgramDetailID"] = new SelectList(_context.ProgramDetails, "ID", "Name");
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private bool ProgramTermExists(int id)
        {
            return _context.ProgramTerms.Any(e => e.ID == id);
        }
    }
}
