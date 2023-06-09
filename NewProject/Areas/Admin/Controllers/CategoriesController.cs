﻿using System;
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

        // GET: Admin/Categories
        public ActionResult Index(string Searchstring, bool trangthai, int page = 1, int pagesize = 20)
        {
            if (Phanquyen())
            {
                var category = new CategoriesDao();
                var listcategory = category.DanhSachDanhMuc(Searchstring, trangthai, page, pagesize);
                ViewBag.Trangthai = trangthai.ToString();
                ViewBag.Search = Searchstring;
                return View(listcategory);
            }
			else
			{
                return RedirectToAction("Error", "Home1");
			}


        }
        public ActionResult GetListProduct(int iddanhmuc)
		{
            var product = new ProductsDao();
            var list = product.GetListSach_IDcategory(iddanhmuc);
            return View(list);
		}
        public ActionResult ThemDanhMuc(string tendanhmuc)
		{
            var category = new CategoriesDao();
            int key = category.ThemDanhMuc(tendanhmuc, true);
            if(key==1)
			{
                ModelState.AddModelError("", "Thêm danh mục mới thành công.");
               
            }
			else
			{
                ModelState.AddModelError("", "Tên danh mục đã tồn tại hoặc bị lỗi.");
                
            }
            return RedirectToAction("Index", new { trangthai = true });

        }
        public ActionResult XoaDanhMuc(int id)
        {
            if (Phanquyen())
            {
                var category = new CategoriesDao();
                category.XoaDanhMuc(id);
                return RedirectToAction("Index", new { trangthai = true });
            }
            else return RedirectToAction("Error", "Home1");
        }
        public ActionResult Bat_Tat_HoatDong(int idls)
        {
            if (Phanquyen())
            {
                var catrgory = new CategoriesDao();
                catrgory.Bat_Tat_HoatDong(idls);
                return RedirectToAction("Index", new { trangthai = true });
            }
            else return RedirectToAction("Error", "Home1");
        }
        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (Phanquyen())
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
            else return RedirectToAction("Error", "Home1");
           
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            if (Phanquyen())
            {
                return View();
            }
            else return RedirectToAction("Error", "Home1");
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Loaisach,Trangthai")] Loai_Sach loai_Sach)
        {

            if (Phanquyen())
            {
                if (ModelState.IsValid)
                {
                   
                        db.Loai_Sach.Add(loai_Sach);
                        db.SaveChanges();
                  
                    return RedirectToAction("Index");
                }

                return View(loai_Sach);
            }
            else return RedirectToAction("Error", "Home1");


          
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Phanquyen())
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
            else return RedirectToAction("Error", "Home1");
            
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Loaisach,Trangthai")] Loai_Sach loai_Sach)
        {
            if (Phanquyen())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(loai_Sach).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(loai_Sach);
            }
            else return RedirectToAction("Error", "Home1");
            
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {

            if (Phanquyen())
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
            else return RedirectToAction("Error", "Home1");
           
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Phanquyen())
            {
                Loai_Sach loai_Sach = db.Loai_Sach.Find(id);
                db.Loai_Sach.Remove(loai_Sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Error", "Home1");
           
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
