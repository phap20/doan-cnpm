using doan.Models;
using Microsoft.AspNetCore.Mvc;
using doan.Admin.Utilities;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class XacThucController : Controller
    {
        private readonly DataContext _context;
        public XacThucController(DataContext context)
        {
            _context = context;
        }

        public IActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangNhap(NhanVien nv)
        {
            string password = Functions.MD5Password(nv.MatKhau);
            var nhanvien = _context.NhanViens.Where(nd1 => nd1.TaiKhoan == nv.TaiKhoan && nd1.MatKhau == password)
                            .FirstOrDefault();

            if (nhanvien == null)
            {
                TempData["msg"] = "Tài khoản hoặc mật khẩu không chính xác.";
                return View();
            }

            Functions._UserAdminID = nhanvien.ID;
            Functions._UserNameAdmin = nhanvien.HoTen ?? "";

            return RedirectToAction("Index", "Home");
        }
    }
}
