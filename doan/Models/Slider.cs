using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{

    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int ID { get; set; }
        public string? Anh { get; set; }
        public string? TieuDe { get; set; }
        public string? TieuDePhu { get; set; }
        public bool? TrangThai { get; set; }
    }

}