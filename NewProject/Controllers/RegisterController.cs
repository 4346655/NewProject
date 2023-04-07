using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewProject.Models;

namespace NewProject.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RegisterModels register)
        {
            if (ModelState.IsValid)
            {
                var user = new AccountDao();
                var res = user.register(register.username, register.password, register.confirm);
                if (res == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (res == 0)
                {
                    ModelState.AddModelError("", "Tên tài khoản đã tồn tại.");
                }
				else
				{
                    ModelState.AddModelError("", "Mật khẩu xác nhận không chính xác.");
                }
            }
            return View();
        }
    }
}