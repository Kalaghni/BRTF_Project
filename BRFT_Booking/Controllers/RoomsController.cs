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

namespace BRTF_Booking.Controllers
{
    public class RoomsController : Controller
    {
        private readonly BRTFContext _context;

        public RoomsController(BRTFContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(string SearchRoom, int? AreaID, int? page, int? pageSizeID,
            string? enabled, string actionButton, string sortDirection = "asc", string sortField = "Room")
        {
            ViewData["Filtering"] = "btn-outline-secondary";

            PopulateDropDownLists();

            var rooms = _context.Rooms
                        .Include(a => a.Area)
                        .AsNoTracking();

            string[] sortOptions = new[] { "Area", "Room", "Description", "Limit" };

            if (AreaID.HasValue)
            {
                rooms = rooms.Where(r => r.AreaID == AreaID);
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchRoom))
            {
                rooms = rooms.Where(r => r.Name.ToUpper().Contains(SearchRoom.ToUpper()));
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(enabled))
            {
                if (enabled == "enabled")
                {
                    rooms = rooms.Where(r => r.Enabled == true);
                    ViewData["Filtering"] = " show";
                }
                else if (enabled == "disabled")
                {
                    rooms = rooms.Where(r => r.Enabled == false);
                    ViewData["Filtering"] = " show";
                }
                else
                {
                    rooms = rooms.Where(r => r.Enabled == false || r.Enabled == true );
                }
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
            else if (sortField == "Enabled")
            {
                if (sortDirection == "asc")
                {
                    rooms = rooms.OrderByDescending(r => r.Enabled);
                }
                else
                {
                    rooms = rooms.OrderBy(r => r.Enabled);
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
                .Include(a => a.Area)
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
            ViewData["AreaID"] = new SelectList(_context.Areas, "ID", "Name");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AreaID,Name,Description,Limit,Enabled")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaID"] = new SelectList(_context.Areas, "ID", "Name");
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
            ViewData["AreaID"] = new SelectList(_context.Areas, "ID", "Name");
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AreaID,Name,Description,Limit,Enabled")] Room room)
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
            ViewData["AreaID"] = new SelectList(_context.Areas, "ID", "Name");
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

        private SelectList AreaSelectList(int? selectedId)
        {
            return new SelectList(_context.Areas
                .OrderBy(d => d.Name), "ID", "Name", selectedId);
        }

        private void PopulateDropDownLists(Room rooms = null)
        {
            ViewData["AreaID"] = AreaSelectList(rooms?.AreaID);
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}
