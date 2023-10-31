using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{

    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        public int ID { get; set; }
        public string? HoTen { get; set; }
        public string? TaiKhoan { get; set; }
        public string? MatKhau { get; set; }
        public string? VaiTro { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Anh { get; set; }
    }

}