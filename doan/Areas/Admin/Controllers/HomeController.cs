using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using doan.Models;
using doan.Admin.Utilities;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (!Functions.IsLogin())
            {
                return RedirectToAction("DangNhap");
            }

            var dsHoaDon = _context.HoaDonDPs.OrderByDescending(hd => hd.ID).ToList();

            ViewBag.DoanhThuHomNay = _context.DatPhongs
                                        .Where(dp => dp.TrangThai == 1 && dp.NgayKetThuc.Day == DateTime.Now.Day)
                                        .Sum(dp => dp.ThanhTien);
            ViewBag.DoanhThuThangNay = _context.DatPhongs
                                        .Where(dp => dp.TrangThai == 1 && dp.NgayKetThuc.Month == DateTime.Now.Month)
                                        .Sum(dp => dp.ThanhTien);
            ViewBag.SoDonThangNay = _context.DatPhongs
                                        .Where(dp => dp.TrangThai != -1 && dp.NgayKetThuc.Month == DateTime.Now.Month)
                                        .Count();

            return View(dsHoaDon);
        }

        public IActionResult XemChiTiet(int ID)
        {
            var hd = _context.HoaDonDPs.Find(ID);
            if (hd == null)
            {
                return NotFound();
            }
            return View(hd);
        }

        public IActionResult HuyDatPhong(int id)
        {
            var dp = _context.DatPhongs.Find(id);
            if (dp.TrangThai == 1)
            {
                return NotFound();
            }

            dp.TrangThai = -1;
            dp.GhiChu = "Nhân viên " + Functions._UserNameAdmin + " hủy đặt phòng";

            _context.DatPhongs.Update(dp);
            _context.SaveChanges();

            return RedirectToAction("XemChiTiet", new { id });
        }

        public IActionResult XacNhanDatPhong(int id)
        {
            var dp = _context.DatPhongs.Find(id);
            if (dp.TrangThai != 0)
            {
                return NotFound();
            }

            dp.TrangThai = 2;
            dp.GhiChu = "Nhân viên " + Functions._UserNameAdmin + " đã duyệt";

            _context.DatPhongs.Update(dp);
            _context.SaveChanges();

            return RedirectToAction("XemChiTiet", new { id });
        }

        public IActionResult HoanTatDatPhong(int id)
        {
            var dp = _context.DatPhongs.Find(id);
            if (dp.TrangThai != 2)
            {
                return NotFound();
            }

            dp.TrangThai = 1;
            dp.GhiChu = "Nhân viên " + Functions._UserNameAdmin + " xác nhận hoàn tất";

            _context.DatPhongs.Update(dp);
            _context.SaveChanges();

            return RedirectToAction("XemChiTiet", new { id });
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

            return RedirectToAction("Index");
        }

        public IActionResult DangXuat()
        {
            Functions._UserAdminID = 0;
            Functions._UserNameAdmin = String.Empty;

            return RedirectToAction("DangNhap");
        }
    }
}
