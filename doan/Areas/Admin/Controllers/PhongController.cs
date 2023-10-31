using doan.Models;
using doan.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhongController : Controller
    {
        private readonly DataContext _context;
        public PhongController(DataContext context)
        {
            _context = context;
        }

        public IActionResult HienThi()
        {
            var phong = _context.Phongs.ToList();
            return View(phong);    
        }
        public IActionResult Them()
        {
            var lplist = (from m in _context.LoaiPhongs
                          select new SelectListItem()
                          {
                              Text = m.TenLoaiPhong,
                              Value = m.ID.ToString()
                          }).ToList();
            lplist.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });

            ViewBag.lplist = lplist;

            var tanglist = (from m in _context.Tangs
                          select new SelectListItem()
                          {
                              Text = m.TenTang,
                              Value = m.ID.ToString()
                          }).ToList();
            tanglist.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });

            ViewBag.tanglist = tanglist;

            return View();
        }
        [HttpPost]
        public IActionResult Them(Phong phong)
        {
            _context.Phongs.Add(phong);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Sua(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var lplist = (from m in _context.LoaiPhongs
                          select new SelectListItem()
                          {
                              Text = m.TenLoaiPhong,
                              Value = m.ID.ToString()
                          }).ToList();
            lplist.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });

            ViewBag.lplist = lplist;

            var tanglist = (from m in _context.Tangs
                            select new SelectListItem()
                            {
                                Text = m.TenTang,
                                Value = m.ID.ToString()
                            }).ToList();
            tanglist.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });

            ViewBag.tanglist = tanglist;

            var tang = _context.Phongs.Find(id);
            return View(tang);
        }
        [HttpPost]
        public IActionResult Sua(Phong phong)
        {
            _context.Phongs.Update(phong);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Xoa(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var tang = _context.Phongs.Find(id);
            return View(tang);

        }
        [HttpPost]
        public IActionResult Xoa(Phong phong)
        {
            _context.Phongs.Remove(phong);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
    }
}
