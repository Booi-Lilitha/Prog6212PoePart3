using ContractMonthlyClaims.Data;
using ContractMonthlyClaims.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaims.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> Lecturer()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null) return RedirectToAction("Login", "Account");

            var claims = await _context.Claims
                .Include(c => c.ClaimItems)
                .Include(c => c.Documents)
                .Where(c => c.LecturerUsername == username)
                .OrderByDescending(c => c.SubmitDate)
                .ToListAsync();

            return View(claims);
        }

        [HttpGet]
        public IActionResult CreateClaim()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null) return RedirectToAction("Login", "Account");

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return RedirectToAction("Login", "Account");

            ViewBag.UserFullName = $"{user.FirstName} {user.LastName}";
            ViewBag.HourlyRate = user.HourlyRate;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClaim(Claim claim, List<ClaimItem> claimItems, IFormFileCollection documents)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null) return RedirectToAction("Login", "Account");

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return RedirectToAction("Login", "Account");

            // Validate total hours
            decimal totalHours = claimItems.Sum(i => i.Hours);
            if (totalHours > 180)
            {
                ModelState.AddModelError("", "Total hours cannot exceed 180 per month.");
                ViewBag.UserFullName = $"{user.FirstName} {user.LastName}";
                ViewBag.HourlyRate = user.HourlyRate;
                return View(claim);
            }

            claim.LecturerUsername = username;
            claim.UserId = user.Id;
            claim.Hours = totalHours;

            // compute item amounts and claim amount
            foreach (var item in claimItems)
            {
                item.Rate = user.HourlyRate;
                item.Amount = item.Hours * item.Rate;
            }
            claim.Amount = claimItems.Sum(i => i.Amount);
            claim.ClaimItems = claimItems;

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            // save documents
            if (documents != null && documents.Count > 0)
            {
                var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadFolder);

                foreach (var file in documents)
                {
                    if (file.Length <= 0) continue;
                    var unique = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var path = Path.Combine(uploadFolder, unique);
                    using var fs = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(fs);

                    _context.Documents.Add(new Document
                    {
                        ClaimId = claim.Id,
                        FileName = file.FileName,
                        StoredFileName = unique,
                        ContentType = file.ContentType
                    });
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Lecturer");
        }

        public async Task<IActionResult> Coordinator()
        {
            if (HttpContext.Session.GetString("Role") != Role.Coordinator.ToString()) return Forbid();

            var claims = await _context.Claims
                .Include(c => c.ClaimItems)
                .Include(c => c.Documents)
                .Where(c => c.Status == ClaimStatus.Submitted)
                .OrderBy(c => c.SubmitDate)
                .ToListAsync();

            return View(claims);
        }

        public async Task<IActionResult> Manager()
        {
            if (HttpContext.Session.GetString("Role") != Role.Manager.ToString()) return Forbid();

            var claims = await _context.Claims
                .Include(c => c.ClaimItems)
                .Include(c => c.Documents)
                .Where(c => c.Status == ClaimStatus.ApprovedByCoordinator)
                .OrderBy(c => c.SubmitDate)
                .ToListAsync();

            return View(claims);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int id, string role)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            if (role == "coordinator" && HttpContext.Session.GetString("Role") == Role.Coordinator.ToString())
            {
                claim.Status = ClaimStatus.ApprovedByCoordinator;
            }
            else if (role == "manager" && HttpContext.Session.GetString("Role") == Role.Manager.ToString())
            {
                claim.Status = ClaimStatus.ApprovedByManager;
            }
            else return Forbid();

            await _context.SaveChangesAsync();
            return RedirectToAction(role == "coordinator" ? "Coordinator" : "Manager");
        }

        [HttpPost]
        public async Task<IActionResult> RejectClaim(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Rejected;
            await _context.SaveChangesAsync();

            return RedirectToAction("Coordinator");
        }
    }
}
