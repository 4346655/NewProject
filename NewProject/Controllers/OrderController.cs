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
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var iduser = kh.GetID(session.username);
                DonHang order = gh.TempGH(idsach, iduser, soluong);
                return View(order);
            }
		}
        public ActionResult TempUser()
        {
            var kh = new CustomersDao();
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var user = kh.GetDetailByUsername(session.username);
                return View(user);
            }
        }
      
        public ActionResult Test_TempProduct( List<int> idsach)
        {
            if(idsach==null)
			{
                return HttpNotFound();
			}
            {

                var product = new ProductsDao();
                var listproduct = product.listIDRange(idsach);
                return View(listproduct);
            }
           

        }

        public ActionResult TempProduct(int idsach)
		{
            var pr = new ProductsDao();
            var product = pr.SachDetail(idsach);
            return View(product);
		}
        public ActionResult TempVoucher()
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var customer = new CustomersDao();
            var id = customer.GetDetailByUsername(session.username).TaiKhoan.ID;
            var account = new AccountDao();
            var listvoucher = account.GetListMGG(id);
            return View(listvoucher);
		}
        [HttpGet]
        public ActionResult AddtoOrder(int idsach,int idthanhtoan, int soluong,string MGG,string note )
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            if (session == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var iduser = kh.GetID(session.username);
                var or = new OrdersDao();
                or.CreateNew(idsach, iduser, idthanhtoan, soluong, MGG, note);
                return RedirectToAction("Index", "Cart");
            }
		}

        public ActionResult HuyDon(int idorder)
		{
            var or = new OrdersDao();
            or.HuyDon(idorder);
            return RedirectToAction("Index","Order");
		}
        public ActionResult Rebuy(int idorder)
		{
            var or = new OrdersDao();
            or.Mualai(idorder);
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult HistoryBuyandCancel()
		{
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session != null)
            {
                var kh = new CustomersDao();
                var id = kh.GetID(session.username);
                var or = new OrdersDao();
                var list = or.Historysearch(id);
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}