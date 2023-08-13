using Microsoft.EntityFrameworkCore;

namespace project.Entities
{
    public class QuanAnContext : DbContext
    {
        public QuanAnContext(DbContextOptions<QuanAnContext> options) : base(options)
        {

        }

        public virtual DbSet<BanAn> BanAns { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<LoaiMonAn> LoaiMonAns { get; set; }
        public virtual DbSet<LoaiTaiKhoan> LoaiTaiKhoans { get; set; }
        public virtual DbSet<MonAn> MonAns { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThongTinHoaHon> ThongTinHoaHons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoaDon>().HasOne(x => x.TaiKhoan).WithMany(x => x.HoaDons).HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<HoaDon>().HasOne(x => x.BanAn).WithMany(x => x.HoaDons).HasForeignKey(x => x.BanAnID).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MonAn>().HasOne(x => x.LoaiMonAn).WithMany(x => x.MonAns).HasForeignKey(x => x.LoaiMonAnID).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TaiKhoan>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<TaiKhoan>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<TaiKhoan>().HasOne(x => x.LoaiTaiKhoan).WithMany(x => x.TaiKhoans).HasForeignKey(x => x.LoaiTaiKhoanID).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ThongTinHoaHon>().HasOne(x => x.HoaDon).WithMany(x => x.ThongTinHoaHons).HasForeignKey(x => x.HoaDonID).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ThongTinHoaHon>().HasOne(x => x.MonAn).WithMany(x => x.ThongTinHoaHons).HasForeignKey(x => x.MonAnID).OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<ThongTinHoaHon>().HasOne(x => x.BanAn).WithMany(x => x.ThongTinHoaHons).HasForeignKey(x => x.BanAnID);
            base.OnModelCreating(modelBuilder);
        }
    }
}
