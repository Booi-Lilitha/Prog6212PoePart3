using ContractMonthlyClaims.Data;
using ContractMonthlyClaims.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaims.Controllers
{
    public class HRController : Controller
    {
        private readonly AppDbContext _db;
        public HRController(AppDbContext db) => _db = db;

        private bool IsHR() => HttpContext.Session.GetString("Role") == Role.HR.ToString();

        public IActionResult Index()
        {
            if (!IsHR()) return Forbid();
            return View(_db.Users.OrderBy(u => u.Username).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsHR()) return Forbid();
            return View(new User());
        }

        [HttpPost]
        public IActionResult Create(User model)
        {
            if (!IsHR()) return Forbid();
            if (!ModelState.IsValid) return View(model);

            if (_db.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError(nameof(model.Username), "Username already exists");
                return View(model);
            }

            _db.Users.Add(model);
            _db.SaveChanges();

            TempData["success"] = "User created successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsHR()) return Forbid();
            var u = _db.Users.Find(id);
            if (u == null) return NotFound();
            return View(u);
        }

        [HttpPost]
        public IActionResult Edit(User model)
        {
            if (!IsHR()) return Forbid();
            if (!ModelState.IsValid) return View(model);

            _db.Users.Update(model);
            _db.SaveChanges();
            TempData["success"] = "User updated";
            return RedirectToAction("Index");
        }
    }
}
