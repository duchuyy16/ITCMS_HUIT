using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ITCMS_HUIT.Models
{
    public partial class ITCMS_HUITContext : DbContext
    {
        public ITCMS_HUITContext()
        {
        }

        public ITCMS_HUITContext(DbContextOptions<ITCMS_HUITContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChuongTrinhDaoTao> ChuongTrinhDaoTaos { get; set; } = null!;
        public virtual DbSet<DoiTuongDangKy> DoiTuongDangKies { get; set; } = null!;
        public virtual DbSet<GiaoVien> GiaoViens { get; set; } = null!;
        public virtual DbSet<HocVien> HocViens { get; set; } = null!;
        public virtual DbSet<KhoaHoc> KhoaHocs { get; set; } = null!;
        public virtual DbSet<LopHoc> LopHocs { get; set; } = null!;
        public virtual DbSet<ThongTinHocVien> ThongTinHocViens { get; set; } = null!;
        public virtual DbSet<TrangThaiHocVien> TrangThaiHocViens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ITCMSConnectString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChuongTrinhDaoTao>(entity =>
            {
                entity.HasKey(e => e.IdchuongTrinh)
                    .HasName("PK__ChuongTr__7B0509A478DA567C");

                entity.ToTable("ChuongTrinhDaoTao");

                entity.Property(e => e.IdchuongTrinh).HasColumnName("IDChuongTrinh");

                entity.Property(e => e.TenChuongTrinh)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DoiTuongDangKy>(entity =>
            {
                entity.HasKey(e => e.IddoiTuong)
                    .HasName("PK__DoiTuong__08CB1431CFEB3459");

                entity.ToTable("DoiTuongDangKy");

                entity.Property(e => e.IddoiTuong).HasColumnName("IDDoiTuong");

                entity.Property(e => e.DoiTuongDangKy1)
                    .HasMaxLength(255)
                    .HasColumnName("DoiTuongDangKy");
            });

            modelBuilder.Entity<GiaoVien>(entity =>
            {
                entity.HasKey(e => e.IdgiaoVien)
                    .HasName("PK__GiaoVien__F2A3A3BDBEE3D49D");

                entity.ToTable("GiaoVien");

                entity.Property(e => e.IdgiaoVien).HasColumnName("IDGiaoVien");

                entity.Property(e => e.ChungChi).HasMaxLength(255);

                entity.Property(e => e.HoSoCaNhan).HasMaxLength(255);

                entity.Property(e => e.TenGiaoVien).HasMaxLength(255);

                entity.Property(e => e.TrinhDo).HasMaxLength(255);
            });

            modelBuilder.Entity<HocVien>(entity =>
            {
                entity.HasKey(e => e.IdhocVien)
                    .HasName("PK__HocVien__0E2229D16ED4347F");

                entity.ToTable("HocVien");

                entity.Property(e => e.IdhocVien).HasColumnName("IDHocVien");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IddoiTuong).HasColumnName("IDDoiTuong");

                entity.Property(e => e.IdtrangThai).HasColumnName("IDTrangThai");

                entity.Property(e => e.NgayDangKy).HasColumnType("date");

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt).HasColumnName("SDT");

                entity.Property(e => e.TenHocVien).HasMaxLength(255);

                entity.HasOne(d => d.IddoiTuongNavigation)
                    .WithMany(p => p.HocViens)
                    .HasForeignKey(d => d.IddoiTuong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HocVien__IDDoiTu__35BCFE0A");

                entity.HasOne(d => d.IdtrangThaiNavigation)
                    .WithMany(p => p.HocViens)
                    .HasForeignKey(d => d.IdtrangThai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HocVien__IDTrang__34C8D9D1");
            });

            modelBuilder.Entity<KhoaHoc>(entity =>
            {
                entity.HasKey(e => e.IdkhoaHoc)
                    .HasName("PK__KhoaHoc__A4BB3AD72D21924C");

                entity.ToTable("KhoaHoc");

                entity.Property(e => e.IdkhoaHoc).HasColumnName("IDKhoaHoc");

                entity.Property(e => e.HocPhi).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IdchuongTrinh).HasColumnName("IDChuongTrinh");

                entity.Property(e => e.TenKhoaHoc).HasMaxLength(255);

                entity.HasOne(d => d.IdchuongTrinhNavigation)
                    .WithMany(p => p.KhoaHocs)
                    .HasForeignKey(d => d.IdchuongTrinh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KhoaHoc__IDChuon__286302EC");
            });

            modelBuilder.Entity<LopHoc>(entity =>
            {
                entity.HasKey(e => e.IdlopHoc)
                    .HasName("PK__LopHoc__DB74F85B63EC399C");

                entity.ToTable("LopHoc");

                entity.Property(e => e.IdlopHoc).HasColumnName("IDLopHoc");

                entity.Property(e => e.DiaDiem).HasMaxLength(255);

                entity.Property(e => e.IdgiaoVien).HasColumnName("IDGiaoVien");

                entity.Property(e => e.IdkhoaHoc).HasColumnName("IDKhoaHoc");

                entity.Property(e => e.NgayBatDau).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.Property(e => e.TenLopHoc).HasMaxLength(255);

                entity.Property(e => e.ThoiGian).HasMaxLength(255);

                entity.HasOne(d => d.IdgiaoVienNavigation)
                    .WithMany(p => p.LopHocs)
                    .HasForeignKey(d => d.IdgiaoVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LopHoc__IDGiaoVi__2E1BDC42");

                entity.HasOne(d => d.IdkhoaHocNavigation)
                    .WithMany(p => p.LopHocs)
                    .HasForeignKey(d => d.IdkhoaHoc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LopHoc__IDKhoaHo__2D27B809");
            });

            modelBuilder.Entity<ThongTinHocVien>(entity =>
            {
                entity.HasKey(e => new { e.IdhocVien, e.IdlopHoc })
                    .HasName("PK__ThongTin__A395665499336A07");

                entity.ToTable("ThongTinHocVien");

                entity.Property(e => e.IdhocVien).HasColumnName("IDHocVien");

                entity.Property(e => e.IdlopHoc).HasColumnName("IDLopHoc");

                entity.Property(e => e.Diem).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.HocPhi)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LyDoThongBao).HasMaxLength(255);

                entity.Property(e => e.NgayGioGiaoDich).HasColumnType("datetime");

                entity.Property(e => e.NgayThongBao).HasColumnType("datetime");

                entity.Property(e => e.SoLanVangMat).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdhocVienNavigation)
                    .WithMany(p => p.ThongTinHocViens)
                    .HasForeignKey(d => d.IdhocVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThongTinH__IDHoc__3A81B327");

                entity.HasOne(d => d.IdlopHocNavigation)
                    .WithMany(p => p.ThongTinHocViens)
                    .HasForeignKey(d => d.IdlopHoc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThongTinH__IDLop__3B75D760");
            });

            modelBuilder.Entity<TrangThaiHocVien>(entity =>
            {
                entity.HasKey(e => e.IdtrangThai)
                    .HasName("PK__TrangTha__55658600BE477F98");

                entity.ToTable("TrangThaiHocVien");

                entity.Property(e => e.IdtrangThai).HasColumnName("IDTrangThai");

                entity.Property(e => e.MoTa).HasMaxLength(255);

                entity.Property(e => e.TenTrangThai).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
