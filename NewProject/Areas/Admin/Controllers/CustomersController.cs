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
    public class CustomersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Customers
      
        public ActionResult Index(string Searchstring, int page = 1, int pagesize = 5)
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
                    var kh = new CustomersDao();
                    var list = kh.Pagelist_kh(Searchstring, page, pagesize);
                    ViewBag.Search = Searchstring;
                    return View(list);
                }
            }

        }
        public ActionResult offactive(int id )
		{
            var cs = new CustomersDao();
            cs.offactive(id);
            return RedirectToAction("Index","Home1");
		}

        // GET: Admin/Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "TenTaiKhoan");
            return View();
        }
       
        // POST: Admin/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_TaiKhoan,HoTen,Diachi,Email,SDT,AnhDaiDien")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "TenTaiKhoan", khachHang.ID_TaiKhoan);
            return View(khachHang);
        }

        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "TenTaiKhoan", khachHang.ID_TaiKhoan);
            return View(khachHang);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_TaiKhoan,HoTen,Diachi,Email,SDT,AnhDaiDien")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "TenTaiKhoan", khachHang.ID_TaiKhoan);
            return View(khachHang);
        }

        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
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
