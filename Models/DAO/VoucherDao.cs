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
		}
		public IEnumerable<Magiamgia> Pagelist_mgg(string Searchstring, int page, int pagesize)
		{
			IOrderedQueryable<Magiamgia> model = db.Magiamgias.OrderByDescending(x => x.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.Ma.Contains(Searchstring)).OrderByDescending(x => x.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
	}
}
