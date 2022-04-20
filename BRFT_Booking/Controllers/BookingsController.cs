using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRTF_Booking.Data;
using BRTF_Booking.Models;
using BRTF_Booking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Microsoft.AspNetCore.Http.Features;
using System.IO;
using OfficeOpenXml.Drawing.Chart;
using BRTF_Booking.Utilities;

namespace BRTF_Booking.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly BRTFContext _context;
        private readonly ApplicationDbContext _identityContext;

        public BookingsController(BRTFContext context, ApplicationDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // GET: Bookings
        public async Task<IActionResult> Index(string[] chkDelete, string SearchName, string SearchRoom, string SearchArea, DateTime SearchDate, int? RoomID, int? page, int? pageSizeID, string actionButton, string sortDirection = "asc", string sortField = "Booking")
        {
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);

            ViewData["Filtering"] = "btn-outline-secondary";

            string[] sortOptions = new[] { "User", "Room", "Area", "Date", "Status" };

            var bookings = from b in _context.Bookings
                            .Include(b => b.User)
                            .Include(b => b.Room)
                            .ThenInclude(b => b.Area)
                           select b;

            PopulateDropDownLists();

            if (RoomID.HasValue)
            {
                bookings = bookings.Where(b => b.RoomID == RoomID);
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchName))
            {
                bookings = bookings.Where(b => b.User.LName.ToUpper().Contains(SearchName.ToUpper())
                                       || b.User.FName.ToUpper().Contains(SearchName.ToUpper()));
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchRoom))
            {
                bookings = bookings.Where(b => b.Room.Name.ToUpper().Contains(SearchRoom.ToUpper()));
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchArea))
            {
                bookings = bookings.Where(b => b.Room.Area.Name.ToUpper().Contains(SearchArea.ToUpper()));
                ViewData["Filtering"] = " show";
            }
            if (SearchDate != DateTime.MinValue)
            {
                bookings = bookings.Where(b => b.StartDate.Date == SearchDate.Date);
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
            if (sortField == "Room")
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings.OrderByDescending(b => b.Room);
                }
                else
                {
                    bookings = bookings.OrderBy(b => b.Room);
                }
            }
            else if (sortField == "Date")
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings.OrderByDescending(b => b.StartDate);
                }
                else
                {
                    bookings = bookings.OrderBy(b => b.StartDate);
                }
            }
            else if (sortField == "Status")
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings.OrderByDescending(b => b.Status);
                }
                else
                {
                    bookings = bookings.OrderBy(b => b.Status);
                }
            }
            else if (sortField == "Area")
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings.OrderByDescending(b => b.Room.Area.Name);
                }
                else
                {
                    bookings = bookings.OrderBy(b => b.Room.Area.Name);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings.OrderBy(b => b.User.LName).ThenBy(b => b.User.FName);
                }
                else
                {
                    bookings = bookings.OrderByDescending(b => b.User.LName).ThenByDescending(b => b.User.FName);
                }
            }


            int countDelete = 0;
            int failedDelete = 0;
            foreach (var temp in chkDelete)
            {
                int strTemp = Convert.ToInt32(temp);
                var deleteBooking = _context.Bookings.FirstOrDefault(p => p.ID == strTemp);
                var deleteTerm = _context.ProgramTerms.FirstOrDefault(p => p.UserID == strTemp);
                try
                {
                    _context.Bookings.Remove(deleteBooking);

                    _context.SaveChanges();
                    countDelete = countDelete + 1;
                }
                catch
                {
                    failedDelete = failedDelete + 1;
                }

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



            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<Booking>.CreateAsync(bookings.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [Authorize]
        [HttpGet]
        // GET: Bookings/Create
        public IActionResult CreateRepeat(int? id)
        {
            ViewDataReturnURL();

            if (id != null)
            {
                Booking booking = _context.Bookings
                    .Include(b => b.Room).ThenInclude(b => b.Area)
                    .Include(b => b.User)
                    .Where(b => b.ID == id)
                    .FirstOrDefault();

                booking.StartDate = booking.StartDate.AddDays(7);
                booking.EndDate = booking.EndDate.AddDays(7);
                booking.ID = _context.Bookings.Count() + 1;


                ViewData["UserID"] = new SelectList(_context.Users.Where(u => u.ID == booking.UserID), "ID", "FullName");
                //ViewData["AreaID"] = new SelectList(_context.Areas.Where(a => a.ID == booking.Room.AreaID), "ID", "Name");
                ViewData["RoomID"] = new SelectList(_context.Rooms.Where(r => r.ID == booking.RoomID), "ID", "Name");

                return View(booking);
            }
            else
            {
                PopulateDropDownLists();
                return View();
            }
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRepeat(int? id, [Bind("UserID,RoomID,StartDate,StartTime,EndDate,EndTime,Status,AreaID")] Booking bookings)
        {
            ViewDataReturnURL();

            Booking booking = _context.Bookings.FindAsync(id).Result;
            booking.StartDate = booking.StartDate.AddDays(7);
            booking.EndDate = booking.EndDate.AddDays(7);
            booking.ID = _context.Bookings.Count() + 1;

            try
            {
                booking.BookingRequested = DateTime.Today;
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }


            PopulateDropDownLists();
            return View(booking);
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // GET: Bookings/Create
        public IActionResult Create(int? id)
        {
            ViewDataReturnURL();

            if (id != null)
            {
                Booking booking = _context.Bookings.FindAsync(id).Result;
                booking.UserID = _context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().ID;


                //ViewData["UserID"] = new SelectList(_context.Users, "ID", "FullName");
                ViewData["AreaID"] = new SelectList(_context.Areas, "ID", "Name");
                //ViewData["RoomID"] = new SelectList(_context.Rooms.Where(r => r.Enabled), "ID", "Name");

                return View(booking);
            }
            else
            {
                PopulateDropDownLists();
                return View();
            }
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,RoomID,StartDate,StartTime,EndDate,EndTime,Status")] Booking booking, DateTime[] selectedTimes)
        {
            ViewDataReturnURL();

            try
            {
                if (ModelState.IsValid)
                {

                    //booking.UserID = _context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().ID;
                    booking.BookingRequested = DateTime.Today;
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            PopulateDropDownLists();
            return View(booking);
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewDataReturnURL();

            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Room)
                .ThenInclude(b => b.Area)
                .Where(b => b.ID == id)
                .FirstOrDefaultAsync();
            if (booking == null)
            {
                return NotFound();
            }
            PopulateDropDownLists();
            return View(booking);
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string[] selectedOptions, Byte[] RowVersion)
        {
            ViewDataReturnURL();

            var bookingToUpdate = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bookingToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Booking>(bookingToUpdate, "", b => b.UserID, b => b.RoomID,
                b => b.BookingRequested, b => b.StartDate, b => b.EndDate, b => b.Status))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(bookingToUpdate.ID))
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
            return View(bookingToUpdate);
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewDataReturnURL();

            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewDataReturnURL();

            var booking = await _context.Bookings.FindAsync(id);
            try
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to delete record. Try again, and if the problem persists see your system administrator.");
            }
            return View(booking);
        }


        [Authorize(Roles = "Admin, Top-Level Admin")]
        // GET: Bookings/DownloadBookings
        public IActionResult DownloadBookings(DateTime reportStart, DateTime reportEnd)
        {
            if (reportEnd == DateTime.MinValue)
            {
                reportEnd = DateTime.MaxValue;
            }

            //main page
            var bookings = (from b in _context.Bookings
                         .Include(b => b.User)
                         .Include(b => b.Room)
                         .ThenInclude(p => p.Area)
                         .Where(a => a.StartDate.Date >= reportStart.Date && a.EndDate.Date <= reportEnd.Date)
                            orderby b.BookingRequested ascending
                            select new
                            {
                                User = b.User.FullName,
                                Area = b.Room.Area.Name,
                                Room = b.Room.Name,
                                Date = b.BookingRequested,
                                Start = b.StartDate,
                                End = b.EndDate,
                                Hours = (b.EndDate - b.StartDate).TotalHours,
                                b.Status
                            }).AsNoTracking().ToList();

            //for chart
            var roomBookings = bookings
               .GroupBy(a => new { a.Area, a.Room })
               .Select(grp => new
               {
                   grp.Key.Area,
                   grp.Key.Room,
                   Number_Of_Bookings = grp.Count(),
                   Total_Hours = (int)grp.Sum(a => a.Hours)
               });

           
            int numRows = bookings.Count();

            if (numRows > 0)
            {
                using (ExcelPackage excel = new ExcelPackage())
                {

                    var workSheet = excel.Workbook.Worksheets.Add("Bookings");
                    var workSheetBookings = excel.Workbook.Worksheets.Add("Room Bookings");

                    workSheet.Cells[3, 1].LoadFromCollection(bookings, true);
                    workSheetBookings.Cells[3, 1].LoadFromCollection(roomBookings, true);

                    workSheet.Column(4).Style.Numberformat.Format = "yyyy-mm-dd";

                    workSheet.Column(5).Style.Numberformat.Format = "yyyy-mm-dd hh:mm";

                    workSheet.Column(6).Style.Numberformat.Format = "yyyy-mm-dd hh:mm";



                    //Start Chart

                    var barChart = (ExcelBarChart)workSheetBookings.Drawings.AddChart("Bookings", eChartType.BarClustered);
                    barChart.SetPosition(5, 25, 6, 25);
                    barChart.SetSize(500, 500);
                    int rowcount = workSheetBookings.Dimension.End.Row;

                    barChart.Series.Add("D4:D" + rowcount, "B4:B" + rowcount);
                    barChart.YAxis.Title.Text = "Hours Booked";
                    barChart.XAxis.Title.Text = "Rooms";
                    //barChart.DataLabel.ShowCategory = false;
                    //barChart.DataLabel.ShowPercent = false;
                    barChart.Legend.Remove();


                    using (ExcelRange headings = workSheet.Cells[3, 1, 3, 8])
                    {
                        headings.Style.Font.Bold = true;
                        var fill = headings.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.FromArgb(8, 124, 232));
                    }
                    if (reportStart != DateTime.MinValue)
                    {
                        workSheet.Cells[2, 1].Value = "Filter Start - " + reportStart.ToShortDateString();

                    }
                    
                    if (reportEnd != DateTime.MaxValue)
                    {
                        workSheet.Cells[2, 2].Value = "Filter End - " + reportEnd.ToShortDateString();

                    }
                    
                    

                    workSheet.Cells.AutoFitColumns();
                    workSheetBookings.Cells.AutoFitColumns();

                    workSheet.Cells[1, 1].Value = "Booking Report";
                    workSheetBookings.Cells[1, 1].Value = "Booking Report";

                    
                    


                    using (ExcelRange Rng = workSheet.Cells[1, 1, 1, 8])
                    {
                        Rng.Merge = true;
                        Rng.Style.Font.Bold = true;
                        Rng.Style.Font.Size = 18;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    DateTime utcDate = DateTime.UtcNow;
                    TimeZoneInfo esTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime localDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, esTimeZone);
                    using (ExcelRange Rng = workSheet.Cells[2, 8])
                    {
                        Rng.Value = "Created: " + localDate.ToShortTimeString() + " on " +
                            localDate.ToShortDateString();
                        Rng.Style.Font.Bold = true;
                        Rng.Style.Font.Size = 12;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }

                    var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
                    if (syncIOFeature != null)
                    {
                        syncIOFeature.AllowSynchronousIO = true;
                        using (var memoryStream = new MemoryStream())
                        {
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.Headers["content-disposition"] = "attachment;  filename=Bookings Report " + DateTime.Now.ToString("yyyy/MM/dd") + ".xlsx";
                            excel.SaveAs(memoryStream);
                            memoryStream.WriteTo(Response.Body);
                        }
                    }
                    else
                    {
                        try
                        {
                            Byte[] theData = excel.GetAsByteArray();
                            string filename = "Bookings Report.xlsx";
                            string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            return File(theData, mimeType, filename);
                        }
                        catch (Exception)
                        {
                            return NotFound();
                        }
                    }
                }
            }
            TempData["Message"] = "No bookings found during that time frame!";
            return RedirectToAction(nameof(Index));
            //return NotFound("No Bookings found during Time frame");
        }

        // GET: Bookings/Request
        public new IActionResult Request(int? areaID, string? date)
        {
            ViewDataReturnURL();

            if (areaID != null)
            {
                ViewData["DateClicked"] = DateTime.Today.ToString("yyyy-MM-dd");
                ViewData["AreaID"] = AreaSelectList(areaID);
                ViewData["RoomID"] = new SelectList(_context.Rooms.Where(r => r.AreaID == areaID), "ID", "Name");
                return View();
            }
            else if (date != null)
            {
                ViewData["DateClicked"] = date;
                ViewData["AreaID"] = AreaSelectList();
                return View();
            }
            else
            {
                ViewData["DateClicked"] = DateTime.Today.ToString("yyyy-MM-dd");
                ViewData["AreaID"] = AreaSelectList();
                return View();
            }
        }

        // POST: Bookings/Request
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public new async Task<IActionResult> Request([Bind("ID,RoomID,StartDate,StartTime,EndDate,EndTime")] Booking booking)
        {
            ViewDataReturnURL();

            var settings = _context.SettingsViewModels.First();
            if (ModelState.IsValid)
            {
                if (((Convert.ToInt64(booking.StartDate.ToString("HHmm")) > Convert.ToInt64(DateTime.Parse(settings.OfficeStartHours).ToString("HHmm"))) && (Convert.ToInt64(booking.StartDate.ToString("HHmm")) < Convert.ToInt64(DateTime.Parse(settings.OfficeEndHours).ToString("HHmm")))) && ((Convert.ToInt64(booking.EndDate.ToString("HHmm")) > Convert.ToInt64(DateTime.Parse(settings.OfficeStartHours).ToString("HHmm"))) && (Convert.ToInt64(booking.EndDate.ToString("HHmm")) < Convert.ToInt64(DateTime.Parse(settings.OfficeEndHours).ToString("HHmm")))))
                {
                    if ((booking.StartDate > DateTime.Parse(settings.TermStart)) && (booking.StartDate < DateTime.Parse(settings.TermEnd)) && (booking.EndDate > DateTime.Parse(settings.TermStart)) && (booking.EndDate < DateTime.Parse(settings.TermEnd)))
                    {
                        booking.UserID = _context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().ID;
                        booking.BookingRequested = DateTime.Today;
                        booking.Status = "Accepted";
                        _context.Add(booking);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Calendar));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Booking request outside of current term");
                        PopulateDropDownLists();
                        return View(booking);
                    }
                }
                else
                {
                    ModelState.AddModelError("","Booking request outside of office hours");
                    PopulateDropDownLists();
                    return View(booking);
                }

                
            }
            PopulateDropDownLists();
            return View(booking);
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.ID == id);
        }


        private SelectList AreaSelectList()
        {
            int groupID = _context.Users
                        .Include(u => u.ProgramTerm)
                        .ThenInclude(u => u.ProgramDetail)
                        .ThenInclude(u => u.ProgramTermGroups)
                        .AsNoTracking()
                        .Where(u => u.Email == User.Identity.Name)
                        .FirstOrDefault().ProgramTerm.ID;


            List<Area> areas = new List<Area>();

            foreach (ProgramTermGroupArea pGroupArea in _context.ProgramTermGroupAreas.Include(p => p.Area).Include(p => p.ProgramTermGroup))
            {
                if (pGroupArea.ID == groupID)
                {
                    areas.Add(_context.Areas.FindAsync(pGroupArea.AreaID).Result);
                }
            }
            foreach (Area area in _context.Areas)
            {
                if (!areas.Contains(area) && !_context.ProgramTermGroupAreas.Where(p => p.AreaID == area.ID).Any())
                {
                    areas.Add(area);
                }
            }

            return new SelectList(areas.OrderBy(d => d.Name), "ID", "Name");
        }

        private SelectList AreaSelectList(int? id)
        {
            int groupID = _context.Users
                        .Include(u => u.ProgramTerm)
                        .ThenInclude(u => u.ProgramDetail)
                        .ThenInclude(u => u.ProgramTermGroups)
                        .AsNoTracking()
                        .Where(u => u.Email == User.Identity.Name)
                        .FirstOrDefault().ProgramTerm.ID;



            List<Area> areas = new List<Area>();

            foreach (ProgramTermGroupArea pGroupArea in _context.ProgramTermGroupAreas.Include(p => p.Area).Include(p => p.ProgramTermGroup))
            {
                if (pGroupArea.ID == groupID)
                {
                    areas.Add(_context.Areas.FindAsync(pGroupArea.AreaID).Result);
                }
            }
            foreach (Area area in _context.Areas)
            {
                if (!areas.Contains(area) && !_context.ProgramTermGroupAreas.Where(p => p.AreaID == area.ID).Any())
                {
                    areas.Add(area);
                }
            }

            return new SelectList(areas.OrderBy(d => d.Name), "ID", "Name", id);

        }
        private SelectList RoomCreateSelectList(int? AreaID, int? selectedId)
        {
            var rooms = _context.Rooms.Where(r => r.AreaID == AreaID);

            //Find rooms that the user is within the same program term group
            var query = from c in _context.Rooms
                        .Include(c => c.Area)
                        .Where(p => p.AreaID == AreaID)
                        select c;

            return new SelectList(query.OrderBy(p => p.Name), "ID", "Name", selectedId);
        }

        [HttpGet]
        public JsonResult GetCreateRooms(int? ID)
        {
            return Json(RoomCreateSelectList(ID, null));
        }

        private SelectList RoomCreateRuleSelectList(int? AreaID, int? selectedId)
        {

            //The AreaID has been added so we can filter by it.
            var query = from c in _context.Rooms
                        .Include(c => c.Area)
                        .Where(p => p.AreaID == AreaID)
                        select c;
            return new SelectList(query.OrderBy(p => p.Rule), "ID", "Rule", selectedId);
        }


        [HttpGet]
        public JsonResult GetCreateRules(int? ID)
        {
            return Json(RoomCreateSelectList(ID, null));
        }

        private SelectList RoomEditSelectList(int? BookingID)
        {
            int AreaMatch = _context.Bookings.Include(b => b.Room).ThenInclude(b => b.Area).Where(p => p.ID == BookingID).FirstOrDefault().Room.AreaID;

            //The AreaID has been added so we can filter by it.
            var query = from c in _context.Rooms.Where(p => p.AreaID == AreaMatch)
                        select c;

            return new SelectList(query.OrderBy(p => p.Name), "ID", "Name", BookingID, "Rule");
        }

        [HttpGet]
        public JsonResult GetEditRooms(int? ID)
        {
            return Json(RoomEditSelectList(ID));
        }


        public IActionResult Calendar()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            var viewModel = new EventViewModel();
            var events = new List<EventViewModel>();


            var bookings = _context.Bookings.Include(b => b.User).Include(b => b.Room).Where(b => b.User.Email == User.Identity.Name);
            foreach (Booking booking in bookings)
            {
                string color = "";

                if (booking.Status == "Accepted")
                {
                    color = "#44CF6C";
                }
                else if (booking.Status == "Declined")
                {
                    color = "#D52941";
                }
                else
                {
                    color = "#E2C044";
                }

                events.Add(new EventViewModel()
                {
                    id = booking.ID,
                    title = booking.Room.Name,
                    start = booking.StartDate.ToString("O"),
                    end = booking.EndDate.ToString("O"),
                    url = Url.RouteUrl(new { Action = "Details", Controller = "Bookings" }) + $"/{booking.ID}",
                    borderColor = color,
                    backgroundColor = color,
                    allDay = false
                });
            }
            return Json(events.ToArray());
        }

        [HttpPost]
        public ActionResult PostEvents(string jsonData)
        {
            EventViewModel bookingEvent = JsonConvert.DeserializeObject<EventViewModel>(jsonData);
            return View(nameof(Calendar));
        }


        [HttpGet]
        public JsonResult GetUserEvents(int? userID)
        {
            var viewModel = new EventViewModel();
            var events = new List<EventViewModel>();


            var bookings = _context.Bookings.Include(b => b.User).Include(b => b.Room).Where(b => b.User.ID == userID);
            foreach (Booking booking in bookings)
            {
                string color = "";

                if (booking.Status == "Accepted")
                {
                    color = "#44CF6C";
                }
                else if (booking.Status == "Declined")
                {
                    color = "#D52941";
                }
                else
                {
                    color = "#E2C044";
                }

                events.Add(new EventViewModel()
                {
                    id = booking.ID,
                    title = booking.Room.Name,
                    start = booking.StartDate.ToString(),
                    end = booking.EndDate.ToString(),
                    url = Url.RouteUrl(new { Action = "Details", Controller = "Bookings" }) + $"/{booking.ID}",
                    borderColor = color,
                    backgroundColor = color,
                    allDay = false
                });

            }

            return Json(events.ToArray());
        }

        [HttpGet]
        public JsonResult GetSettings()
        {
            var settings = _context.SettingsViewModels.First();
            settings.OfficeStartHours = DateTime.Parse(settings.OfficeStartHours).ToString("HH:mm");
            settings.OfficeEndHours = DateTime.Parse(settings.OfficeEndHours).ToString("HH:mm");
            settings.TermStart = DateTime.Parse(settings.TermStart).ToString("yyyy-MM-dd");
            settings.TermEnd = DateTime.Parse(settings.TermEnd).ToString("yyyy-MM-dd");

            return Json(settings);
        }

        [HttpGet]
        public JsonResult GetRoomEvents(int? roomID)
        {
            var viewModel = new EventViewModel();
            var events = new List<EventViewModel>();

            var bookings = _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .Where(b => b.Room.ID == roomID);

            foreach (Booking booking in bookings)
            {
                string color = "";

                if (booking.Status == "Accepted")
                {
                    color = "#44CF6C";
                }
                else if (booking.Status == "Declined")
                {
                    color = "#D52941";
                }
                else
                {
                    color = "#E2C044";
                }

                events.Add(new EventViewModel()
                {
                    id = booking.ID,
                    title = booking.User.FullName,
                    start = booking.StartDate.ToString(),
                    end = booking.EndDate.ToString(),
                    url = Url.RouteUrl(new { Action = "Details", Controller = "Bookings" }) + $"/{booking.ID}",
                    borderColor = color,
                    backgroundColor = color,
                    allDay = false
                });

            }
            return Json(events.ToArray());
        }

        public JsonResult GetAreas(string term)
        {
            var result = from a in _context.Areas
                         where a.Name.ToUpper().Contains(term.ToUpper())
                         orderby a.Name
                         select new { value = a.Name };
            return Json(result);
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }

        private void PopulateDropDownLists()
        {
            //ViewData["RoomID"] = new SelectList(_context.Rooms.Where(r => r.Enabled), "ID", "Name");
            ViewData["UserID"] = new SelectList(_context.Users.OrderBy(u => u.FName), "ID", "FullName");
            ViewData["AreaID"] = new SelectList(_context.Areas.OrderBy(u => u.Name), "ID", "Name");
            ViewData["RoomID"] = new SelectList(_context.Rooms.OrderBy(u => u.Name), "ID", "Name");
        }


    }
}
