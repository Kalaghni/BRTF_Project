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
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;

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
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(programTerm);
        }

        [HttpPost]
        // POST: Users/InsertFromExcel
        public async Task<IActionResult> InsertFromExcelProgTerm(IFormFile theExcel)
        {
            //Make sure file is uploaded
            if (theExcel == null)
            {
                TempData["Message"] = "Please select an Excel file to upload. File type must be .csv or .xlsx!";
                return RedirectToAction(nameof(Index));
            }

            string uploadMessage = "";
            int i = 0;//Counter for inserted records
            int j = 0;//Counter for duplicates

            try
            {
                ExcelPackage excel;
                using (var memoryStream = new MemoryStream())
                {
                    await theExcel.CopyToAsync(memoryStream);
                    excel = new ExcelPackage(memoryStream);
                }
                var workSheet = excel.Workbook.Worksheets[0];
                var start = workSheet.Dimension.Start;
                var end = workSheet.Dimension.End;

                //Start a new list to hold imported objects
                List<User> userList = new List<User>();
                List<ProgramTerm> userPrograms = new List<ProgramTerm>();

                for (int row = start.Row + 1; row <= end.Row; row++)
                {
                    // Row by row...
                    User u = new User
                    {
                        StudentID = workSheet.Cells[row, 1].Text,
                        FName = workSheet.Cells[row, 2].Text,
                        MName = workSheet.Cells[row, 3].Text,
                        LName = workSheet.Cells[row, 4].Text,
                        Email = workSheet.Cells[row, 7].Text,
                    };
                    ProgramTerm p = new ProgramTerm
                    {
                        User = _context.Users.Where(u => u.StudentID == workSheet.Cells[row, 1].Text).FirstOrDefault(), //studentID
                        AcadPlan = workSheet.Cells[row, 5].Text,
                        ProgramDetail = _context.ProgramDetails.Where(p => p.Name == workSheet.Cells[row, 6].Text).FirstOrDefault(),
                        StrtLevel = Int32.Parse(workSheet.Cells[row, 8].Text),
                        LastLevel = workSheet.Cells[row, 9].Text,
                        Term = Int32.Parse(workSheet.Cells[row, 10].Text),
                    };
                    userList.Add(u);
                    userPrograms.Add(p);
                }
                _context.Users.AddRange(userList);
                //_context.ProgramTerms.AddRange(userPrograms);
                _context.SaveChanges();

                _context.ProgramTerms.AddRange(userPrograms);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
                uploadMessage = "Failed to import data.  Check that file type is .csv or .xlsx!";
            }
            if (String.IsNullOrEmpty(uploadMessage))
            {
                uploadMessage = "Imported " + (i + j).ToString() + " records, with "
                    + j.ToString() + " rejected as duplicates and " + i.ToString() + " inserted.";
            }
            TempData["Message"] = uploadMessage;
            return RedirectToAction(nameof(Index));
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
