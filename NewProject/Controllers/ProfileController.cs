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
        
        [HttpGet]
        public ActionResult Suathongtin(string hoten, string diachi, string email , string sdt,string avt)
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var id = kh.GetID(session.username);
            kh.ChangeInfo(id, hoten, diachi, email, sdt, avt);
            return RedirectToAction("Index");
		}
       
     
        
        [HttpGet]
        public ActionResult Doimatkhau(string oldpass, string newpass,string confirm)
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var id = kh.GetID(session.username);
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
			else
			{
                ModelState.AddModelError("", "Đổi mật khẩu thành công");
            }
            return RedirectToAction("Index");
            
        }
    }
}