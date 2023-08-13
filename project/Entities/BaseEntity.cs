using System.ComponentModel.DataAnnotations.Schema;

namespace project.Entities
{
    public class BaseEntity
    {
        [Column(TypeName = "datetime2")]
        public DateTime? NgayTao { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? NgayUpdate { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool Status { get; set; } = true;
    }
}
