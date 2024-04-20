using CodeFirst.data_access;
using CodeFirst.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien;
using System.Text;

namespace CodeFirst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<SchoolContext>();
                    dbContext.Database.Migrate();
                    if (!dbContext.Khoas.Any())
                    {
                        dbContext.Khoas.AddRange(
                            new Khoa { TenKhoa = "CNTT" },
                            new Khoa { TenKhoa = "Kinh tế" },
                            new Khoa { TenKhoa = "Ngoại ngữ" }
                        );
                        dbContext.SaveChanges();
                        Console.WriteLine("Inserted data into the Khoa table.");
                    }
                    if (!dbContext.Lops.Any())
                    {
                        dbContext.Lops.AddRange(
                            new Lop { TenLop = "L01", KhoaId = 1 },
                            new Lop { TenLop = "L02", KhoaId = 2 },
                            new Lop { TenLop = "L03", KhoaId = 1 },
                            new Lop { TenLop = "L04", KhoaId = 3 }
                        );
                        dbContext.SaveChanges();
                        Console.WriteLine("Inserted data into the Lop table.");
                    }
                    if (!dbContext.SinhViens.Any())
                    {
                        dbContext.SinhViens.AddRange(
                            new SinhVien { Ten = "Nguyễn Huy Hoàng", Tuoi = 19, LopId = 1 },
                            new SinhVien { Ten = "Trần Thị Tuyết", Tuoi = 20, LopId = 2 },
                            new SinhVien { Ten = "Hoàng Huy Vượng", Tuoi = 18, LopId = 1 },
                            new SinhVien { Ten = "Lê Văn Huy", Tuoi = 19, LopId = 1 },
                            new SinhVien { Ten = "Trần Thị Mai", Tuoi = 20, LopId = 2 },
                            new SinhVien { Ten = "Hoàng Kim Tuyền", Tuoi = 18, LopId = 3 }
                        // Thêm sinh viên khác nếu cần
                        );
                        dbContext.SaveChanges();
                        Console.WriteLine("Inserted data into the SinhVien table.");
                    }
                    try
                    {
                        var KhoaList = dbContext.Khoas.ToList();
                        var LopList = dbContext.Lops.ToList();
                        var sinhVienList = dbContext.SinhViens.ToList();
                        foreach (var lop in LopList)
                        {
                            Console.WriteLine($"Danh sách sinh viên trong lớp {lop.TenLop}:");
                            if (lop.SinhViens != null)
                            {
                                foreach (var sv in lop.SinhViens)
                                {
                                    Console.WriteLine($"Tên: {sv.Ten}, Tuổi: {sv.Tuoi}, LopID: {sv.LopId}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Không có sinh viên.");
                            }
                            Console.WriteLine();
                        }
                        var sinhVienCNTTList = dbContext.SinhViens
                                .Include(sv => sv.Lop)
                                .ThenInclude(lop => lop.Khoa)
                                .Where(sv => sv.Lop.Khoa.TenKhoa == "CNTT" && sv.Tuoi >= 18 && sv.Tuoi <= 20)
                                .ToList();
                        Console.WriteLine("Danh sách  thông tin Sinh Viên thuộc khoa CNTT và có tuổi từ 18 đến 20.");
                        foreach (var sinhVien in sinhVienCNTTList)
                        {
                            Console.WriteLine($"Tên: {sinhVien.Ten}, Tuổi: {sinhVien.Tuoi}, Lớp: {sinhVien.Lop.TenLop}, Khoa: {sinhVien.Lop.Khoa.TenKhoa}");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while migrating the database.");
                    Console.WriteLine(ex.Message);
                }
            }
            host.Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
