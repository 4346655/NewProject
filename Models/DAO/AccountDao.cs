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
		public List<TaiKhoan> List()
		{
			return db.TaiKhoans.ToList();
		}
		public int InsertForFacebook(string username,string password)
		{
			if(db.TaiKhoans.Count(x=>x.TenTaiKhoan == username) ==0)
			{
				string pass = Hash_MD5.EncryptMD5(password);
				TaiKhoan a = new TaiKhoan
				{
					ID_LoaiTK = 1,
					TenTaiKhoan = username,
					MatKhau = pass,
					Trangthai = true,
				};
				db.TaiKhoans.Add(a);

				db.SaveChanges();
				CreateNew(username);
				Temp b = new Temp
				{
					ID_TK = a.ID,
					ID_MGG = 1,

				};
				db.Temps.Add(b);
				db.SaveChanges();
				return 1;
			}
			else
			{
				return 0;
			}
		}
		public int login(string username,string password)
		{
			if(String.IsNullOrEmpty(username)|| String.IsNullOrEmpty(password))
			{
				return 0; //tai khoan hoac mat khau trong
			}
			else
			{
					int key = 0;
					for(int i=0;i<username.Length;i++)
					{
						if((int)username[i] ==32)
						{
							key = 1; // ten tai khoan chua dau cach
						break;
						}
					}
					if (key != 0) return key;
					for (int i = 0; i < password.Length; i++)
					{
						if ((int)password[i] == 32)
						{
							key=1; // mat khau chua dau cach
							break;
						}
					}
					if (key != 0) return key;

					var model = db.TaiKhoans.Where(x => x.TenTaiKhoan == username ).SingleOrDefault();
					if (model == null)
					{
						key= 2; // khoong co tai khoan trong he thong
					}
					else
					{
						if (model.MatKhau != Hash_MD5.EncryptMD5( password))
						{
							key = 3; // sai mat khau
						}
						else
						{
							if (model.Trangthai == false)
							{
								key = 4; //bi khoa tai khoan
							}
							else
							{
								key = 5; // thanh cong
							}
						}
					}
				return key;
			}
		}
		public int checkregister(string username, string newpassword, string confirm)
		{
			var model = db.TaiKhoans.Where(x => x.TenTaiKhoan == username).SingleOrDefault();
			if (model != null)
			{
				return 0; // da co ten tai khoan nay
			}
			else
			{
				int key = 0;
				if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(newpassword) || String.IsNullOrEmpty(confirm))
				{
					key = 1;// nhạp chưa đủ thông tin
				}
				if (key != 0) return key;

				for (int i = 0; i < username.Length; i++)
				{
					if ((int)username[i] == 32)
					{
						key = 2; // ten tai khoan chua dau cach
						break;
					}
				}
				if (key != 0) return key;

				for (int i = 0; i < newpassword.Length; i++)
				{
					if ((int)newpassword[i] == 32)
					{
						key = 2; // mat khau chua dau cach
						break;
					}
				}
				if (key != 0) return key;

				if (string.Compare(newpassword, confirm) == 0 && newpassword != "")
				{
					if (newpassword.Length < 8 || username.Length < 8)
					{
						key = 3; // tai khoan,mat khau >8 kí tụ
					}
					else
					{
						key = 4; // tai khoan, mat khau >8 ki tu
					}
				}
				else
				{
					key =5; // khác  mật khẩu xác nhận
				}
				return key;
			}
		}
		public int register(string username, string newpassword, string confirm)
		{
			int key = checkregister(username, newpassword, confirm);
			
			if (key == 4)
			{
				string pass = Hash_MD5.EncryptMD5(newpassword);
				TaiKhoan a = new TaiKhoan {
					ID_LoaiTK = 1,
					TenTaiKhoan = username,
					MatKhau =pass,
					Trangthai = true,
				};
				db.TaiKhoans.Add(a);
				
				db.SaveChanges();
				CreateNew(username);
				Temp b = new Temp
				{
					ID_TK = a.ID,
					ID_MGG = 1,

				};
				db.Temps.Add(b);
				db.SaveChanges();
				return key;
			}
			else {
				return key;
			}
		}
		public int Byname(string username)
		{
			var model = db.TaiKhoans.Where(x => x.TenTaiKhoan == username).SingleOrDefault();
			if (model.ID_LoaiTK == 1)
				return 1;
			else if (model.ID_LoaiTK == 2)
				return 2;
			else return 3;
		}
		public string quenmatkhau(string username, string email)
		{
			if (username != "" && email != "")
			{
				var model = db.TaiKhoans.Where(x => x.TenTaiKhoan == username).Single();
				if (model == null)
				{
					return "0";
				}
				else
				{
					var model1 = db.KhachHangs.Where(x => x.TaiKhoan.TenTaiKhoan == username).SingleOrDefault();
					if (model1.Email != email)
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
						model.MatKhau = Hash_MD5.EncryptMD5( randomSDT);
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
					AnhDaiDien = "/Uploads/images/Hinh-Gau-Truc-Cute-Chibi-de-thuong-nhat.jpg",
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
		public List<Temp> GetListMGG(int iduser)
		{
			
			var model = db.TaiKhoans.Where(x => x.ID == iduser).SingleOrDefault();
			var model1 = db.Temps.Where(x => x.ID_TK == model.ID).ToList();
			return model1;
		}
		public int Changepassword(int id , string oldpass,string newpass, string confirm)
		{
			oldpass = Hash_MD5.EncryptMD5(oldpass);
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
					if (newpass.Length < 8)
					{
						return 4;
					}
					else
					{
						for (int i = 0; i < newpass.Length; i++)
						{
							if ((int)newpass[i] == 32)
							{
								return  5; // mat khau chua dau cach
								break;
							}
						}
						model.MatKhau = Hash_MD5.EncryptMD5(newpass);
						db.SaveChanges();
						return 3;
					}
				}
			}
		}
		
	}
}
