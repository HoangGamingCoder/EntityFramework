namespace CodeFirst.Models
{
    public class Khoa
    {
        public int KhoaId { get; set; }
        public string TenKhoa { get; set; }
        public ICollection<Lop> Lops { get; set; }
    }
}
