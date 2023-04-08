using Models.DAO;
using Models.DTO;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session != null)
            {
                var kh = new CustomersDao();
                var id = kh.GetID(session.username);
                var or = new OrdersDao();
                var list = or.List_IDKH(id);
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }
        public ActionResult TempPage(int idsach,int soluong)
		{
            var gh = new OrdersDao();
            var kh = new CustomersDao();
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var iduser = kh.GetID(session.username);
            var order = gh.TempGH(idsach,iduser,soluong);
            return View(order);
		}
        [HttpGet]
        public ActionResult AddtoOrder(int idsach,int idthanhtoan, int soluong,string MGG,string note )
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var iduser = kh.GetID(session.username);
            var or = new OrdersDao();
            or.CreateNew(idsach, iduser, idthanhtoan, soluong, MGG, note);
            return RedirectToAction("Index","Cart");
		}

        public ActionResult HuyDon(int idorder)
		{
            var or = new OrdersDao();
            or.HuyDon(idorder);
            return RedirectToAction("Index","Cart");
		}
        public ActionResult Rebuy(int idorder)
		{
            var or = new OrdersDao();
            or.Mualai(idorder);
            return RedirectToAction("Index", "Cart");
        }
    }
}