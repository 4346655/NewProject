using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewProject.Models;

namespace NewProject.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        
        public ActionResult Index()
        {
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session != null)
            {
                var ac = new AccountDao();
                ac.CreateNew(session.username);
                var kh = new CustomersDao();
                var khachhang = kh.GetDetailByUsername(session.username);
                return View(khachhang);
            }
			else
			{
                return RedirectToAction("Index", "Login");
			}
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Suathongtin(string hoten, string diachi, string email , string sdt,string avt)
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var detail = kh.GetDetailByUsername(session.username);
            var id = kh.GetID(session.username);
            int key = kh.ChangeInfo(id, hoten, diachi, email, sdt, avt);
            if(key==0)
			{
                ModelState.AddModelError("", "Thay đổi thành công");
			}
			else if(key==1) 
            {
                ModelState.AddModelError("", "Họ và tên của bạn chứa kí tự không hợp lệ");
            }
            else if (key == 3)
            {
                ModelState.AddModelError("", "Email không hợp lệ");
            }
            else if(key==2)
			{
                ModelState.AddModelError("", "Số điện thoại chứa kí tự không hợp lệ");
			}
			else
			{
                ModelState.AddModelError("", "Vui lòng nhập đúng số điện thoại");
            }             
            return View(detail);
		}
        public ActionResult Doimatkhau()
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var detail = kh.GetDetailByUsername(session.username);
            return View(detail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Doimatkhau(string oldpass, string newpass,string confirm)
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var id = kh.GetIDTK(session.username);
            var ac = new AccountDao();
            var key = ac.Changepassword(id, oldpass, newpass, confirm);
            if(key==0)
			{
                ModelState.AddModelError("", "Nhập đầy đủ thông tin");
			}
			else if(key==1)
			{
                ModelState.AddModelError("", "Mật khẩu cũ không chính xác");
            }
            else if(key==2)
			{
                ModelState.AddModelError("", "Mật khẩu không khớp");
            }
            else if (key == 4)
            {
                ModelState.AddModelError("", "Mật khẩu nhiều hơn 8 kí tự");
            }
            else if (key == 5)
            {
                ModelState.AddModelError("", "Mật khẩu chứa kí tự không cho phép");
            }
            else
			{
                ModelState.AddModelError("", "Đổi mật khẩu thành công");
            }

            if (session != null)
            {
                ac.CreateNew(session.username);
                var khachhang = kh.GetDetailByUsername(session.username);
                return View(khachhang);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}