namespace CodeFirst.Models
{
    public class Lop
    {
        public int LopId { get; set; }
        public string TenLop { get; set; }
        public int KhoaId { get; set; }
        public Khoa Khoa { get; set; }
        public ICollection<SinhVien> SinhViens { get; set; }
    }
}
