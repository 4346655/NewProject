using Common;
using Models.DAO;
using Models.DTO;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session != null)
            {
                var kh = new CustomersDao();
                var id = kh.GetID(session.username);
                var or = new OrdersDao();
                var list = or.List_IDKH(id);
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }
        public ActionResult Listproduct()
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var cs = new CustomersDao();
            var id = cs.GetID(session.username);
            var gh = new CartDao();
            var list = gh.GioHang_IDUser(id);
            return View(list);
        }
        public ActionResult PageOrder(int iduser)
		{
            var customer = new CustomersDao();
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var infocustomer = customer.GetDetailByUsername(session.username);

            ViewBag.HoTen = infocustomer.HoTen;
            ViewBag.SDT = infocustomer.SDT;
            ViewBag.DiaChi = infocustomer.Diachi;
            ViewBag.Email = infocustomer.Email;

            var gh = new CartDao();
            var list = gh.GioHang_IDUser(iduser);
            int total = 0;
            foreach(var item in list)
			{
                total += ((int)item.Sach.Gia * (int)item.SoLuong);
			}
            ViewBag.Total = total.ToString();
            return View(list);
		}
        public ActionResult PageOrder_Oneproduct(int idsach,int soluong)
        {
            var customer = new CustomersDao();
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var infocustomer = customer.GetDetailByUsername(session.username);

            ViewBag.HoTen = infocustomer.HoTen;
            ViewBag.SDT = infocustomer.SDT;
            ViewBag.DiaChi = infocustomer.Diachi;
            ViewBag.Email = infocustomer.Email;
            ViewBag.SoLuong = soluong.ToString();
            var pr = new ProductsDao();
            var sach = pr.SachDetail(idsach);
            ViewBag.GiaGiam = sach.Gia_giam.ToString();
            ViewBag.Total = ((sach.Gia-sach.Gia_giam)*soluong).ToString();
            return View(sach);
        }
        public ActionResult OrderProduct(int phuongthuc, string hoten, string sdt,string diachi,string email , string note)
		{

            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var id = kh.GetID(session.username);
            var gh = new CartDao();
            var list = gh.GioHang_IDUser(id);
            var order1 = new OrdersDao();
            foreach(var item in list)
			{
                var order = order1.CreateNew((int)item.ID_Sach, id, phuongthuc, (int)item.SoLuong, "", note, hoten, sdt, diachi, email);

                var model = kh.GetDetailByUsername(session.username);

                String content = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/neworder.html"));
                content = content.Replace("{{CustomerName}}", model.HoTen);
                content = content.Replace("{{IDORDER}}", model.ID.ToString());
                content = content.Replace("{{Time}}", DateTime.Now.ToString());
                content = content.Replace("{{Address}}", model.Diachi);
                content = content.Replace("{{SDT}}", model.SDT);
                content = content.Replace("{{Email}}", model.Email);
                content = content.Replace("{{GIA}}", order.Sach.Gia.ToString());
                content = content.Replace("{{NAMEPRODUCT}}", order.Sach.TenSach);
                content = content.Replace("{{TONGGIA}}", order.Tong_gia.ToString());
                content = content.Replace("{{SOLUONG}}", order.SoLuong.ToString());

                Common.MailHelpper.SendMail("LAVADO", "Đơn hàng #" + model.ID, content.ToString(), model.Email);


                String content1 = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/AdminMail.html"));
                content1 = content1.Replace("{{CustomerName}}", model.HoTen);
                content1 = content1.Replace("{{IDORDER}}", model.ID.ToString());
                content1 = content1.Replace("{{Time}}", DateTime.Now.ToString());
                content1 = content1.Replace("{{Address}}", model.Diachi);
                content1 = content1.Replace("{{SDT}}", model.SDT);
                content1 = content1.Replace("{{Email}}", model.Email);
                content1 = content1.Replace("{{GIA}}", order.Sach.Gia.ToString());
                content1 = content1.Replace("{{NAMEPRODUCT}}", order.Sach.TenSach);
                content1 = content1.Replace("{{TONGGIA}}", order.Tong_gia.ToString());
                content1 = content1.Replace("{{SOLUONG}}", order.SoLuong.ToString());

                string email1 = ConfigurationManager.AppSettings["Email"];

                Common.MailHelpper.SendMail("LAVADO", "Đơn hàng #" + model.ID, content1.ToString(), email1);

                ThongBao a = new ThongBao()
                {
                    TieuDe = "Đơn hàng mới từ " + model.Email,
                    NoiDung = "Khách hàng " + model.HoTen + " vừa đặt 1 đơn hàng : " + order.Sach.TenSach,
                    EmailUser = model.Email,
                };
                ThongbaoDao tbd = new ThongbaoDao();
                tbd.Add(a);

            }                

            return RedirectToAction("Index");
		}
        public ActionResult OrderProduct_Oneproduct(int phuongthuc, string hoten, string sdt, string diachi, string email, string note)
        {

            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var id = kh.GetID(session.username);
            var gh = new CartDao();
            var list = gh.GioHang_IDUser(id);
            var order1 = new OrdersDao();
            foreach (var item in list)
            {
                var order = order1.CreateNew((int)item.ID_Sach, id, phuongthuc, (int)item.SoLuong, "", note, hoten, sdt, diachi, email);

                var model = kh.GetDetailByUsername(session.username);

                String content = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/neworder.html"));
                content = content.Replace("{{CustomerName}}", model.HoTen);
                content = content.Replace("{{IDORDER}}", model.ID.ToString());
                content = content.Replace("{{Time}}", DateTime.Now.ToString());
                content = content.Replace("{{Address}}", model.Diachi);
                content = content.Replace("{{SDT}}", model.SDT);
                content = content.Replace("{{Email}}", model.Email);
                content = content.Replace("{{GIA}}", order.Sach.Gia.ToString());
                content = content.Replace("{{NAMEPRODUCT}}", order.Sach.TenSach);
                content = content.Replace("{{TONGGIA}}", order.Tong_gia.ToString());
                content = content.Replace("{{SOLUONG}}", order.SoLuong.ToString());

                Common.MailHelpper.SendMail("LAVADO", "Đơn hàng #" + model.ID, content.ToString(), model.Email);


                String content1 = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/AdminMail.html"));
                content1 = content1.Replace("{{CustomerName}}", model.HoTen);
                content1 = content1.Replace("{{IDORDER}}", model.ID.ToString());
                content1 = content1.Replace("{{Time}}", DateTime.Now.ToString());
                content1 = content1.Replace("{{Address}}", model.Diachi);
                content1 = content1.Replace("{{SDT}}", model.SDT);
                content1 = content1.Replace("{{Email}}", model.Email);
                content1 = content1.Replace("{{GIA}}", order.Sach.Gia.ToString());
                content1 = content1.Replace("{{NAMEPRODUCT}}", order.Sach.TenSach);
                content1 = content1.Replace("{{TONGGIA}}", order.Tong_gia.ToString());
                content1 = content1.Replace("{{SOLUONG}}", order.SoLuong.ToString());

                string email1 = ConfigurationManager.AppSettings["Email"];

                Common.MailHelpper.SendMail("LAVADO", "Đơn hàng #" + model.ID, content1.ToString(), email1);

                ThongBao a = new ThongBao()
                {
                    TieuDe = "Đơn hàng mới từ " + model.Email,
                    NoiDung = "Khách hàng " + model.HoTen + " vừa đặt 1 đơn hàng : " + order.Sach.TenSach,
                    EmailUser = model.Email,
                };
                ThongbaoDao tbd = new ThongbaoDao();
                tbd.Add(a);

            }

            return RedirectToAction("Index");
        }

        public ActionResult TempVoucher()
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var customer = new CustomersDao();
            var id = customer.GetDetailByUsername(session.username).TaiKhoan.ID;
            var account = new AccountDao();
            var listvoucher = account.GetListMGG(id);
            return View(listvoucher);
		}


        

        public ActionResult HuyDon(int idorder)
		{
            var or = new OrdersDao();
            var model = or.Donhang(idorder);




            String content = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/CancelOrder.html"));
            content = content.Replace("{{CustomerName}}", model.KhachHang.HoTen);
            content = content.Replace("{{IDORDER}}",idorder.ToString());
            content = content.Replace("{{Time}}", DateTime.Now.ToString());
            content = content.Replace("{{Address}}", model.KhachHang.Diachi);
            content = content.Replace("{{SDT}}", model.KhachHang.SDT);
            content = content.Replace("{{Email}}", model.KhachHang.Email);
            content = content.Replace("{{TONGGIA}}", model.Tong_gia.ToString());
            content = content.Replace("{{NAMEPRODUCT}}", model.Sach.TenSach);
            content = content.Replace("{{GIA}}", model.Sach.Gia.ToString());
            content = content.Replace("{{SOLUONG}}", model.SoLuong.ToString());


            Common.MailHelpper.SendMail("LAVADO", "Đơn hàng #" + model.ID, content.ToString(), model.KhachHang.Email);

            or.HuyDon(idorder);
            return RedirectToAction("Index","Order");
		}
        public ActionResult Rebuy(int idorder)
		{
            var or = new OrdersDao();
            or.Mualai(idorder);
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult HistoryBuyandCancel()
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session != null)
            {
                var kh = new CustomersDao();
                var id = kh.GetID(session.username);
                var or = new OrdersDao();
                var list = or.Historysearch(id);
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        public ActionResult Respond(int idsach,int iduser,int sao,string binhluan,string tieude,string nhanxet,int andanh,string email)
		{
            // xu li chat luong danh gia
            var product_star = new ProductsDao();
            product_star.AddStart(idsach, sao);
            // xulibinhluan
            bool Andanh = (andanh == 1) ? true : false;
            product_star.addcomment(idsach, iduser, binhluan, Andanh);

            // xu li bao cao
            if (!String.IsNullOrEmpty(nhanxet))
            {
                ThongBao thongbao = new ThongBao
                {
                    EmailUser = email,
                    TieuDe = tieude,
                    NoiDung = nhanxet,
                };
                var thongbaodao = new ThongbaoDao();
                thongbaodao.Add(thongbao);
                
            }

            return RedirectToAction("Index");
		}
    }
}