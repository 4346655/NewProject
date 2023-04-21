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
                if (res == 4)
                {
                    ModelState.AddModelError("", "Đăng kí thành công");
                }
                else if (res == 0)
                {
                    ModelState.AddModelError("", "Tên tài khoản đã tồn tại.");
                }
                else if(res==1)
				{
                    ModelState.AddModelError("", "Vui lòng nhập đủ thông tin");
                }
                else if (res == 2)
                {
                    ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu lỗi");
                }
                else if (res == 3)
                {
                    ModelState.AddModelError("", "Tên tài khoản , mật khẩu hơn 8 kí tự");
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