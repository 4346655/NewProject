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
    public class VouchersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Vouchers
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
                    var mgg = new VoucherDao();
                    var list = mgg.Pagelist_mgg(Searchstring, page, pagesize);
                    ViewBag.Search = Searchstring;
                    return View(list);
                }
            }


        }

        // GET: Admin/Vouchers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magiamgia magiamgia = db.Magiamgias.Find(id);
            if (magiamgia == null)
            {
                return HttpNotFound();
            }
            return View(magiamgia);
        }

        // GET: Admin/Vouchers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Vouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ma,Soluong,Trangthai,Giatri")] Magiamgia magiamgia)
        {
            if (ModelState.IsValid)
            {
                db.Magiamgias.Add(magiamgia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(magiamgia);
        }

        // GET: Admin/Vouchers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magiamgia magiamgia = db.Magiamgias.Find(id);
            if (magiamgia == null)
            {
                return HttpNotFound();
            }
            return View(magiamgia);
        }

        // POST: Admin/Vouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ma,Soluong,Trangthai,Giatri")] Magiamgia magiamgia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magiamgia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(magiamgia);
        }

        // GET: Admin/Vouchers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magiamgia magiamgia = db.Magiamgias.Find(id);
            if (magiamgia == null)
            {
                return HttpNotFound();
            }
            return View(magiamgia);
        }

        // POST: Admin/Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Magiamgia magiamgia = db.Magiamgias.Find(id);
            db.Magiamgias.Remove(magiamgia);
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
