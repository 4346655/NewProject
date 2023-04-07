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
    public class ProductsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Products
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
                    var pr = new ProductsDao();
                    var list = pr.Pagelist_sa(Searchstring, page, pagesize);
                    ViewBag.Search = Searchstring;
                    return View(list);
                }
            }


        }
        public ActionResult Statefalse(string Searchstring, int page = 1, int pagesize = 20)
        {
            var pr = new ProductsDao();
            var list = pr.Pagelist_false(Searchstring, page, pagesize);
            ViewBag.Search = Searchstring;
            return View(list);
        }
        public ActionResult Restore(int id)
        {
            var pr = new ProductsDao();
            pr.SetStates(id, true);
            return RedirectToAction("Statefalse");
        }
        public ActionResult Del(int id)
        {
            var pr = new ProductsDao();
            pr.SetStates(id, false);
            return RedirectToAction("Index");
        }
        public ActionResult Dele(int id)
        {
            var pr = new ProductsDao();
            pr.Delete(id);
            return RedirectToAction("Statefalse");
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.ID_LoaiSach = new SelectList(db.Loai_Sach, "ID", "Loaisach");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_LoaiSach,TenSach,Anh,SoLuong,SoLuong_DaBan,Mota,Gia,Gia_giam,Trangthai,NhaXuatBan,TacGia")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_LoaiSach = new SelectList(db.Loai_Sach, "ID", "Loaisach", sach.ID_LoaiSach);
            return View(sach);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LoaiSach = new SelectList(db.Loai_Sach, "ID", "Loaisach", sach.ID_LoaiSach);
            return View(sach);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_LoaiSach,TenSach,Anh,SoLuong,SoLuong_DaBan,Mota,Gia,Gia_giam,Trangthai,NhaXuatBan,TacGia")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_LoaiSach = new SelectList(db.Loai_Sach, "ID", "Loaisach", sach.ID_LoaiSach);
            return View(sach);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
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
