using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Areas.Admin.Components
{
    [ViewComponent(Name = "AboutUsIndex")]
    public class AboutUsIndexComponent : ViewComponent
    {
        private readonly DataContext _context;

        public AboutUsIndexComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.TongSoPhong = _context.Phongs.Where(p => p.TrangThai == true).Count();
            ViewBag.TongSoNhanVien = _context.NhanViens.Count();
            ViewBag.TongSoKhachHang = _context.NguoiDungs.Count();

            return await Task.FromResult((IViewComponentResult)View("Default"));
        }
    }
}
