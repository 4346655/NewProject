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
        public ActionResult Index(string Searchstring, int page = 1, int pagesize = 5)
        {
            if (Phanquyen())
            {
                var customer = new CustomersDao();
                var listcustomer = customer.DanhSachKhachHang(Searchstring, page, pagesize);
                ViewBag.Search = Searchstring;
                return View(listcustomer);
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }



        }
        public ActionResult Bat_Tat_HoatDong(int id )
		{
            if (Phanquyen())
            {
                var customer = new CustomersDao();
                customer.Bat_Tat_HoatDong(id);
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }
           
		}

        // GET: Admin/Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

           
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            if (Phanquyen())
            {
                ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "TenTaiKhoan");
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

            
        }
       
        // POST: Admin/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_TaiKhoan,HoTen,Diachi,Email,SDT,AnhDaiDien")] KhachHang khachHang)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

            
        }

        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }           
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_TaiKhoan,HoTen,Diachi,Email,SDT,AnhDaiDien")] KhachHang khachHang)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

           
        }

        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Phanquyen())
            {
                KhachHang khachHang = db.KhachHangs.Find(id);
                db.KhachHangs.Remove(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
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
