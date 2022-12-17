using MB.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MB.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MB.Controllers
{
    public class DriverController : Controller
    {
        private readonly DataDbContext _db;

        public DriverController(DataDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Driver> objList = _db.Driver;

            foreach (var obj in objList)
            {
                obj.Car = _db.Car.FirstOrDefault(u => u.Id == obj.Car_id);
            }
            return View(objList);
        }

        //GET-CREATE
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            DriverVM driverVM = new DriverVM()
            {
                Driver = new Driver(),
                TDDCar = _db.Car.Select(i => new SelectListItem
                {
                    Text = i.Model,
                    Value = i.Id.ToString()
                })
            };

            return View(driverVM);
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create(DriverVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Driver.Add(obj.Driver);
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
            var obj = _db.Driver.Find(id);
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
            var obj = _db.Driver.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Driver.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-UPDATE
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Update(int? id)
        {
            DriverVM driverVM = new DriverVM()
            {
                Driver = new Driver(),
                TDDCar = _db.Car.Select(i => new SelectListItem
                {
                    Text = i.Model,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            driverVM.Driver = _db.Driver.Find(id);
            if (driverVM.Driver == null)
            {
                return NotFound();
            }
            return View(driverVM);
        }

        //POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Update(DriverVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Driver.Update(obj.Driver);
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

            var driver = await _db.Driver.Include(b => b.Car)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);

        }
    }
}
