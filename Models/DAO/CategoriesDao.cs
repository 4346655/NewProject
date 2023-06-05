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
		public List<Loai_Sach> List0()
		{
			return db.Loai_Sach.ToList();
		}
		public IEnumerable<Loai_Sach> DanhSachDanhMuc(string Searchstring,bool trangthai, int page, int pagesize)
		{
			IOrderedQueryable<Loai_Sach> model = db.Loai_Sach.Where(x=>x.Trangthai == trangthai).OrderByDescending(x => x.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.Loaisach.Contains(Searchstring)).Where(x=>x.Trangthai == trangthai).OrderByDescending(x => x.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
		public void XoaDanhMuc(int id)
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
		public bool IsValidTEN(string name)
		{
			// Kiểm tra tên danh mcuj không bắt đầu bằng khoảng trắng hoặc ký tự đặc biệt
			if (char.IsWhiteSpace(name[0]) || char.IsPunctuation(name[0]) || char.IsSymbol(name[0]))
			{
				return false;
			}

			// Kiểm tra tên chỉ chứa các ký tự chữ cái, khoảng trắng, dấu gạch ngang, và các ký tự đặc biệt
			foreach (char c in name)
			{
				if (!(char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' ||
					char.IsHighSurrogate(c) || char.IsLowSurrogate(c)))
				{
					return false;
				}
			}

			// Nếu không có lỗi, trả về true
			return true;
		}
		public int ThemDanhMuc(string name,bool trangthai)
		{
			if(IsValidTEN(name))
			{
				Loai_Sach a = new Loai_Sach
				{
					Loaisach = name,
					Trangthai = trangthai,
				};
				db.Loai_Sach.Add(a);
				db.SaveChanges();
				return 1;
			}
			return 0;
		}
		public List<Loai_Sach> List()
		{
			var model = db.Loai_Sach.Where(x => x.Trangthai == true).ToList();
			List<Loai_Sach> loai_Saches = new List<Loai_Sach>();

			foreach(var item in model)
			{
				var listsach = db.Saches.Where(x => x.ID_LoaiSach == item.ID).ToList();
				foreach(var item1 in listsach)
				{
					if(item1.Trangthai == true)
					{
						loai_Saches.Add(item);
						break;
					}
				}
			}
			return loai_Saches;
		}
		public void Bat_Tat_HoatDong(int idls)
		{
			var model = db.Loai_Sach.Where(x => x.ID == idls).SingleOrDefault();
			model.Trangthai = !model.Trangthai;
			db.SaveChanges();
		}
	}
}
