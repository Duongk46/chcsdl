using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Entities
{
    [Table("QLQA_LOAITAIKHOAN")]
    public class LoaiTaiKhoan : BaseEntity
    {
        [Key]
        public Guid MaLoaiTaiKhoan { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(255)]
        public string TenLoai { get; set; }
        [NotMapped]
        public virtual ICollection<TaiKhoan>? TaiKhoans { get; set; }
    }
}
