using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models.DTO
{
	public partial class Model1 : DbContext
	{
		public Model1()
			: base("name=Model1")
		{
		}

		public virtual DbSet<BinhLuan> BinhLuans { get; set; }
		public virtual DbSet<DonHang> DonHangs { get; set; }
		public virtual DbSet<GioHang> GioHangs { get; set; }
		public virtual DbSet<History_Search> History_Search { get; set; }
		public virtual DbSet<KhachHang> KhachHangs { get; set; }
		public virtual DbSet<Loai_Sach> Loai_Sach { get; set; }
		public virtual DbSet<LoaiTK> LoaiTKs { get; set; }
		public virtual DbSet<Magiamgia> Magiamgias { get; set; }
		public virtual DbSet<Sach> Saches { get; set; }
		public virtual DbSet<Saodanhgia> Saodanhgias { get; set; }
		public virtual DbSet<shipper> shippers { get; set; }
		public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
		public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
		public virtual DbSet<Temp> Temps { get; set; }
		public virtual DbSet<ThanhToan> ThanhToans { get; set; }
		public virtual DbSet<ThongBao> ThongBaos { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DonHang>()
				.HasMany(e => e.shippers)
				.WithOptional(e => e.DonHang)
				.HasForeignKey(e => e.ID_DonHang);

			modelBuilder.Entity<KhachHang>()
				.HasMany(e => e.BinhLuans)
				.WithOptional(e => e.KhachHang)
				.HasForeignKey(e => e.ID_KhachHang);

			modelBuilder.Entity<KhachHang>()
				.HasMany(e => e.DonHangs)
				.WithOptional(e => e.KhachHang)
				.HasForeignKey(e => e.ID_KhachHang);

			modelBuilder.Entity<KhachHang>()
				.HasMany(e => e.GioHangs)
				.WithOptional(e => e.KhachHang)
				.HasForeignKey(e => e.ID_KhachHang);

			modelBuilder.Entity<KhachHang>()
				.HasMany(e => e.History_Search)
				.WithOptional(e => e.KhachHang)
				.HasForeignKey(e => e.ID_KhachHang);

			modelBuilder.Entity<Loai_Sach>()
				.HasMany(e => e.Saches)
				.WithOptional(e => e.Loai_Sach)
				.HasForeignKey(e => e.ID_LoaiSach);

			modelBuilder.Entity<LoaiTK>()
				.HasMany(e => e.TaiKhoans)
				.WithRequired(e => e.LoaiTK)
				.HasForeignKey(e => e.ID_LoaiTK)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Magiamgia>()
				.Property(e => e.Ma)
				.IsUnicode(false);

			modelBuilder.Entity<Magiamgia>()
				.HasMany(e => e.Temps)
				.WithOptional(e => e.Magiamgia)
				.HasForeignKey(e => e.ID_MGG);

			modelBuilder.Entity<Sach>()
				.HasMany(e => e.BinhLuans)
				.WithOptional(e => e.Sach)
				.HasForeignKey(e => e.ID_Sach);

			modelBuilder.Entity<Sach>()
				.HasMany(e => e.DonHangs)
				.WithOptional(e => e.Sach)
				.HasForeignKey(e => e.ID_Sach);

			modelBuilder.Entity<Sach>()
				.HasMany(e => e.GioHangs)
				.WithOptional(e => e.Sach)
				.HasForeignKey(e => e.ID_Sach);

			modelBuilder.Entity<Sach>()
				.HasMany(e => e.Saodanhgias)
				.WithOptional(e => e.Sach)
				.HasForeignKey(e => e.ID_Sach);

			modelBuilder.Entity<TaiKhoan>()
				.Property(e => e.MatKhau)
				.IsUnicode(false);

			modelBuilder.Entity<TaiKhoan>()
				.HasMany(e => e.KhachHangs)
				.WithOptional(e => e.TaiKhoan)
				.HasForeignKey(e => e.ID_TaiKhoan);

			modelBuilder.Entity<TaiKhoan>()
				.HasMany(e => e.shippers)
				.WithOptional(e => e.TaiKhoan)
				.HasForeignKey(e => e.ID_TaiKhoan);

			modelBuilder.Entity<TaiKhoan>()
				.HasMany(e => e.Temps)
				.WithOptional(e => e.TaiKhoan)
				.HasForeignKey(e => e.ID_TK);

			modelBuilder.Entity<ThanhToan>()
				.HasMany(e => e.DonHangs)
				.WithOptional(e => e.ThanhToan)
				.HasForeignKey(e => e.ID_ThanhToan);
		}
	}
}
