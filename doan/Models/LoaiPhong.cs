using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{

    [Table("LoaiPhong")]
    public class LoaiPhong
    {
        [Key]
        public int ID { get; set; }
        public string? TenLoaiPhong { get; set; }
    }

}