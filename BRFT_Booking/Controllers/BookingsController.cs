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
        // GET: Bookings/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
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

        // GET: Bookings/Request
        public IActionResult Request()
        {
            PopulateDropDownLists();
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


        private SelectList AreaSelectList(int? selectedId)
        {
            return new SelectList(_context.Areas.OrderBy(d => d.Name), "ID", "Name", selectedId);
        }


        private SelectList RoomCreateSelectList(int? AreaID, int? selectedId)
        {

            //The AreaID has been added so we can filter by it.
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

        private SelectList RoomEditSelectList(int? BookingID)
        {
            int AreaMatch = _context.Bookings.Include(b => b.Room).ThenInclude(b => b.Area).Where(p => p.ID == BookingID).FirstOrDefault().Room.AreaID;

            //The AreaID has been added so we can filter by it.
            var query = from c in _context.Rooms.Where(p => p.AreaID == AreaMatch)
                        select c;

            return new SelectList(query.OrderBy(p => p.Name), "ID", "Name", BookingID);
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
