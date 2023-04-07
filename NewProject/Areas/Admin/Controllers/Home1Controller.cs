using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewProject.Models;

namespace NewProject.Areas.Admin.Controllers
{
    public class Home1Controller : Controller
    {
        // GET: Admin/Home1
        public ActionResult Index()
        {
            var ac = new AccountDao();
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session == null)
            {
                return RedirectToAction("Error");
            }
            else
            {
                var account = ac.Byname(session.username);
                if (account != 2)
                {
                    return RedirectToAction("Error");
                }
                else
                {
                    return View();
                }
            }
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}