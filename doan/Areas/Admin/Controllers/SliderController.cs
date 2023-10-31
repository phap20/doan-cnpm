using doan.Models;
using doan.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly DataContext _context;
        public SliderController(DataContext context)
        {
            _context = context;
        }

        public IActionResult HienThi()
        {
            var slider = _context.Sliders.ToList();
            return View(slider);    
        }
        public IActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Them(Slider slider)
        {
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Sua(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var slider = _context.Sliders.Find(id);
            return View(slider);
        }
        [HttpPost]
        public IActionResult Sua(Slider slider)
        {
            _context.Sliders.Update(slider);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
        public IActionResult Xoa(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var slider = _context.Sliders.Find(id);
            return View(slider);

        }
        [HttpPost]
        public IActionResult Xoa(Slider slider)
        {
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction("HienThi");
        }
    }
}
