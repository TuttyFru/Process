using MB.Data;
using MB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MB.Controllers
{
    public class RouteController : Controller
    {
        private readonly DataDbContext _db;

        public RouteController(DataDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Route> objList = _db.Route;

            foreach (var obj in objList)
            {
                obj.Address_departure = _db.Address.FirstOrDefault(u => u.Id == obj.Address_departure_id);
                obj.Address_arrival= _db.Address.FirstOrDefault(u => u.Id == obj.Address_arrival_id);
            }
            return View(objList);
        }

        //GET-CREATE
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            RouteVM routeVM = new RouteVM()
            {
                Route = new Route(),
                TDDAddress_departure = _db.Address.Select(i => new SelectListItem
                {
                    Text = i.City,
                    Value = i.Id.ToString()
                }),
                TDDAddress_arrival = _db.Address.Select(i => new SelectListItem
                {
                    Text = i.City,
                    Value = i.Id.ToString()
                }),
            };

            return View(routeVM);
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create(RouteVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Route.Add(obj.Route);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET-DELETE
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Route.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Route.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Route.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-UPDATE
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Update(int? id)
        {
            RouteVM routeVM = new RouteVM()
            {
                Route = new Route(),
                TDDAddress_departure = _db.Address.Select(i => new SelectListItem
                {
                    Text = i.City,
                    Value = i.Id.ToString()
                }),
                TDDAddress_arrival = _db.Address.Select(i => new SelectListItem
                {
                    Text = i.City,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            routeVM.Route = _db.Route.Find(id);
            if (routeVM.Route == null)
            {
                return NotFound();
            }
            return View(routeVM);
        }

        //POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Update(RouteVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Route.Update(obj.Route);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _db.Route.Include(b => b.Address_departure).Include(c => c.Address_arrival)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (route == null)
            {
                return NotFound();
            }

            return View(route);

        }
    }
}
