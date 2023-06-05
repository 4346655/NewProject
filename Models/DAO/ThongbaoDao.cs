using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class ThongbaoDao
	{
		private Model1 db;
		public ThongbaoDao()
		{
			db = new Model1();
		}
		public void Add(ThongBao a)
		{
			db.ThongBaos.Add(a);
			db.SaveChanges();
		}
		public List<ThongBao> List()
		{
			return db.ThongBaos.ToList();
		}
		public void Delete(int ID)
		{
			var model = db.ThongBaos.Find(ID);
			db.ThongBaos.Remove(model);
			db.SaveChanges();
		}
	}
}
