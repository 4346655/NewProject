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
		
		public ActionResult Searchbox(string tensach)
		{
			if(String.IsNullOrEmpty(tensach))
			{
				return RedirectToAction("Index");
			}
			else
			{
				var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
				if (session != null)
				{
					var kh = new CustomersDao();
					var id = kh.GetID(session.username);
					kh.addHistorySearch(id, tensach);
				}
					var pr = new ProductsDao();
				var li = pr.Searchbox(tensach);
				return View(li);
			}
		}
		public ActionResult HistorySearch()
		{
			var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
			if (session != null)
			{
				var kh = new CustomersDao();
				var id = kh.GetID(session.username);
				var li = kh.getListHistorySearch(id);
				return View(li);
			}
			else return View();
		}
	}
}