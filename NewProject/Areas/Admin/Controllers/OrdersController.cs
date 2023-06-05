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
        // GET: Admin/Orders
        public ActionResult Index(string Searchstring, int trangthai, int page = 1, int pagesize = 20 )
        {
            if (Phanquyen())
            {
                ViewBag.TrangThai = trangthai;
                var order = new OrdersDao();
                var listorder = order.DanhSachDonHang(Searchstring, page, pagesize, trangthai);
                ViewBag.Search = Searchstring;
                return View(listorder);
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

        }
        public ActionResult DanhSachMoiMua()
		{
            if (Phanquyen())
            {
                var order = new OrdersDao();
                var listorder = order.DanhSachChuanBi();
                return View(listorder);
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

           
		}
        public ActionResult HuyDonAdmin(int idor)
		{
            if (Phanquyen())
            {
                var order = new OrdersDao();
                order.HuyDon(idor);
                return RedirectToAction("Index", new { trangthai = 0 });
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

            
		}
        public ActionResult KhoiphucAdmin(int idor)
        {
            if (Phanquyen())
            {
                var or = new OrdersDao();
                or.Mualai(idor);
                return RedirectToAction("Index", new { trangthai = 1 });
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

           
        }
        public ActionResult ChuyenTiepTrangThai(int idor)
		{
            if (Phanquyen())
            {
                var or = new OrdersDao();
                or.ChuyenTiepTrangThai(idor);
                int trangthai = or.LayTrangThai(idor);
                return RedirectToAction("Index", new { trangthai = trangthai });
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

           
		}

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

           
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            if (Phanquyen())
            {
                ViewBag.ID_KhachHang = new SelectList(db.KhachHangs, "ID", "HoTen");
                ViewBag.ID_Sach = new SelectList(db.Saches, "ID", "TenSach");
                ViewBag.ID_ThanhToan = new SelectList(db.ThanhToans, "ID", "TenThanhToan");
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home1");
            }

           
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_KhachHang,ID_Sach,ID_ThanhToan,SoLuong,Ma_GG,Note,Tong_gia,Trang_thai")] DonHang donHang)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

           
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_KhachHang,ID_Sach,ID_ThanhToan,SoLuong,Ma_GG,Note,Tong_gia,Trang_thai")] DonHang donHang)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

            
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Phanquyen())
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
            else
            {
                return RedirectToAction("Error", "Home1");
            }

            
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Phanquyen())
            {
                DonHang donHang = db.DonHangs.Find(id);
                db.DonHangs.Remove(donHang);
                db.SaveChanges();
                return RedirectToAction("Index", new { trangthai = 0 });
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
