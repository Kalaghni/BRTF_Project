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
        public async Task<IActionResult> Index()
        {
            var bRFTContext = _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .ThenInclude(b => b.Area);

            return View(await bRFTContext.ToListAsync());
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

        [Authorize(Roles = "Admin, Top-Level Admin")]
        [HttpGet]
        // GET: Bookings/Create
        public IActionResult CreateRepeat(int? id)
        {
            if (id != null)
            {
                Booking booking = _context.Bookings
                    .Include(b => b.Room).ThenInclude(b => b.Area)
                    .Include(b => b.User)
                    .Where(b => b.ID == id)
                    .FirstOrDefault();

                booking.StartDate = booking.StartDate.AddDays(7);
                booking.EndDate = booking.EndDate.AddDays(7);


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

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRepeat([Bind("ID,UserID,RoomID,StartDate,EndDate,Status")] Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
        // GET: Bookings/Create
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                Booking booking = _context.Bookings.FindAsync(id).Result;
                booking.StartDate = booking.StartDate.AddDays(7);
                booking.EndDate = booking.EndDate.AddDays(7);

                
                ViewData["UserID"] = new SelectList(_context.Users, "ID", "FullName");
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
        public async Task<IActionResult> Create([Bind("ID,UserID,RoomID,StartDate,EndDate,Status")] Booking booking, DateTime[] selectedTimes)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        public IActionResult DownloadBookings()//DateTime start, DateTime end)
        {

            //main page
            var bookings = (from b in _context.Bookings
                         .Include(b => b.User)
                         .Include(b => b.Room)
                         .ThenInclude(p => p.Area)
                                //.Where(a => a.StartDate >= start && a.EndDate < end)
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
                    //barChart.WorkSheet.Calculate("=SUM(9,4,G4:G10)");
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
            return NotFound();
        }

        // GET: Bookings/Request
        public IActionResult Request()
        {
            ViewData["AreaID"] = AreaSelectList();
            return View();
        }

        // POST: Bookings/Request
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Request([Bind("ID,RoomID,StartDate,EndDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.UserID = _context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().ID;
                booking.BookingRequested = DateTime.Today;
                booking.Status = "Awaiting";
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Calendar));
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
                    Console.WriteLine(_context.Areas.FindAsync(pGroupArea.AreaID).Result.Name);
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

            return new SelectList(query.OrderBy(p => p.Name), "ID", "Name", BookingID,"Rule");
        }

        [HttpGet]
        public JsonResult GetEditRooms(int? ID)
        {
            return Json(RoomEditSelectList(ID));
        }


        public async Task<IActionResult> Calendar()
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

                if(booking.Status == "Accepted")
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
                    url = Url.RouteUrl(new { Action = "Details", Controller = "Bookings"}) + $"/{booking.ID}",
                    borderColor = color,
                    backgroundColor = color,
                    allDay = false
                }) ;
            }
            return Json(events.ToArray());
        }

        [HttpPost]
        public ActionResult PostEvents(string jsonData)
        {
            Console.WriteLine(jsonData);
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

        private void PopulateDropDownLists()
        {
            //ViewData["RoomID"] = new SelectList(_context.Rooms.Where(r => r.Enabled), "ID", "Name");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "FullName");
            ViewData["AreaID"] = new SelectList(_context.Areas, "ID", "Name");
        }
    }
}
