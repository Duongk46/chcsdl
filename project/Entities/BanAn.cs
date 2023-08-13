using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Entities
{
    [Table("QLQA_BANAN")]
    public class BanAn
    {
        [Key]
        public Guid MaBanAn { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(255)]
        public string Ten { get; set; }

        [NotMapped]
        public virtual ICollection<ThongTinHoaHon>? ThongTinHoaHons { get; set; }
        [NotMapped]
        public virtual ICollection<HoaDon>? HoaDons { get; set; }
    }
}
