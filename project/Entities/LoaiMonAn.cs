using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Entities
{
    [Table("QLQA_LOAIMONAN")]
    public class LoaiMonAn
    {
        [Key]
        public Guid MaLoaiMonAn { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(255)]
        public string Ten { get; set; }
        [NotMapped]
        public virtual Collection<MonAn>? MonAns { get; set; }
    }
}
