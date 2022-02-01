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

namespace BRFT_Booking.Controllers
{
    public class RoomsController : Controller
    {
        private readonly BRFTContext _context;

        public RoomsController(BRFTContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(string SearchArea, string SearchRoom, int? page, int? pageSizeID,
            string actionButton, string sortDirection = "asc", string sortField = "Room")
        {
            ViewData["Filtering"] = "btn-outline-secondary";
            var rooms = from r in _context.Rooms
                        .AsNoTracking()
                        select r;

            string[] sortOptions = new[] { "Area", "Room", "Description", "Limit" };

            if (!String.IsNullOrEmpty(SearchArea))
            {
                rooms = rooms.Where(r => r.Area.ToUpper().Contains(SearchArea.ToUpper()));
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchRoom))
            {
                rooms = rooms.Where(r => r.Name.ToUpper().Contains(SearchRoom.ToUpper()));
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
            if (sortField == "Area")
            {
                if (sortDirection == "asc")
                {
                    rooms = rooms.OrderByDescending(r => r.Area);
                }
                else
                {
                    rooms = rooms.OrderBy(r => r.Area);
                }
            }
            else if (sortField == "Description")
            {
                if (sortDirection == "asc")
                {
                    rooms = rooms.OrderByDescending(r => r.Description);
                }
                else
                {
                    rooms = rooms.OrderBy(r => r.Description);
                }
            }
            else if (sortField == "Limit")
            {
                if (sortDirection == "asc")
                {
                    rooms = rooms.OrderByDescending(r => r.Limit);
                }
                else
                {
                    rooms = rooms.OrderBy(r => r.Limit);
                }
            }
            else
            {
                if (sortDirection == "Room")
                {
                    rooms = rooms.OrderByDescending(r => r.Name);
                }
                else
                {
                    rooms = rooms.OrderBy(r => r.Name);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<Room>.CreateAsync(rooms.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Area,Name,Description,Limit,Enabled")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Area,Name,Description,Limit,Enabled")] Room room)
        {
            if (id != room.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.ID))
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
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}
