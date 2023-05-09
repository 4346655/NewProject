using Models.DAO;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewProject.Controllers
{
    public class ProductDetailsController : Controller
    {
        // GET: ProductDetails
        public ActionResult Index(int id)
        {
            var pr = new ProductsDao();
            var sach = pr.SachDetail(id);
            return View(sach);
        }
        [HttpGet]
        public ActionResult Addcomment(int idsach,string noidung,bool andanh)
		{
            var pr = new ProductsDao();
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var id = kh.GetID(session.username);
           
            pr.addcomment(idsach, id, noidung,andanh);
            return RedirectToAction("Index", new { id = idsach });
		}
        public ActionResult Start(int idsach)
		{
            var pr = new ProductsDao();
            var avg = pr.AvgStart(idsach);
            ViewBag.NumberStart = avg;
            return View();

		}
        [HttpGet]
        public ActionResult Listcomment(int idsach)
		{
            var cm = new ProductsDao();
            var li = cm.GetComment(idsach);
            return View(li);
		}
    }
}