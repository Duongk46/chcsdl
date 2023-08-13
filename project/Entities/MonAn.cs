using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Entities
{
    [Table("QLQA_MONAN")]
    public class MonAn
    {
        [Key]
        public Guid MaMonAn { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(255)]
        public string TenMonAn { get; set; }
        public long Gia { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime? NgayTao { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime? NgayUpdate { get; set; }
		public Guid LoaiMonAnID { get; set; }
		public bool Status { get; set; } = true;
		[NotMapped]
        public virtual LoaiMonAn? LoaiMonAn { get; set; }
        [NotMapped]
        public virtual ICollection<ThongTinHoaHon>? ThongTinHoaHons { get; set; }
    }
}
