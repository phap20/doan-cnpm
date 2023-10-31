using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{

    [Table("Phong")]
    public class Phong
    {
        [Key]
        public int ID { get; set; }
        public string? TenPhong { get; set; }
        public string? MoTa { get; set; }
        public string? Anh { get; set; }
        public int? SoGiuong { get; set; }
        public int? SoBonTam { get; set; }
        public bool? Wifi { get; set; }
        public int? Gia { get; set; }
        public int? IDTang { get; set; }
        public int? IDLoaiPhong { get; set; }
        public bool? TrangThai { get; set; }
    }

}