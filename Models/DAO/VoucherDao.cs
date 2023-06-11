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
				if (item.Time1.Value <= DateTime.Now && item.Time2.Value > DateTime.Now)
				{
					item.Trangthai = true;
				}
			}
		}
		public int CheckCode(Magiamgia a)
		{
			var model = db.Magiamgias.SingleOrDefault(x => x.Ma == a.Ma);
			if(model != null)
			{
				model.Soluong = a.Soluong;
				model.Giatri = a.Giatri;
				model.Time1 = a.Time1;
				model.Time2 = a.Time2;
				db.SaveChanges();
				return 1;
			}
			else
			{
				return 0;
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
		public void giveout(Magiamgia a ,int soluong)
		{
			var account = db.TaiKhoans.ToList();
			if(account.Count <= soluong)
			{
				foreach(var item in account)
				{
					Temp a1 = new Temp
					{
						ID_MGG = a.ID,
						ID_TK = item.ID
					};
					db.Temps.Add(a1);
				}
			}
			else
			{
				Random random = new Random();
				int minValue = 0;
				int maxValue = account.Count;
				int count = soluong;

				List<int> randomNumbers = new List<int>();

				while (randomNumbers.Count < count)
				{
					int randomNumber = random.Next(minValue, maxValue);

					if (!randomNumbers.Contains(randomNumber))
					{
						randomNumbers.Add(randomNumber);
					}
				}
				foreach(var item in randomNumbers)
				{
					Temp a1 = new Temp
					{
						ID_MGG = a.ID,
						ID_TK = account[item].ID,
					};
					db.Temps.Add(a1);
				}	

			}
			db.SaveChanges();
		}
	}
}
