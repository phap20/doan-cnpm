using doan.Models;
using doan.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace doan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DichVu()
        {
            var dv = _context.DichVus.Where(dv => dv.TrangThai == true).ToList();

            return View(dv);
        }

        public IActionResult NhanVien()
        {
            var nv = _context.NhanViens.ToList();

            return View(nv);
        }

        public IActionResult Phong(DateTime? BatDau = null, DateTime? KetThuc = null, int IDTang = 0, int IDLoaiPhong = 0)
        {
            var phong = _context.Phongs.Where(p => p.TrangThai == true);

            if (IDTang != 0)
            {
                phong = phong.Where(p => p.IDTang == IDTang);
            }

            if (IDLoaiPhong != 0)
            {
                phong = phong.Where(p => p.IDLoaiPhong == IDLoaiPhong);
            }

            ViewBag.dsIDPhongDaDuocDat = new List<int>();
            if (BatDau == null)
            {
                BatDau = DateTime.Now;
            }
            if (KetThuc == null)
            {
                KetThuc = DateTime.Now;
            }

            var dsIDPhongDaDuocDat = new List<int>();
            var dsHoaDonTrongKhoangThoiGian = _context.DatPhongs
                                                .Where(dp => dp.TrangThai == 0 || dp.TrangThai == 2)
                                                .Where(dp =>
                                                (dp.NgayBatDau > BatDau && dp.NgayBatDau < KetThuc) ||
                                                (dp.NgayKetThuc > BatDau && dp.NgayKetThuc < KetThuc) ||
                                                (dp.NgayBatDau < BatDau && dp.NgayKetThuc > KetThuc)).ToList();
            foreach (var item in dsHoaDonTrongKhoangThoiGian)
            {
                dsIDPhongDaDuocDat.Add(item.IDPhong);
            }

            ViewBag.dsIDPhongDaDuocDat = dsIDPhongDaDuocDat;

            return View(phong.ToList());
        }

        public IActionResult DatPhong(int IDPhong)
        {
            var phong = _context.Phongs.Find(IDPhong);
            if (phong == null)
            {
                return NotFound();
            }

            ViewBag.Tang = _context.Tangs.Find(phong.IDTang).TenTang ?? "";
            ViewBag.LoaiPhong = _context.LoaiPhongs.Find(phong.IDLoaiPhong).TenLoaiPhong ?? "";
            return View(phong);
        }

        [HttpPost]
        public IActionResult DatPhong(int IDPhong, DateTime BatDau, DateTime KetThuc, string YeuCau = "")
        {
            if (!Functions.IsLogin())
            {
                TempData["msg"] = "Yêu cầu đăng nhập để sử dụng chức năng này!";
                return RedirectToAction("HienThiDangNhap");
            }

            if (KetThuc < BatDau)
            {
                TempData["msg"] = "Ngày kết thúc không được nhỏ hơn ngày bắt đầu";
                return RedirectToAction("DatPhong", new { IDPhong });
            }

            var kiemTraPhongDaDat = _context.DatPhongs
                                                        .Where(dp => dp.IDPhong == IDPhong)
                                                        .Where(dp => dp.TrangThai == 0 || dp.TrangThai == 2)
                                                        .Where(dp => (dp.NgayBatDau > BatDau && dp.NgayBatDau < KetThuc) ||
                                                        (dp.NgayKetThuc > BatDau && dp.NgayKetThuc < KetThuc) ||
                                                        (dp.NgayBatDau < BatDau && dp.NgayKetThuc > KetThuc)).FirstOrDefault();
            if (kiemTraPhongDaDat != null)
            {
                TempData["msg"] = "Phòng đã được đặt trong khoảng thời gian này!";
                return RedirectToAction("DatPhong", new { IDPhong });
            }

            int soNgay = (int)Math.Ceiling((KetThuc - BatDau).TotalHours / 24);

            var phong = _context.Phongs.Find(IDPhong);

            var datphong = new DatPhong();
            datphong.IDPhong = IDPhong;
            datphong.YeuCau = YeuCau;
            datphong.TrangThai = 0;
            datphong.IDNguoiDung = Functions._UserID;
            datphong.NgayBatDau = BatDau;
            datphong.NgayKetThuc = KetThuc;
            datphong.GhiChu = "";
            datphong.Gia = phong.Gia;
            datphong.ThanhTien = datphong.Gia * soNgay;

            _context.DatPhongs.Add(datphong);
            _context.SaveChanges();

            return RedirectToAction("DSDatPhong");
        }

        public IActionResult HuyDatPhong(int id)
        {
            if (!Functions.IsLogin())
            {
                TempData["msg"] = "Yêu cầu đăng nhập để sử dụng chức năng này!";
                return RedirectToAction("HienThiDangNhap");
            }

            var dp = _context.DatPhongs.Find(id);
            if (dp == null)
            {
                return NotFound();
            }

            if (dp.IDNguoiDung != Functions._UserID)
            {
                TempData["msg"] = "Bạn Không có quyền này!";
                return RedirectToAction("DSDatPhong");
            }

            if (dp.TrangThai != 0)
            {
                TempData["msg"] = "Phòng không ở trạng thái chờ, không thể hủy!";
                return RedirectToAction("DSDatPhong");
            }

            dp.TrangThai = -1;
            dp.GhiChu = "Khách hàng hủy";

            _context.DatPhongs.Update(dp);
            _context.SaveChanges();

            TempData["msg"] = "Hủy thành công!";
            return RedirectToAction("DSDatPhong");
        }

        public IActionResult DSDatPhong()
        {
            if (!Functions.IsLogin())
            {
                TempData["msg"] = "Yêu cầu đăng nhập để sử dụng chức năng này!";
                return RedirectToAction("HienThiDangNhap");
            }
            var dsdp = _context.DatPhong_Phongs.Where(dp => dp.IDNguoiDung == Functions._UserID).OrderByDescending(dp => dp.ID).ToList();

            return View(dsdp);
        }

        public IActionResult HienThiDangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult XuLyDangKy(NguoiDung nd)
        {
            var kiemTraCoTaiKhoanDaSuDungUsername = _context.NguoiDungs.Where(nd1 => nd1.TaiKhoan == nd.TaiKhoan).FirstOrDefault();
            if (kiemTraCoTaiKhoanDaSuDungUsername != null)
            {
                TempData["msg"] = "Tài khoản đã tồn tại trong hệ thống.";
                return RedirectToAction("HienThiDangKy");
            }

            nd.MatKhau = Functions.MD5Password(nd.MatKhau);
            _context.NguoiDungs.Add(nd);
            _context.SaveChanges();
            TempData["msg"] = "Đăng ký thành công.";
            return RedirectToAction("HienThiDangNhap");
        }

        public IActionResult HienThiDangNhap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult XuLyDangNhap(NguoiDung nd)
        {
            string password = Functions.MD5Password(nd.MatKhau);
            var nguoiDung = _context.NguoiDungs.Where(nd1 => nd1.TaiKhoan == nd.TaiKhoan && nd1.MatKhau == password)
                            .FirstOrDefault();

            if (nguoiDung == null)
            {
                TempData["msg"] = "Tài khoản hoặc mật khẩu không chính xác.";
                return RedirectToAction("HienThiDangNhap");
            }

            Functions._UserID = nguoiDung.ID;
            Functions._UserName = nguoiDung.HoTen ?? "";

            return RedirectToAction("Index");
        }

        public IActionResult DangXuat()
        {
            Functions._UserID = 0;
            Functions._UserName = String.Empty;
            return RedirectToAction("HienThiDangNhap");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}