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
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;

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
        public async Task<IActionResult> Index(string SearchName, int? ProgramTermID,
            int? page, int? pageSizeID,
            string actionButton, string sortDirection = "asc", string sortField = "User")
        {
            ViewData["Filtering"] = "btn-outline-secondary";

            PopulateDropDownLists();

            string[] sortOptions = new[] { "StudentID" };

            var users = from u in _context.Users
                        .AsNoTracking()
                        select u;

            if (ProgramTermID.HasValue)
            {
                users = users.Where(u => u.StudentID == ProgramTermID);
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchName))
            {
                users = users.Where(u => u.LName.ToUpper().Contains(SearchName.ToUpper())
                                       || u.FName.ToUpper().Contains(SearchName.ToUpper()));
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
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Users.StudentID"))
                {
                    ModelState.AddModelError("StudentID", "Unable to save changes. Remember, you cannot have duplicate Student ID numbers.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
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

        [HttpPost]
        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
        {
            //TODO Add Error Handling to the files. Prevent duplication
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
                    StudentID = Int32.Parse(workSheet.Cells[row, 1].Text),
                    FName = workSheet.Cells[row, 2].Text,
                    MName = workSheet.Cells[row, 3].Text,
                    LName = workSheet.Cells[row, 4].Text,
                    Email = workSheet.Cells[row, 7].Text,
                };
                ProgramTerm p = new ProgramTerm
                {
                    StudentID = Int32.Parse(workSheet.Cells[row, 1].Text), //studentID
                    AcadPlan = workSheet.Cells[row, 5].Text,
                    Description = workSheet.Cells[row, 6].Text,
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

            foreach (var p in userPrograms)
            {
                p.UserID = _context.Users.Where(u => u.StudentID == p.StudentID).FirstOrDefault().ID;
            }
            _context.ProgramTerms.AddRange(userPrograms);
            _context.SaveChanges();
            return RedirectToAction("Index", "Users");
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private SelectList AcadPlanList(int? selectedId)
        {
            return new SelectList(_context.ProgramTerms
                .OrderBy(u => u.AcadPlan), "ID", "AcadPlan", selectedId);
        }

        private void PopulateDropDownLists(User users = null)
        {
            ViewData["ProgramTermID"] = AcadPlanList(users?.ID);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }

        
    }
}
