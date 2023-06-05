using Models.DTO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Models.DAO
{
	public class CustomersDao
	{
		private Model1 db;
		public CustomersDao()
		{
			db = new Model1();
		}
		public int CheckInfo(int id)
		{
			var model = db.KhachHangs.SingleOrDefault(x => x.ID == id);
			if(String.IsNullOrEmpty(model.Diachi) || String.IsNullOrEmpty(model.Email) || String.IsNullOrEmpty(model.SDT))
			{
				return 0;
			}
			return 1;
		}
		public IEnumerable<KhachHang> DanhSachKhachHang(string Searchstring, int page, int pagesize)
		{
			IOrderedQueryable<KhachHang> model = db.KhachHangs.Where(x=>x.TaiKhoan.ID_LoaiTK !=2).OrderByDescending(x => x.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.TaiKhoan.TenTaiKhoan.Contains(Searchstring)).OrderByDescending(x => x.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
		public int GetID(string username)
		{
			var model = db.KhachHangs.Where(x => x.TaiKhoan.TenTaiKhoan == username).SingleOrDefault();
			return model.ID;
		}
		public int GetIDTK(string username)
		{
			var model = db.KhachHangs.Where(x => x.TaiKhoan.TenTaiKhoan == username).SingleOrDefault();
			return (int)model.ID_TaiKhoan;
		}

		public KhachHang GetDetailByUsername(string username)
		{
			var model = db.KhachHangs.Where(x => x.TaiKhoan.TenTaiKhoan == username).SingleOrDefault();
			return model;
		}

		public  bool IsValidTEN(string name)
		{
			// Kiểm tra tên không bắt đầu bằng khoảng trắng hoặc ký tự đặc biệt
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

			// Kiểm tra tên không quá dài
			if (name.Length > 50)
			{
				return false;
			}

			// Nếu không có lỗi, trả về true
			return true;
		}
		public int checkChangeInfo(string hoten, string email, string sdt)
		{
			int key =0;
			
		

			for (int i = 0; i < sdt.Length; i++)
			{
				if(sdt.Length!=10)
				{
					key = 4;
					break;
				}	
				if((int)sdt[i] < 48 || (int)sdt[i]>57)
				{
					key = 2;
					break;
				}
				
			}
			if (key != 0) return key;

			int count1 = 0,count2=0;
			
			for (int i = 0; i < email.Length; i++)
			{
				if ((int)email[i] == 64 )
				{
					count1++;
				}
				if ((int)email[i] == 46)
				{
					count2++;
				}
			}
			if(count1!=1 && count2 ==0)
			{
				key = 3;
			}	

			return key;


			}
		public int ChangeInfo(int id,string hoten, string diachi, string email, string sdt, string avt)
		{
			int key = checkChangeInfo(hoten, email, sdt);
			if (key == 0)
			{
				var model = db.KhachHangs.Where(x => x.ID == id).SingleOrDefault();
				if (!string.IsNullOrEmpty(hoten))
				{
					if (IsValidTEN(hoten) == true)
					{
						model.HoTen = hoten;
					}
					else
					{
						key = 1;
					}
					
				}
				if (!string.IsNullOrEmpty(diachi))
				{
					model.Diachi = diachi;
				}
				if (!string.IsNullOrEmpty(email))
				{
					model.Email = email;
				}
				if (!string.IsNullOrEmpty(sdt))
				{
					model.SDT = sdt;
				}
				if (!string.IsNullOrEmpty(avt))
				{
					model.AnhDaiDien = avt;
				}
				db.SaveChanges();
			}
			return key;
			
		}
		public void Bat_Tat_HoatDong(int id )
		{
			var model = db.KhachHangs.Where(x => x.ID == id).SingleOrDefault();
			model.TaiKhoan.Trangthai = !model.TaiKhoan.Trangthai;
			db.SaveChanges();
		}
		public void addHistorySearch(int idser,string noidung)
		{
			var model = db.History_Search.Where(x => x.KhachHang.ID == idser && x.NoiDung == noidung).SingleOrDefault();
			if(model ==null)
			{
				db.History_Search.Add(new History_Search
				{
					ID_KhachHang = idser,
					NoiDung = noidung,
				});
				db.SaveChanges();
			}	
		}
		public List<History_Search> getListHistorySearch(int iduser)
		{
			return db.History_Search.Where(x => x.ID_KhachHang == iduser).OrderByDescending(x=>x.ID).ToList();
		}
		
	}
}
