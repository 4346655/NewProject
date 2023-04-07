using Models.DTO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class CustomersDao
	{
		private Model1 db;
		public CustomersDao()
		{
			db = new Model1();
		}
		public IEnumerable<KhachHang> Pagelist_kh(string Searchstring, int page, int pagesize)
		{
			IOrderedQueryable<KhachHang> model = db.KhachHangs.OrderByDescending(x => x.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.HoTen.Contains(Searchstring)).OrderByDescending(x => x.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
		public int GetID(string username)
		{
			var model = db.KhachHangs.Where(x => x.TaiKhoan.TenTaiKhoan == username).SingleOrDefault();
			return model.ID;
		}
		
		public KhachHang GetDetailByUsername(string username)
		{
			var model = db.KhachHangs.Where(x => x.TaiKhoan.TenTaiKhoan == username).SingleOrDefault();
			return model;
		}
		public void ChangeInfo(int id,string hoten, string diachi, string email, string sdt, string avt)
		{
			var model = db.KhachHangs.Where(x => x.ID == id).SingleOrDefault();
			if(!string.IsNullOrEmpty(hoten))
			{
				model.HoTen = hoten;
			}
			if(!string.IsNullOrEmpty(diachi))
			{
				model.Diachi = diachi;
			}
			if(!string.IsNullOrEmpty(email))
			{
				model.Email = email;
			}
			if(!string.IsNullOrEmpty(sdt))
			{
				model.SDT = sdt;
			}
			if(!string.IsNullOrEmpty(avt))
			{
				model.AnhDaiDien = avt;
			}
			db.SaveChanges();
		}
	}
}
