using Microsoft.EntityFrameworkCore;
using doan.Areas.Admin.Models;
using doan.Models;

namespace doan.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<DatPhong> DatPhongs { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Tang> Tangs { get; set; }
        public DbSet<view_DatPhong_Phong> DatPhong_Phongs { get; set; }
        public DbSet<view_HoaDonDP> HoaDonDPs { get; set; }

    }
}
