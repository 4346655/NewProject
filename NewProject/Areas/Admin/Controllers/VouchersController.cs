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
        public bool Phanquyen()
        {
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session == null)
            {

                return false;

            }
            else
            {
                var account = new AccountDao();
                var id = account.Byname(session.username);
                if (id != 2)
                {
                    return false;

                }
            }
            return true;


        }

        // GET: Admin/Vouchers
        public ActionResult Index(string Searchstring,bool trangthai, int page = 1, int pagesize = 20)
        {
            if (Phanquyen())
            {
                var mgg = new VoucherDao();
                var list = mgg.Pagelist_mgg(Searchstring, trangthai, page, pagesize);
                ViewBag.Search = Searchstring;
                ViewBag.Status = trangthai;
                return View(list);
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

        }
        public ActionResult GianHan(int id, DateTime time1, DateTime time2)
        {
            var voucher = new VoucherDao();
            voucher.GiaHan(id, time1, time2);
            return RedirectToAction("Index", new {trangthai =true });
		}
       public ActionResult Bo_KhoiPhuc_MGG(int idvoucher)
		{
            var voucher = new VoucherDao();
            voucher.SetDeleteStatus(idvoucher);
            return RedirectToAction("index", new { trangthai = true });
		}

        // GET: Admin/Vouchers/Details/5
        public ActionResult Details(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

          
        }

        // GET: Admin/Vouchers/Create
        public ActionResult Create()
        {
            if (Phanquyen())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }
          
        }

        // POST: Admin/Vouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ma,Soluong,Trangthai,Giatri,Time1,Time2,DeleteStatus")] Magiamgia magiamgia)
        {
            if (Phanquyen())
            {
                if (ModelState.IsValid)
                {
                    db.Magiamgias.Add(magiamgia);
                    db.SaveChanges();
                    return RedirectToAction("Index",new { trangthai = false });
                }

                return View(magiamgia);
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

          
        }

        // GET: Admin/Vouchers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

          
        }

        // POST: Admin/Vouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ma,Soluong,Trangthai,Giatri")] Magiamgia magiamgia)
        {
            if (Phanquyen())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(magiamgia).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index",new {trangthai=false });
                }
                return View(magiamgia);
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }
          
        }

        // GET: Admin/Vouchers/Delete/5
        public ActionResult Delete(int? id)
        {

            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }
          
        }

        // POST: Admin/Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Phanquyen())
            {
                Magiamgia magiamgia = db.Magiamgias.Find(id);
                db.Magiamgias.Remove(magiamgia);
                db.SaveChanges();
                return RedirectToAction("Index", new { trangthai = true });
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }
          
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
