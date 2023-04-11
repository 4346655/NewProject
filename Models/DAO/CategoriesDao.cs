using Models.DTO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class CategoriesDao
	{
		private Model1 db;
		public CategoriesDao()
		{
			db = new Model1();
		}
		public IEnumerable<Loai_Sach> Pagelist_dm(string Searchstring,bool trangthai, int page, int pagesize)
		{
			IOrderedQueryable<Loai_Sach> model = db.Loai_Sach.Where(x=>x.Trangthai == trangthai).OrderByDescending(x => x.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.Loaisach.Contains(Searchstring)).Where(x=>x.Trangthai == trangthai).OrderByDescending(x => x.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
		public void Delete(int id)
		{
			var model = db.Saches.Where(x => x.ID_LoaiSach == id).ToList();
			foreach (Sach i in model)
			{
				i.Loai_Sach.Loaisach = "None";
			}
			var model1 = db.Loai_Sach.Find(id);
			db.Loai_Sach.Remove(model1);
			db.SaveChanges();
		}
		public List<Loai_Sach> List()
		{
			return db.Loai_Sach.Where(x => x.Trangthai == true && x.Saches.Count > 0).ToList();
		}
		public void ChangeStatus(int idls)
		{
			var model = db.Loai_Sach.Where(x => x.ID == idls).SingleOrDefault();
			model.Trangthai = !model.Trangthai;
			db.SaveChanges();
		}
	}
}
