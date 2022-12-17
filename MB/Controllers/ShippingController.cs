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
    public class ShippingController : Controller
    {
        private readonly DataDbContext _db;

        public ShippingController(DataDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Shipping> objList = _db.Shipping;

            foreach (var obj in objList)
            {
                obj.Route = _db.Route.FirstOrDefault(u => u.Id == obj.Route_id);
                obj.Driver = _db.Driver.FirstOrDefault(u => u.Id == obj.Driver_id);
                obj.Cargo = _db.Cargo.FirstOrDefault(u => u.Id == obj.Cargo_id);
            }
            return View(objList);
        }

        //GET-CREATE
        public IActionResult Create()
        {
            ShippingVM shippingVM = new ShippingVM()
            {
                Shipping = new Shipping(), 
                TDDRoute = _db.Route.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                TDDDriver = _db.Driver.Select(i => new SelectListItem
                {
                    Text = i.Full_name,
                    Value = i.Id.ToString()
                }),
                TDDCargo = _db.Cargo.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            return View(shippingVM);
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ShippingVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Shipping.Add(obj.Shipping);
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
            var obj = _db.Shipping.Find(id);
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
            var obj = _db.Shipping.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Shipping.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-UPDATE
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Update(int? id)
        {
            ShippingVM shippingVM = new ShippingVM()
            {
                Shipping = new Shipping(),
                TDDRoute = _db.Route.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                TDDDriver = _db.Driver.Select(i => new SelectListItem
                {
                    Text = i.Full_name,
                    Value = i.Id.ToString()
                }),
                TDDCargo = _db.Cargo.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            shippingVM.Shipping = _db.Shipping.Find(id);
            if (shippingVM.Shipping == null)
            {
                return NotFound();
            }
            return View(shippingVM);
        }

        //POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public IActionResult Update(ShippingVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Shipping.Update(obj.Shipping);
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

            var shipping = await _db.Shipping.Include(b => b.Route).Include(c => c.Driver).Include(d => d.Cargo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);

        }
    }
}
