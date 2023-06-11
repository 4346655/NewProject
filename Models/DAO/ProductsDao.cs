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
			setStatus();
		}
		// khi bỏ thùng rác / hết số lượng : dừng bán
		public void setStatus()
		{
			var mode = db.Saches.ToList();
			foreach(var item in mode)
			{
				if(item.DeleteStatus == true)
				{
					item.Trangthai = false;
				}
				if(item.SoLuong<=0)
				{
					item.Trangthai = false;
				}
			}
			db.SaveChanges();
		}
		public List<Sach> List(int idloaisach)
		{
			return db.Saches.Where(x => x.ID_LoaiSach == idloaisach).ToList();
		}
		public List<Sach> List()
		{
			return db.Saches.ToList();
		}
		
		// lấy danh sách sản phẩm
		public IEnumerable<Sach> DanhSachSanPham(string Searchstring, int page, int pagesize)
		{
			IOrderedQueryable<Sach> model = db.Saches.Where(x=>x.DeleteStatus == false).OrderByDescending(x => x.Loai_Sach.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.TenSach.Contains(Searchstring)).OrderByDescending(x => x.Loai_Sach.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
		// tìm kiếm sản phẩm : client interfaces
		public List<Sach> TimKiemSach(string tensach)
		{
			return db.Saches.Where(x => x.TenSach.Contains(tensach) && x.Trangthai ==true).OrderByDescending(x=>x.ID_LoaiSach).ToList();
		}

		//khi xóa 1 sản phẩm : xóa các phần tử nối với khóa ngoại
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

		// lấy ra danh sách đang bỏ thùng rác
		public IEnumerable<Sach> DanhSachThungRac(string Searchstring, int page, int pagesize)
		{
			IOrderedQueryable<Sach> model = db.Saches.Where(x => x.DeleteStatus == true).OrderByDescending(x => x.Loai_Sach.ID);
			if (!string.IsNullOrEmpty(Searchstring))
			{
				model = model.Where(x => x.TenSach.Contains(Searchstring)).OrderByDescending(x => x.Loai_Sach.ID);
			}
			return model.ToPagedList(page, pagesize);
		}
		// thay đổi trạng thái : true -> đang được hoạt dộng : false -> đang bỏ thùng rác
		public void Bo_KhoiPhuc_SanPham(int id, bool a)
		{
			var model = db.Saches.Find(id);
			model.DeleteStatus = a;
			db.SaveChanges();
		}

		// lấy danh sách sách theo danh mục
		public List<Sach> Sach_categoriesID(int idloaisach)
		{
			var model = db.Saches.Where(x => x.ID_LoaiSach == idloaisach && x.Trangthai == true && x.SoLuong>0).ToList();
			return model;
		}

		// lấy chi tiết sách
		public Sach SachDetail(int id)
		{
			return db.Saches.Find(id);
		}

		// lấy các bình luận của sách
		public List<BinhLuan> GetComment(int idsach)
		{ 
			return db.BinhLuans.Where(x => x.ID_Sach == idsach).ToList();	
		}

		// thêm bình luận
		public void addcomment(int idsach,int iduser,string noidung,bool andanh)
		{
			
			BinhLuan a = new BinhLuan
			{
				ID_KhachHang = iduser,
				ID_Sach = idsach,
				NoiDung = noidung,
				AnDanh = andanh,
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
				avg += (int)item.Sosao;
			}
			return (double)avg / (model.Count);
		}

		// lấy danh sách theo giỏ hàng
		public List<Sach> listIDRange(List<int> idsach)
		{
			List<Sach> model = new List<Sach>();
			foreach(var item in idsach)
			{
				var itemtemp = db.Saches.Where(x => x.ID == item).SingleOrDefault();
				model.Add(itemtemp);
			}
			return model;

		}

		// thay đổi dừng / bán sách
		public void Ban_DungBan(int idsach)
		{
			var model = db.Saches.Where(x => x.ID == idsach).SingleOrDefault();
			model.Trangthai = !model.Trangthai;
			db.SaveChanges();

		}
		public List<Sach> GetListSach_IDcategory(int iddanhmuc)
		{
			return db.Saches.Where(x => x.Loai_Sach.ID == iddanhmuc).ToList();
		}
	}
}
