using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRTF_Booking.Data;
using BRTF_Booking.Models;

namespace BRTF_Booking.Controllers
{
    public class BookingsController : Controller
    {
        private readonly BRTFContext _context;

        public BookingsController(BRTFContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bRFTContext = _context.Bookings.Include(b => b.User).Include(b => b.Room).ThenInclude(b => b.Area);
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
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,RoomID,BookingRequested,StartTime,EndTime")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDownLists();
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.Include(b => b.Room).ThenInclude(b => b.Area).Where(b => b.ID == id).FirstOrDefaultAsync();
            if (booking == null)
            {
                return NotFound();
            }
            PopulateDropDownLists();
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,RoomID,BookingRequested,StartTime,EndTime")] Booking booking)
        {
            if (id != booking.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.ID))
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
            PopulateDropDownLists();
            return View(booking);
        }

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

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.ID == id);
        }

        private SelectList AreaSelectList(int? selectedId)
        {
            return new SelectList(_context.Areas
                .OrderBy(d => d.Name), "ID", "Name", selectedId);
        }


        private SelectList RoomCreateSelectList(int? AreaID, int? selectedId)
        {

            //The AreaID has been added so we can filter by it.
            var query = from c in _context.Rooms.Include(c => c.Area).Where(p => p.AreaID == AreaID)
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

        private void PopulateDropDownLists()
        {
            //ViewData["RoomID"] = new SelectList(_context.Rooms.Where(r => r.Enabled), "ID", "Name");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            ViewData["AreaID"] = new SelectList(_context.Areas, "ID", "Name");
        }

    }
}
