using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
	public class AccountDao
	{
		private Model1 db;
		public AccountDao()
		{
			db = new Model1();
		}
		public int login(string username, string password)
		{
			var model = db.TaiKhoans.Where(x => x.TenTaiKhoan == username && x.MatKhau == password).SingleOrDefault();

			if (model == null)
			{
				return 0;
			}
			else
			{
				return 1;
			}
		}
		public int register(string username, string newpassword, string confirm)
		{
			var model = db.TaiKhoans.Where(x => x.TenTaiKhoan == username).SingleOrDefault();
			if (model != null)
			{
				return 0;
			}
			else
			{
				if (string.Compare(newpassword, confirm) == 0 && newpassword != "")
				{
					TaiKhoan a = new TaiKhoan
					{
						ID_LoaiTK = 1,
						Trangthai = true,
						TenTaiKhoan = username,
						MatKhau = newpassword,
					};
					db.TaiKhoans.Add(a);
					db.SaveChanges();
					return 1;
				}
				else
				{
					return 2;
				}
			}
		}
		public int Byname(string username)
		{
			var model = db.TaiKhoans.Where(x => x.TenTaiKhoan == username).SingleOrDefault();
			if (model.LoaiTK.Ten_LoaiTK == "user")
				return 1;
			else if (model.LoaiTK.Ten_LoaiTK == "admin")
				return 2;
			else return 3;
		}
		public string quenmatkhau(string username, string sodienthoai)
		{
			if (username != "" && sodienthoai != "")
			{
				var model = db.TaiKhoans.Where(x => x.TenTaiKhoan == username).Single();
				if (model == null)
				{
					return "0";
				}
				else
				{
					var model1 = db.KhachHangs.Where(x => x.TaiKhoan.TenTaiKhoan == username).SingleOrDefault();
					if (model1.SDT != sodienthoai)
					{
						return "1";
					}
					else
					{
						string randomSDT = "";
						try
						{

							string[] newSDT = new string[8];
							Random autoRand = new Random();
							for (int i = 0; i < newSDT.Length; i++)
							{
								newSDT[i] = Convert.ToChar(Convert.ToInt32(autoRand.Next(65, 87))).ToString();
								randomSDT += (newSDT[i].ToString());
							}
						}
						catch (Exception ex)
						{
							randomSDT = "error";
						}
						model.MatKhau = randomSDT;
						db.SaveChanges();
						return randomSDT;
					}
				}
			}
			else
			{
				return "2";
			}


		}
		public void CreateNew(string username)
		{
			var model = db.TaiKhoans.Where(x => x.TenTaiKhoan == username).SingleOrDefault();
			var mode = model.KhachHangs.Where(x => x.ID_TaiKhoan == model.ID).SingleOrDefault();
			if (mode == null)
			{
				KhachHang a = new KhachHang
				{
					ID_TaiKhoan = model.ID,
					HoTen = "",
					Diachi = "",
					Email = "",
					SDT = "",
					AnhDaiDien = "",
				};
				db.KhachHangs.Add(a);
				db.SaveChanges();
			}
		}
		public List<string> GetMGG(int iduser)
		{
			List<string> li = new List<string>();
			var model = db.TaiKhoans.Where(x => x.ID == iduser).SingleOrDefault();
			var model1 = db.Temps.Where(x => x.ID_TK == model.ID).ToList();
			if (model1 != null)
			{
				foreach (var item in model1)
				{
					li.Add(item.Magiamgia.Ma);
				}
			}
			return li;
		}
		public int Changepassword(int id , string oldpass,string newpass, string confirm)
		{
			var model = db.TaiKhoans.Where(x => x.ID == id).SingleOrDefault();
			if(string.IsNullOrEmpty(oldpass) || string.IsNullOrEmpty(newpass) || string.IsNullOrEmpty(confirm))
			{
				return 0;
			}
			else
			{
				if(model.MatKhau != oldpass)
				{
					return 1;
				}
				else if(newpass != confirm)
				{
					return 2;
				}
				else
				{
					model.MatKhau = newpass;
					db.SaveChanges();
					return 3;
				}
			}
		}
		
	}
}
