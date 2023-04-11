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
        public ActionResult Index(string Searchstring, bool trangthai, int page = 1, int pagesize = 20)
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
                    var list = dm.Pagelist_dm(Searchstring, trangthai, page, pagesize);
                    ViewBag.Search = Searchstring;
                    return View(list);
                }
            }

        }
        public ActionResult Dele(int id)
        {
            var ct = new CategoriesDao();
            ct.Delete(id);
            return RedirectToAction("Index", new { trangthai = true });
        }
        public ActionResult ChangeStatus(int idls)
        {
            var ct = new CategoriesDao();
            ct.ChangeStatus(idls);
            return RedirectToAction("Index", new { trangthai = true });
        }
        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai_Sach loai_Sach = db.Loai_Sach.Find(id);
            if (loai_Sach == null)
            {
                return HttpNotFound();
            }
            return View(loai_Sach);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Loaisach,Trangthai")] Loai_Sach loai_Sach)
        {
            if (ModelState.IsValid)
            {
                db.Loai_Sach.Add(loai_Sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loai_Sach);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai_Sach loai_Sach = db.Loai_Sach.Find(id);
            if (loai_Sach == null)
            {
                return HttpNotFound();
            }
            return View(loai_Sach);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Loaisach,Trangthai")] Loai_Sach loai_Sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loai_Sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loai_Sach);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai_Sach loai_Sach = db.Loai_Sach.Find(id);
            if (loai_Sach == null)
            {
                return HttpNotFound();
            }
            return View(loai_Sach);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loai_Sach loai_Sach = db.Loai_Sach.Find(id);
            db.Loai_Sach.Remove(loai_Sach);
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
