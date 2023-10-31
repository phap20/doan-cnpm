using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan.Models
{

    [Table("Tang")]
    public class Tang
    {
        [Key]
        public int ID { get; set; }
        public string? TenTang { get; set; }
    }

}