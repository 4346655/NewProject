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
        public bool Phanquyen()
        {
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session == null)
            {

                return false;

            }
            else
            {
                var account = new AccountDao();
                var id = account.Byname(session.username);
                if (id != 2)
                {
                    return false;

                }
            }
            return true;


        }
        // GET: Admin/Home1
        public ActionResult Index()
        {
            if (Phanquyen())
            {

                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Account_info()
		{
            var account = new AccountDao();
            return View(account.List());

		}
        public ActionResult Product_info()
		{
            var product = new ProductsDao();
            return View(product.List());
        }
        public ActionResult Order_info()
        {
            var order = new OrdersDao();
            return View(order.List());
        }
        public ActionResult Category_info()
        {
            var category = new CategoriesDao();
            return View(category.List0());
        }
        public ActionResult Category_info_1()
        {
            var category = new CategoriesDao();
            return View(category.List0());
        }
        public ActionResult Category_1(int idloaisach)
		{
            var product = new ProductsDao();
            var list1 = product.List(idloaisach);
            var list = product.List();
            ViewBag.Loaisach = list[0].Loai_Sach.Loaisach;
            int percent =  ( list1.Count() * 100 )/ list.Count();

            ViewBag.list = (int)percent;
            return View(list1);
        }
        public ActionResult Percent_Order()
		{
            var order = new OrdersDao();
            var list = order.List_distinct();
            return View(list);
		}
        public ActionResult Percent_Order1(string tensach)
        {
            var order = new OrdersDao();
            var list = order.List(tensach);
            var list1 = order.List();
            ViewBag.list = list.Count()*100 / list1.Count();
            return View(list);
        }
        public ActionResult Percent_typeOrder()
		{
            var order = new OrdersDao();
            var list = order.List();
            return View(list);
		}
    }
}