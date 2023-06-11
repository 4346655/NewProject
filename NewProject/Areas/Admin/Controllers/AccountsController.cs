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
    public class AccountsController : Controller
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
        // GET: Admin/Accounts
        public ActionResult Index()
        {
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var khachhang = kh.GetDetailByUsername(session.username);
            return View(khachhang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string hoten, string diachi, string email, string sdt, string avt)
        {
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var detail = kh.GetDetailByUsername(session.username);
            var id = kh.GetID(session.username);
            int key = kh.ChangeInfo(id, hoten, diachi, email, sdt, avt);
            if (key == 0)
            {
                ModelState.AddModelError("", "Thay đổi thành công");
            }
            else if (key == 1)
            {
                ModelState.AddModelError("", "Họ và tên của bạn chứa kí tự không hợp lệ");
            }
            else if (key == 3)
            {
                ModelState.AddModelError("", "Email không hợp lệ");
            }
            else if (key == 2)
            {
                ModelState.AddModelError("", "Số điện thoại chứa kí tự không hợp lệ");
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập đúng số điện thoại");
            }
            return View(detail);
        }


        public ActionResult DoimatkhauAdmin()
        {
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            var kh = new CustomersDao();
            var khachhang = kh.GetDetailByUsername(session.username);
            return View(khachhang);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoimatkhauAdmin(string oldpass, string newpass, string confirm)
        {
            var session = (LoginModels)Session[LoginConstants.LOGIN_SESSION];
            if (session != null)
            {
                var kh = new CustomersDao();
                var id = kh.GetIDTK(session.username);
                var ac = new AccountDao();
                var key = ac.Changepassword(id, oldpass, newpass, confirm);
                if (key == 0)
                {
                    ModelState.AddModelError("", "Nhập đầy đủ thông tin");
                }
                else if (key == 1)
                {
                    ModelState.AddModelError("", "Mật khẩu cũ không chính xác");
                }
                else if (key == 2)
                {
                    ModelState.AddModelError("", "Mật khẩu không khớp");
                }
                else if (key == 4)
                {
                    ModelState.AddModelError("", "Mật khẩu nhiều hơn 8 kí tự");
                }
                else if (key == 5)
                {
                    ModelState.AddModelError("", "Mật khẩu chứa kí tự không cho phép");
                }
                else
                {
                    ModelState.AddModelError("", "Đổi mật khẩu thành công");
                }


                ac.CreateNew(session.username);
                var khachhang = kh.GetDetailByUsername(session.username);
                return View(khachhang);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }



        // GET: Admin/Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: Admin/Accounts/Create
        public ActionResult Create()
        {
            ViewBag.ID_LoaiTK = new SelectList(db.LoaiTKs, "ID", "Ten_LoaiTK");
            return View();
        }



        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenTaiKhoan,MatKhau,ID_LoaiTK,Trangthai")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_LoaiTK = new SelectList(db.LoaiTKs, "ID", "Ten_LoaiTK", taiKhoan.ID_LoaiTK);
            return View(taiKhoan);
        }

        // GET: Admin/Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LoaiTK = new SelectList(db.LoaiTKs, "ID", "Ten_LoaiTK", taiKhoan.ID_LoaiTK);
            return View(taiKhoan);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenTaiKhoan,MatKhau,ID_LoaiTK,Trangthai")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_LoaiTK = new SelectList(db.LoaiTKs, "ID", "Ten_LoaiTK", taiKhoan.ID_LoaiTK);
            return View(taiKhoan);
        }

        // GET: Admin/Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taiKhoan);
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
