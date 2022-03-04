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
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BRTF_Booking.Controllers
{
    [Authorize(Roles = "Admin, Top-Level Admin")]
    public class UsersController : Controller
    {
        private readonly BRTFContext _context;
        private readonly ApplicationDbContext _identityContext;
        private readonly UserManager<IdentityUser> _userManager;


        public UsersController(BRTFContext context, ApplicationDbContext identityContext, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _identityContext = identityContext;
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
                        .Include(u => u.ProgramTerm)
                        .ThenInclude(u => u.ProgramDetail)
                        .ThenInclude(u => u.ProgramTermGroups)
                        .AsNoTracking()
                        select u;

            if (ProgramTermID.HasValue)
            {
                users = users.Where(u => u.StudentID == ProgramTermID.ToString());
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
        public async Task<IActionResult> Create([Bind("ID,StudentID,FName,MName,LName,Email,Password")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    if (_userManager.FindByEmailAsync(user.Email).Result == null)
                    {
                        IdentityUser Iuser = new IdentityUser
                        {
                            UserName = user.Email,
                            Email = user.Email
                        };

                        IdentityResult result = _userManager.CreateAsync(Iuser, user.Password).Result;

                        if (result.Succeeded)
                        {
                            _userManager.AddToRoleAsync(Iuser, "Student").Wait();
                        }
                    }

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
        public async Task<IActionResult> Edit(int id)
        {
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<User>(userToUpdate, "", u => u.StudentID, u => u.FName, 
                u => u.MName, u => u.LName))
            {          
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                    {
                        ModelState.AddModelError("StudentID", "Unable to save changes. Remember, you cannot have duplicate student ID.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }

            PopulateDropDownLists(userToUpdate);
            return View(userToUpdate);
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
            await _userManager.DeleteAsync(_userManager.FindByEmailAsync(user.Email).Result);
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
