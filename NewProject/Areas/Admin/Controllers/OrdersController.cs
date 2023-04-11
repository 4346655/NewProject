using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.DTO;
using NewProject.Models;

namespace NewProject.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Orders
        public ActionResult Index(string Searchstring, int trangthai, int page = 1, int pagesize = 20 )
        {
            var ac = new AccountDao();
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session == null)
            {
                return RedirectToAction("Error", "Home1");
            }
            else
            {
                var account = ac.Byname(session.username);
                if (account != 2)
                {
                    return RedirectToAction("Error", "Home1");
                }
                else
                {
                    var dh = new OrdersDao();
                    var list = dh.Pagelist_dh(Searchstring, page, pagesize,trangthai);
                    ViewBag.Search = Searchstring;
                    return View(list);
                }
            }

        }
        public ActionResult list()
		{
            var dh = new OrdersDao();
            var li = dh.list();
            return View(li);
		}
        public ActionResult HuyDonAdmin(int idor)
		{
            var or = new OrdersDao();
            or.HuyDon(idor);
            return RedirectToAction("Index", new {trangthai =0});
		}
        public ActionResult KhoiphucAdmin(int idor)
        {
            var or = new OrdersDao();
            or.Mualai(idor);
            return RedirectToAction("Index", new { trangthai = 1 });
        }
        public ActionResult ChangeStatus(int idor)
		{
            var or = new OrdersDao();
            or.ChangeStatus(idor);
            int trangthai = or.getStatus(idor);
            return RedirectToAction("Index" ,new { trangthai = trangthai });
		}

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.ID_KhachHang = new SelectList(db.KhachHangs, "ID", "HoTen");
            ViewBag.ID_Sach = new SelectList(db.Saches, "ID", "TenSach");
            ViewBag.ID_ThanhToan = new SelectList(db.ThanhToans, "ID", "TenThanhToan");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_KhachHang,ID_Sach,ID_ThanhToan,SoLuong,Ma_GG,Note,Tong_gia,Trang_thai")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_KhachHang = new SelectList(db.KhachHangs, "ID", "HoTen", donHang.ID_KhachHang);
            ViewBag.ID_Sach = new SelectList(db.Saches, "ID", "TenSach", donHang.ID_Sach);
            ViewBag.ID_ThanhToan = new SelectList(db.ThanhToans, "ID", "TenThanhToan", donHang.ID_ThanhToan);
            return View(donHang);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_KhachHang = new SelectList(db.KhachHangs, "ID", "HoTen", donHang.ID_KhachHang);
            ViewBag.ID_Sach = new SelectList(db.Saches, "ID", "TenSach", donHang.ID_Sach);
            ViewBag.ID_ThanhToan = new SelectList(db.ThanhToans, "ID", "TenThanhToan", donHang.ID_ThanhToan);
            return View(donHang);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_KhachHang,ID_Sach,ID_ThanhToan,SoLuong,Ma_GG,Note,Tong_gia,Trang_thai")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_KhachHang = new SelectList(db.KhachHangs, "ID", "HoTen", donHang.ID_KhachHang);
            ViewBag.ID_Sach = new SelectList(db.Saches, "ID", "TenSach", donHang.ID_Sach);
            ViewBag.ID_ThanhToan = new SelectList(db.ThanhToans, "ID", "TenThanhToan", donHang.ID_ThanhToan);
            return View(donHang);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
            db.SaveChanges();
            return RedirectToAction("Index",new { trangthai=0});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
