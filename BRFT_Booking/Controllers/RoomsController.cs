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

namespace BRTF_Booking.Controllers
{
    [Authorize]
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
                    rooms = rooms.OrderByDescending(r => r.Area.Name);
                }
                else
                {
                    rooms = rooms.OrderBy(r => r.Area.Name);
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
            else if (sortField == "Room")
            {
                if (sortDirection == "asc")
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

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["AreaID"] = new SelectList(_context.Areas, "ID", "Name");
            return View();
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AreaID,Name,Description,Limit,Enabled")] Room room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(room);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewData["AreaID"] = new SelectList(_context.Areas, "ID", "Name");
            return View(room);
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
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

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var roomToUpdate = await _context.Rooms
                .Include(r => r.Area)
                .FirstOrDefaultAsync(r => r.ID == id);
            if (roomToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Room>(roomToUpdate, "", r => r.AreaID, r => r.Name, 
                r => r.Description, r => r.Limit, r => r.Enabled))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(roomToUpdate.ID))
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
            ViewData["AreaID"] = new SelectList(_context.Areas, "ID", "Name");
            return View(roomToUpdate);
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [Authorize(Roles = "Admin, Top-Level Admin")]
        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            
            try
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to Delete Room. Remember, you cannot delete a Room with existing Bookings.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(room);
        }

        [HttpGet]
        public JsonResult GetRoomRules(int? roomID)
        {
            Room room = _context.Rooms.FindAsync(roomID).Result;

            return Json(room);
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
