using doan.Models;
using doan.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiPhongController : Controller
    {
        private readonly DataContext _context;
        public LoaiPhongController(DataContext context)
        {
            _context = context;
        }

        public IActionResult HienThi()
        {
            var nd = _context.LoaiPhongs.ToList();
            return View(nd);    
        }
        public IActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Them(LoaiPhong lp)
        {
            _context.LoaiPhongs.Add(lp);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Sua(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var nd = _context.LoaiPhongs.Find(id);
            return View(nd);
        }
        [HttpPost]
        public IActionResult Sua(LoaiPhong lp)
        {
            _context.LoaiPhongs.Update(lp);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Xoa(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var nd = _context.LoaiPhongs.Find(id);
            return View(nd);

        }
        [HttpPost]
        public IActionResult Xoa(LoaiPhong lp)
        {
            _context.LoaiPhongs.Remove(lp);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
    }
}
