using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class CartDao
	{
		private Model1 db;
		public CartDao()
		{
			db = new Model1();
		}
		public List<GioHang> GioHang_IDUser(int iduser)
		{
			return db.GioHangs.Where(x => x.ID_KhachHang == iduser).ToList();
		}
		public void Themsanpham(int idsach, int iduser, int soluong)
		{
			GioHang a = new GioHang
			{
				ID_KhachHang = iduser,
				ID_Sach = idsach,
				SoLuong = soluong,
			};
			db.GioHangs.Add(a);
			db.SaveChanges();
		}
		public void Xoasanpham(int id)
		{
			var model = db.GioHangs.Find(id);
			db.GioHangs.Remove(model);
			db.SaveChanges();
		}
	}
}
