using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewProject.Models;

namespace NewProject.Views.Home
{
    public class CartController : Controller
    {
        // GET: Cart
     
        public ActionResult Index()
        {

            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session != null)
            {
                var cs = new CustomersDao();
                var id = cs.GetID(session.username);
                var gh = new CartDao();
                var list = gh.GioHang_IDUser(id);
                return View(list);
            }
			else
			{
                return RedirectToAction("Index", "Login");
			}
        }
        public ActionResult Update(int id, int soluong)
        {
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var gh = new CartDao();
                gh.Update(id, soluong);
                return RedirectToAction("Index");
            }
        }
        public ActionResult AddtoCart(int idsach,int soluong)
		{
            var gh = new CartDao();
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var cs = new CustomersDao();
                var id = cs.GetID(session.username);
                gh.Themsanpham(idsach, id, soluong);
                return RedirectToAction("Index");
            }
        }
        public ActionResult DeleteCart(int id)
		{
            var gh = new CartDao();
            gh.Xoasanpham(id);
            return RedirectToAction("Index");
        }
    }
}