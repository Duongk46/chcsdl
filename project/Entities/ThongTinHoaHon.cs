using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Entities
{
    [Table("QLQA_THONGTINHOADON")]
    public class ThongTinHoaHon
    {
        [Key]
        public Guid? MaThongTinHoaDon { get; set; }
        public int SoLuong { get; set; }
        public float Tong { get; set; }
        public Guid? HoaDonID { get; set; }
        public Guid? MonAnID { get; set; }
        [NotMapped]
        public HoaDon? HoaDon { get; set; }
        [NotMapped]
        public MonAn? MonAn { get; set; }

        public ThongTinHoaHon()
        {
            if (HoaDon != null && MonAn != null)
            {
                this.Tong = this.SoLuong * MonAn.Gia;
            }
        }

    }
}
