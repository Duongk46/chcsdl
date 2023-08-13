using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Entities
{
    [Table("QLQA_HOADON")]
    public class HoaDon : BaseEntity
    {
        [Key]
        public Guid MaHoaDon { get; set; }
        public float VAT { get; set; }
        public float? TongTien { get; set; }
        public Guid BanAnID { get; set; }
        [NotMapped]
        public virtual BanAn? BanAn { get; set; }
        [NotMapped]
        public virtual ICollection<ThongTinHoaHon>? ThongTinHoaHons { get; set; } = new HashSet<ThongTinHoaHon>();
        [NotMapped]
        public virtual TaiKhoan? TaiKhoan { get; set; }
        public HoaDon()
        {
            foreach(var item in ThongTinHoaHons)
            {
                this.TongTien += item.SoLuong;
			}
            this.TongTien = this.TongTien - this.TongTien * (float)(this.VAT / 100);
        }
    }
}
