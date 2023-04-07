using Models.DTO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class ProductsDao
	{
		private Model1 db;
		public ProductsDao()
		{
			db = new Model1();
		}
		public IEnumerable<Sach> Pagelist_sa(string Searchstring, int page, int pagesize)
		{
			IOrderedQueryable<Sach> model = db.Saches.Where(x => x.Trangthai == true).OrderByDescending(x => x.Loai_Sach.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.TenSach.Contains(Searchstring)).OrderByDescending(x => x.Loai_Sach.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
		public void Delete(int id)
		{
			var model0 = db.Saches.Find(id);
			db.Saches.Remove(model0);

			var model = db.BinhLuans.Where(x => x.ID_Sach == id).ToList();
			db.BinhLuans.RemoveRange(model);

			var model1 = db.GioHangs.Where(x => x.ID_Sach == id).ToList();
			db.GioHangs.RemoveRange(model1);

			var model2 = db.DonHangs.Where(x => x.ID_Sach == id).ToList();
			db.DonHangs.RemoveRange(model2);

			var model3 = db.Saodanhgias.Where(x => x.ID_Sach == id).ToList();
			db.Saodanhgias.RemoveRange(model3);

			db.SaveChanges();

		}
		public IEnumerable<Sach> Pagelist_false(string Searchstring, int page, int pagesize)
		{
			IOrderedQueryable<Sach> model = db.Saches.Where(x => x.Trangthai == false).OrderByDescending(x => x.Loai_Sach.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.TenSach.Contains(Searchstring)).OrderByDescending(x => x.Loai_Sach.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
		public void SetStates(int id, bool a)
		{
			var model = db.Saches.Find(id);
			model.Trangthai = a;
			db.SaveChanges();
		}
		public List<Sach> Sach_categoriesID(int idloaisach)
		{
			var model = db.Saches.Where(x => x.ID_LoaiSach == idloaisach && x.Trangthai == true && x.SoLuong>0).ToList();
			return model;
		}
		public Sach SachDetail(int id)
		{
			return db.Saches.Find(id);
		}
		public List<BinhLuan> GetComment(int idsach)
		{ 
			return db.BinhLuans.Where(x => x.ID_Sach == idsach).ToList();	
		}
		public void addcomment(int idsach,int iduser,string noidung)
		{
			BinhLuan a = new BinhLuan
			{
				ID_KhachHang = iduser,
				ID_Sach = idsach,
				NoiDung = noidung,
			};
			db.BinhLuans.Add(a);
			db.SaveChanges();
		}
		public void AddStart(int idsach,int sosao)
		{
			Saodanhgia a = new Saodanhgia
			{
				ID_Sach = idsach,
				Sosao = sosao,
			};
			db.Saodanhgias.Add(a);
			db.SaveChanges();
		}
		public double AvgStart(int idsach)
		{
			var model = db.Saodanhgias.Where(x => x.ID_Sach == idsach).ToList();
			int avg = 0;
			foreach(var item in model)
			{
				avg += item.Sosao;
			}
			return (double)avg / (model.Count);
		}
	}
}
