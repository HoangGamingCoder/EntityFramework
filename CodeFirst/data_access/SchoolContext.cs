using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.data_access
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
    }
}
