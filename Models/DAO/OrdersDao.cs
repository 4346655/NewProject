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
		public IEnumerable<DonHang> Pagelist_dh(string Searchstring, int page, int pagesize)
		{
			IOrderedQueryable<DonHang> model = db.DonHangs.OrderByDescending(x => x.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.KhachHang.TaiKhoan.TenTaiKhoan.Contains(Searchstring)).OrderByDescending(x => x.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
		public List<DonHang> List_IDKH(int iduser)
		{
			return db.DonHangs.Where(x => x.ID_KhachHang == iduser ).ToList();
		}
		public List<DonHang> list()
		{
			return db.DonHangs.ToList();
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
			};
			db.Saches.Where(x => x.ID == idsach).SingleOrDefault().SoLuong -= soluong;
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
		
	}
}
