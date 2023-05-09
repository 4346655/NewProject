using Models.DTO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class OrdersDao
	{
		private Model1 db;
		public OrdersDao()
		{
			db = new Model1();
		}
	
		public List<DonHang> List()
		{
			return db.DonHangs.ToList();
		}
		public List<DonHang> List_distinct()
		{
			var distinctDonHangs = db.DonHangs
									.GroupBy(d => d.ID_Sach)
									.Select(g => g.FirstOrDefault())
									.ToList();
			return distinctDonHangs;
		}
		public List<DonHang> List(string tensach)
		{
			return db.DonHangs.Where(x => x.Sach.TenSach == tensach).ToList();
		}

		public IEnumerable<DonHang> DanhSachDonHang(string Searchstring, int page, int pagesize,int trangthai)
		{
			IOrderedQueryable<DonHang> model = db.DonHangs.Where(x=>x.Trang_thai==trangthai).OrderByDescending(x => x.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.KhachHang.TaiKhoan.TenTaiKhoan.Contains(Searchstring) && x.Trang_thai ==trangthai).OrderByDescending(x => x.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
	
		public List<DonHang> List_IDKH(int iduser)
		{
			return db.DonHangs.Where(x => x.ID_KhachHang == iduser && (x.Trang_thai==1 || x.Trang_thai==2) ).ToList();
		}
		public List<DonHang> DanhSachChuanBi()
		{
			var model = db.DonHangs.Where(x => x.Trang_thai == 1).OrderByDescending(x => x.ID);
			return model.ToList();
		}
		public DonHang TempGH(int idsach, int iduser, int soluong)
		{
			var model = db.Saches.Where(x => x.ID == idsach).SingleOrDefault();
			if (soluong < model.SoLuong)
			{
				return new DonHang
				{
					ID_Sach = idsach,
					ID_KhachHang = iduser,
					SoLuong = soluong,
				};
			}
			else
			{
				return new DonHang
				{
					ID_Sach = idsach,
					ID_KhachHang = iduser,
					SoLuong = 1,
				};
			}
		}
		public void CreateNew(int idsach, int iduser, int idthanhtoan, int soluong, string MGG, string note)
		{
			
			DonHang a = new DonHang
			{
				ID_KhachHang = iduser,
				ID_Sach = idsach,
				ID_ThanhToan = idthanhtoan,
				SoLuong = soluong,
				Ma_GG = MGG,
				Note = note,
				Trang_thai = 1,
				Tong_gia = 0,
			};
			db.Saches.Where(x => x.ID == idsach).SingleOrDefault().SoLuong -= soluong;
			db.Saches.Where(x => x.ID == idsach).SingleOrDefault().SoLuong_DaBan += soluong;
			
			

			var model = db.Magiamgias.Where(x => x.Ma == MGG).SingleOrDefault();
			int gia = db.Saches.Where(x=>x.ID == idsach).SingleOrDefault().Gia.Value;
			if (model != null)
			{
				
				if (model.Soluong > 0)
				{
					a.Tong_gia = a.SoLuong * gia - Convert.ToInt32(model.Giatri);
					model.Soluong -= 1;
				}
				else
				{
					a.Tong_gia = a.SoLuong * gia;

				}
				var mgg = db.Temps.Where(x => x.ID_TK == iduser && x.Magiamgia.Ma == MGG).SingleOrDefault();
				db.Temps.Remove(mgg);
			}
			else
			{
				a.Tong_gia = a.SoLuong * gia;
			}
			db.DonHangs.Add(a);
			db.SaveChanges();

		}
		public void HuyDon(int idorder)
		{
			var model = db.DonHangs.Where(x => x.ID == idorder).SingleOrDefault();
			model.Trang_thai = 0;
			db.SaveChanges();
		}
		public void Mualai(int idorder)
		{
			var model = db.DonHangs.Where(x => x.ID == idorder).SingleOrDefault();
			model.Trang_thai = 1;
			db.SaveChanges();
		}
		public void ChuyenTiepTrangThai(int idor)
		{
			var model = db.DonHangs.Where(x => x.ID == idor).SingleOrDefault();
			model.Trang_thai += 1;
			db.SaveChanges();
		}
		public int LayTrangThai(int idor)
		{
			return db.DonHangs.Where(x => x.ID == idor).SingleOrDefault().Trang_thai.Value;
		}
		public List<DonHang> Historysearch(int iduser)
		{
			var model = db.DonHangs.Where(x => x.ID_KhachHang == iduser && (x.Trang_thai == 4 || x.Trang_thai == 0)).ToList();
			return model;
		}
		public bool checkDonhang(int idsach,int soluong)
		{
			var model = db.Saches.Where(x => x.ID == idsach).SingleOrDefault();
			if (soluong > model.SoLuong)
				return false;
			return true;
		}
		public void CreateNewRange(List<GioHang> li,int iduser, int idthanhtoan, string MGG, string note)
		{
			foreach(var item in li)
			{
				if(checkDonhang((int)item.ID_Sach,(int)item.SoLuong) == true)
				{
					CreateNew((int)item.ID_Sach, iduser, idthanhtoan, (int)item.SoLuong, MGG, note);
				}
			}
		}
	}
}
