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
    public class CategoriesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Categories
        public ActionResult Index(string Searchstring, int page = 1, int pagesize = 20)
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
                    var dm = new CategoriesDao();
                    var list = dm.Pagelist_dm(Searchstring, page, pagesize);
                    ViewBag.Search = Searchstring;
                    return View(list);
                }
            }

        }
        public ActionResult Dele(int id)
        {
            var ct = new CategoriesDao();
            ct.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            ViewBag.ID_KhachHang = new SelectList(db.KhachHangs, "ID", "HoTen");
            ViewBag.ID_Sach = new SelectList(db.Saches, "ID", "TenSach");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_Sach,SoLuong,ID_KhachHang")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.GioHangs.Add(gioHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_KhachHang = new SelectList(db.KhachHangs, "ID", "HoTen", gioHang.ID_KhachHang);
            ViewBag.ID_Sach = new SelectList(db.Saches, "ID", "TenSach", gioHang.ID_Sach);
            return View(gioHang);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_KhachHang = new SelectList(db.KhachHangs, "ID", "HoTen", gioHang.ID_KhachHang);
            ViewBag.ID_Sach = new SelectList(db.Saches, "ID", "TenSach", gioHang.ID_Sach);
            return View(gioHang);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_Sach,SoLuong,ID_KhachHang")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_KhachHang = new SelectList(db.KhachHangs, "ID", "HoTen", gioHang.ID_KhachHang);
            ViewBag.ID_Sach = new SelectList(db.Saches, "ID", "TenSach", gioHang.ID_Sach);
            return View(gioHang);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GioHang gioHang = db.GioHangs.Find(id);
            db.GioHangs.Remove(gioHang);
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
