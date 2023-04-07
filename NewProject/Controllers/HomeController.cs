using Models.DAO;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewProject.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Box()
		{
			var dm = new CategoriesDao();
			var li = dm.List();
			return View(li);
		}
		public ActionResult Listpr()
		{
			var dm = new CategoriesDao();
			var li = dm.List();
			return View(li);
		}
		public ActionResult Listproduct(int id)
		{
			var pr = new ProductsDao();
			var li = pr.Sach_categoriesID(id);
			return View(li);
		}
		public ActionResult CartOrder()
		{
			var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
			if (session != null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Login");
			}
		}
	}
}