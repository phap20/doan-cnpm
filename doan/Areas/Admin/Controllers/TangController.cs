using doan.Models;
using doan.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TangController : Controller
    {
        private readonly DataContext _context;
        public TangController(DataContext context)
        {
            _context = context;
        }

        public IActionResult HienThi()
        {
            var tang = _context.Tangs.ToList();
            return View(tang);    
        }
        public IActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Them(Tang tang)
        {
            _context.Tangs.Add(tang);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Sua(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var tang = _context.Tangs.Find(id);
            return View(tang);
        }
        [HttpPost]
        public IActionResult Sua(Tang tang)
        {
            _context.Tangs.Update(tang);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Xoa(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var tang = _context.Tangs.Find(id);
            return View(tang);

        }
        [HttpPost]
        public IActionResult Xoa(Tang tang)
        {
            _context.Tangs.Remove(tang);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
    }
}
