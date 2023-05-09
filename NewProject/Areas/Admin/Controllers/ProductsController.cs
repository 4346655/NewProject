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

        // GET: Admin/Products
        public ActionResult Index(string Searchstring, int page = 1, int pagesize = 20)
        {
            if (Phanquyen())
            {
                var product = new ProductsDao();
                var listproduct = product.DanhSachSanPham(Searchstring, page, pagesize);
                ViewBag.Search = Searchstring;
                return View(listproduct);
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }
        }
        public ActionResult ThungRacSanPham(string Searchstring, int page = 1, int pagesize = 20)
        {
            if (Phanquyen())
            {
                var product = new ProductsDao();
                var listproduct = product.DanhSachThungRac(Searchstring, page, pagesize);
                ViewBag.Search = Searchstring;
                return View(listproduct);
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }
          
        }
        public ActionResult KhoiPhucSanPham(int id)
        {
            if (Phanquyen())
            {
                var pr = new ProductsDao();
                pr.Bo_KhoiPhuc_SanPham(id, false);
                return RedirectToAction("ThungRacSanPham");
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }
           
        }
        public ActionResult BoThungRac(int id)
        {
            if (Phanquyen())
            {
                var pr = new ProductsDao();
                pr.Bo_KhoiPhuc_SanPham(id, true);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

           
        }
        public ActionResult XoaSanPham(int id)
        {
            if (Phanquyen())
            {
                var pr = new ProductsDao();
                pr.Delete(id);
                return RedirectToAction("ThungRacSanPham");
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }
           
        }

        public ActionResult Ban_DungBan(int id)
        {
            if (Phanquyen())
            {
                var pr = new ProductsDao();
                pr.Ban_DungBan(id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

        }
        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }
           
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            if (Phanquyen())
            {
                ViewBag.ID_LoaiSach = new SelectList(db.Loai_Sach, "ID", "Loaisach");
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }
           
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_LoaiSach,TenSach,Anh,SoLuong,SoLuong_DaBan,Mota,Gia,Gia_giam,Trangthai,NhaXuatBan,TacGia")] Sach sach)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

            
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }
            
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_LoaiSach,TenSach,Anh,SoLuong,SoLuong_DaBan,Mota,Gia,Gia_giam,Trangthai,NhaXuatBan,TacGia,DeleteStatus")] Sach sach)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

           
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }
            
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Phanquyen())
            {
                Sach sach = db.Saches.Find(id);
                db.Saches.Remove(sach);
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
