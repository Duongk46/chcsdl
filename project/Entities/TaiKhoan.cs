using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Entities
{
    [Table("QLQA_TAIKHOAN")]
    public class TaiKhoan: BaseEntity
    {
        [Key]
        public Guid MaTaiKhoan { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(255)]
        public string HoTen { get; set; }
        public int GioiTinh { get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(255)]
        public string? DiaChi { get; set; }
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public Guid LoaiTaiKhoanID { get; set; }
        [NotMapped]
        public virtual LoaiTaiKhoan? LoaiTaiKhoan { get; set; }
        [NotMapped]
        public virtual ICollection<LoaiMonAn>? LoaiMonAns { get; set; }
        [NotMapped]
        public virtual ICollection<MonAn>? MonAns { get; set; }
        [NotMapped]
        public virtual ICollection<BanAn>? BanAns { get; set; }
        [NotMapped]
        public virtual ICollection<HoaDon>? HoaDons { get; set; }
        [NotMapped]
        public virtual ICollection<ThongTinHoaHon>? ThongTinHoaHons { get; set; }
    }
}
