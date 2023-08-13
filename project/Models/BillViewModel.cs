namespace project.Models
{
    public class ObjectItem
    {
        public Guid Item { get; set; }
        public int Amount { get; set; }
    }
    public class BillViewModel
    {
        public float VAT { get; set; }
        public Guid BanAnID { get; set; }
        public IEnumerable<ObjectItem> ObjectDatas { get; set; }
    }
}
