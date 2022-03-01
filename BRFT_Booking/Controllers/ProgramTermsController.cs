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
        public async Task<IActionResult> Index(string SearchDescription, int? page, int? pageSizeID,
            string actionButton, string sortDirection = "asc", string sortField = "ProgramTerms")
        {
            ViewData["Filtering"] = "btn-outline-secondary";

            var terms = _context.ProgramTerms
                .Include(t => t.User)
                .AsNoTracking();

            string[] sortOptions = new[] { "Academic Plan", "Description" };

            if (!String.IsNullOrEmpty(SearchDescription))
            {
                terms = terms.Where(t => t.Description.ToUpper().Contains(SearchDescription.ToUpper()));
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
            else if (sortField == "Description")
            {
                if (sortDirection == "asc")
                {
                    terms = terms.OrderByDescending(t => t.Description);
                }
                else
                {
                    terms = terms.OrderBy(t => t.Description);
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
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            return View();
        }

        // POST: ProgramTerms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,StudentID,AcadPlan,Description,StrtLevel,LastLevel,Term")] ProgramTerm programTerm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programTerm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", programTerm.UserID);
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
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", programTerm.UserID);
            return View(programTerm);
        }

        // POST: ProgramTerms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,StudentID,AcadPlan,Description,StrtLevel,LastLevel,Term")] ProgramTerm programTerm)
        {
            if (id != programTerm.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programTerm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramTermExists(programTerm.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", programTerm.UserID);
            return View(programTerm);
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
            _context.ProgramTerms.Remove(programTerm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
