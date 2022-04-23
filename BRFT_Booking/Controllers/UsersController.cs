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
    [Authorize]
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

        /*[HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            //
            string idd = formCollection["ID"];
            string[] ids = idd.Split(new char[] { ',' });
            //var user = User.FindFirst(id);
            //this.db.FName.Remove(user);
            foreach (string id in ids)
            {
                var employee = this.db.Users.Find(int.Parse(id));
                this.db.Users.Remove(employee);
                this.db.SaveChanges();
            }
            return RedirectToAction("Index");
        }*/

        // GET: Users
        [Authorize(Roles = "Admin, Top-Level Admin")]
        public async Task<IActionResult> Index(string[] chkDelete, string SearchName, string SearchNumber, string SearchEmail, int? page, int? pageSizeID, string actionButton, string sortDirection = "asc", string sortField = "User")
        {
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);

            ViewData["Filtering"] = "btn-outline-secondary";

            PopulateDropDownLists();

            string[] sortOptions = new[] { "StudentID" };

            var users = from u in _context.Users
                        .Include(u => u.Bookings)
                        .Include(u => u.ProgramTerm)
                        .ThenInclude(u => u.ProgramDetail)
                        .ThenInclude(u => u.ProgramTermGroups)
                        select u;

            users = users.Where(u => u.Invisible != true);

            if (!String.IsNullOrEmpty(SearchName))
            {
                users = users.Where(u => u.LName.ToUpper().Contains(SearchName.ToUpper())
                                       || u.FName.ToUpper().Contains(SearchName.ToUpper()));
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchNumber))
            {
                users = users.Where(u => u.StudentID.Contains(SearchNumber));
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchEmail))
            {
                users = users.Where(u => u.Email.Contains(SearchEmail));
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


            //string selected = Request.Form["chkDelete"].ToString();
            //string[] selectedList = selected.Split(',');
            int countDelete = 0;
            int failedDelete = 0;
            foreach (var temp in chkDelete)
            {
                int strTemp = Convert.ToInt32(temp);
                var deleteUser = _context.Users.FirstOrDefault(p => p.ID == strTemp);
                var deleteTerm = _context.ProgramTerms.FirstOrDefault(p => p.UserID == strTemp);
                var deleteBooking = _context.Bookings.Where(p => p.UserID == strTemp);
                //var delete = _context.Bookings.AllAsync(p => p.UserID == strTemp);
                try
                {

                    if (deleteTerm != null)
                    {
                        _context.ProgramTerms.Remove(deleteTerm);
                    }

                    foreach (var delete in deleteBooking)
                    {
                        _context.Bookings.Remove(delete);
                    }

                    _context.Users.Remove(deleteUser);

                    _context.SaveChanges();
                    countDelete = countDelete + 1;
                }
                catch
                {
                    failedDelete = failedDelete + 1;
                }

                /*var user = await _context.Users.FindAsync(strTemp);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();*/
            }

            if (failedDelete > 0)
            {
                ModelState.AddModelError("", "Unable to delete " + failedDelete + " users");
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            }
            else if (countDelete > 0)
            {
                ModelState.AddModelError("", "Successfully deleted " + countDelete + " users");
            }
            var pagedData = await PaginatedList<User>.CreateAsync(users.AsNoTracking(), page ?? 1, pageSize);
            //var Users = _context.Users.Where(u => chkDelete.Contains(u.ID));

            return View(pagedData);
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewDataReturnURL();

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.ProgramTerm)
                .ThenInclude(u => u.ProgramDetail)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // GET: Users/Create
        public IActionResult Create()
        {
            ViewDataReturnURL();

            return View();
        }


        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Top-Level Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StudentID,FName,MName,LName,Email,DateOfBirth")] User user, string Password)
        {
            ViewDataReturnURL();

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

                        if (Password == null || Password == "")
                        {
                            if (user.DateOfBirth != null)
                            {
                                Password = ((DateTime)user.DateOfBirth).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                Password = "password";
                            }
                        }

                        IdentityResult result = _userManager.CreateAsync(Iuser, Password).Result;

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
                    ModelState.AddModelError("StudentID", "This ID already exists. Please try a different one!");
                }
                else if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Users.Email"))
                {
                    ModelState.AddModelError("Email", "This email already exists. Please try a different one!");
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
            ViewDataReturnURL();

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

        //GET: Users/EditPersonal
        public async Task<IActionResult> PersonalData(int? id)
        {
            ViewDataReturnURL();

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Email != User.Identity.Name)
            {
                return NotFound();
            }
            ViewData["ID"] = user.ID;
            return View(user);
        }
        // POST: Users/PersonalData/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonalData(int id)
        {
            ViewDataReturnURL();

            var userToUpdate = await _context.Users.FindAsync(id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<User>(userToUpdate, "", u => u.StudentID, u => u.FName,
                u => u.MName, u => u.LName, u => u.Accessibility))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return Redirect(Url.RouteUrl(new { Controller = "Home", Action = "Index" }));
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
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            PopulateDropDownLists(userToUpdate);
            return View(userToUpdate);
        }
        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            ViewDataReturnURL();

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
                        ModelState.AddModelError("StudentID", "This ID already exists. Please try a different one!");
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
            ViewDataReturnURL();

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
            ViewDataReturnURL();
            int strTemp = Convert.ToInt32(id);
            var user = await _context.Users.FindAsync(id);
            var userToDelete = _identityContext.Users.Where(u => u.Email == user.Email).First();
            var deleteTerm = _context.ProgramTerms.FirstOrDefault(p => p.UserID == strTemp);
            var deleteBooking = _context.Bookings.Where(p => p.UserID == strTemp);
            

            try
            {

                if (deleteTerm != null)
                {
                    _context.ProgramTerms.Remove(deleteTerm);
                }

                foreach (var delete in deleteBooking)
                {
                    _context.Bookings.Remove(delete);
                }
                //await _userManager.DeleteAsync(_userManager.FindByEmailAsync(user.Email).Result);
                await _userManager.DeleteAsync(userToDelete);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete. This user still have existing bookings!");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(user);
        }

        [HttpPost]
        // POST: Users/InsertFromExcel
        public async Task<IActionResult> InsertFromExcelUser(IFormFile theExcel)
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

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
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


        public ActionResult DeleteSelected()
        {

            string selected = Request.Form["chkDelete"].ToString();
            string[] selectedList = selected.Split(',');
            foreach (var temp in selectedList)
            {
                int strTemp = Convert.ToInt32(temp);
                var deleteUser = _context.Users.FirstOrDefault(p => p.ID == strTemp);
                _context.Users.Remove(deleteUser);
                _context.SaveChanges();
                /*var user = await _context.Users.FindAsync(strTemp);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();*/
            }
            return RedirectToAction("Index");
        }

    }
}
