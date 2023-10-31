using doan.Models;
using doan.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        private readonly DataContext _context;
        public NhanVienController(DataContext context)
        {
            _context = context;
        }

        public IActionResult HienThi()
        {
            var nv = _context.NhanViens.ToList();
            return View(nv);
        }
        public IActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Them(NhanVien nv)
        {
            nv.MatKhau = Functions.MD5Password(nv.MatKhau ?? "");
            _context.NhanViens.Add(nv);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Sua(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var nv = _context.NhanViens.Find(id);
            return View(nv);
        }
        [HttpPost]
        public IActionResult Sua(NhanVien nv)
        {
            nv.MatKhau = Functions.MD5Password(nv.MatKhau ?? "");
            _context.NhanViens.Update(nv);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Xoa(int id)
        {
            if (id == null || id == 0 || id == 1)
            {
                return NotFound();
            }
            var nd = _context.NhanViens.Find(id);
            return View(nd);

        }
        [HttpPost]
        public IActionResult Xoa(NhanVien nv)
        {
            if (nv.ID == 1)
            {
                return NotFound();
            }
            _context.NhanViens.Remove(nv);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
    }
}
