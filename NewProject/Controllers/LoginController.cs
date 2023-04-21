using Models.DAO;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModels login)
        {
            if (ModelState.IsValid)
            {
                var user = new AccountDao();
                var res = user.login(login.username, login.password);
                if (res == 5)
                {
                    Session.Add(LoginConstants.LOGIN_SESSION, login);
                    var cs = new CustomersDao();
                    var ac = cs.GetDetailByUsername(login.username);
                    if (ac.TaiKhoan.ID_LoaiTK == 2)
                    {
                        return RedirectToAction("Index", "Home1");
                    }
					else
					{
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if(res==0)
				{
                    ModelState.AddModelError("", "Vui lòng nhập đủ thông tin.");
                }
                else if(res==1)
				{
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu bị lỗi.");
                }
				else if(res==2)
				{
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (res == 3)
                {
                    ModelState.AddModelError("", "Sai mật khẩu ");
                }
                else if (res == 4)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa.");
                }
            }
            return View();
        }
        public ActionResult Forgetpassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgetpassword(string username, string sodienthoai)
        {
            var ac = new AccountDao();
            var mk = ac.quenmatkhau(username, sodienthoai);
            if (mk == "0")
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại.");
            }
            else if (mk == "1")
            {
                ModelState.AddModelError("", "Số điện thoại không chính xác.");
            }
            else if (mk == "2")
            {
                ModelState.AddModelError("", "Nhập đủ thông tin.");
            }
            else
            {
                ViewBag.Alert = mk;
                username = "";
                sodienthoai = "";
            }
            return View();
        }
        public ActionResult Logout()
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            Session.Remove(session.username);
            Session.Remove(session.password);
            return RedirectToAction("Index");

        }
    }
}