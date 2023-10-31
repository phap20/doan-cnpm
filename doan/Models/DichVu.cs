using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{

    [Table("DichVu")]
    public class DichVu
    {
        [Key]
        public int ID { get; set; }
        public string? TenDichVu { get; set; }
        public string? MoTa { get; set; }
        public string? Icon { get; set; }
        public bool? TrangThai { get; set; }
    }

}