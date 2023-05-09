using Models.DTO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class VoucherDao
	{
		private Model1 db;
		public VoucherDao()
		{
			db = new Model1();
			setTime();
		}
		public IEnumerable<Magiamgia> Pagelist_mgg(string Searchstring,bool trangthai, int page, int pagesize)
		{
			IOrderedQueryable<Magiamgia> model = db.Magiamgias.Where(x=>x.DeleteStatus ==trangthai).OrderByDescending(x => x.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.Ma.Contains(Searchstring)).Where(x => x.DeleteStatus == trangthai).OrderByDescending(x => x.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
		public void setTime()
		{
			var model = db.Magiamgias.ToList();
			foreach(var item in model)
			{
				if(item.Time2.Value < DateTime.Now)
				{
					item.Trangthai = false;
				}
				if(item.Soluong <= 0 )
				{
					item.Trangthai = false;
				}
			}
		}
		public void GiaHan(int id,DateTime time1,DateTime time2)
		{
			var model = db.Magiamgias.Where(x => x.ID == id).SingleOrDefault();
			model.Time1 = time1;
			model.Time2 = time2;
			db.SaveChanges();
		}
		public void SetDeleteStatus(int idvoucher)
		{
			var model = db.Magiamgias.Find(idvoucher);
			model.DeleteStatus = !model.DeleteStatus;
			db.SaveChanges();
		}
	}
}
