using doan.Models;
using doan.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DichVuController : Controller
    {
        private readonly DataContext _context;
        public DichVuController(DataContext context)
        {
            _context = context;
        }

        public IActionResult HienThi()
        {
            var nd = _context.DichVus.ToList();
            return View(nd);    
        }
        public IActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Them(DichVu dv)
        {
            _context.DichVus.Add(dv);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Sua(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var nd = _context.DichVus.Find(id);
            return View(nd);
        }
        [HttpPost]
        public IActionResult Sua(DichVu dv)
        {
            _context.DichVus.Update(dv);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Xoa(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var nd = _context.DichVus.Find(id);
            return View(nd);

        }
        [HttpPost]
        public IActionResult Xoa(DichVu dv)
        {
            _context.DichVus.Remove(dv);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
    }
}
