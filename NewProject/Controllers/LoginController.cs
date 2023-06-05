using Models.DAO;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewProject.Areas.Admin.Controllers;
using Facebook;
using System.Configuration;

namespace NewProject.Controllers
{
    public class LoginController : Controller
    {
        private Uri RedirectUri
		{
			get
			{
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
			}
		}
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModels login)
        {
            if (ModelState.IsValid)
            {
                var user = new AccountDao();
                var res = user.login(login.username, login.password);
                if (res == 5)
                {
                    Session.Add(LoginConstants.LOGIN_SESSION, login);
                    var cs = new CustomersDao();
                    var ac = cs.GetDetailByUsername(login.username);
             
                        return RedirectToAction("Index", "Home");
                   

                }
                else if(res==0)
				{
                    ModelState.AddModelError("", "Vui lòng nhập đủ thông tin.");
                }
                else if(res==1)
				{
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu bị lỗi.");
                }
				else if(res==2)
				{
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (res == 3)
                {
                    ModelState.AddModelError("", "Sai mật khẩu ");
                }
                else if (res == 4)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa.");
                }
            }
            return View();
        }
        public ActionResult Forgetpassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgetpassword(string username, string email)
        {
            var ac = new AccountDao();
            var mk = ac.quenmatkhau(username, email);
            if (mk == "0")
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại.");
            }
            else if (mk == "1")
            {
                ModelState.AddModelError("", "Email tài khoản không chính xác.");
            }
            else if (mk == "2")
            {
                ModelState.AddModelError("", "Nhập đủ thông tin.");
            }
            else
            {
                String content = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/QuenMatKhau.html"));
                content = content.Replace("{{CustommerName}}", username);
                content = content.Replace("{{NEWPASS}}", mk);
                
                Common.MailHelpper.SendMail("LAVADO", "Tài khoản " , content.ToString(), email);

                ViewBag.Alert = mk;
                username = "";
                email = "";
                ModelState.AddModelError("", "Lấy mật khẩu thành công.");
            }
            return View();
        }
        public ActionResult Logout()
		{

            Session.Clear();
            return RedirectToAction("Index");

        }
        public ActionResult LoginFacebook()
		{
            var facebook = new FacebookClient();
            var loginUrl = facebook.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                respond_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
		}
        public ActionResult FacebookCallback(string code)
		{
            var facebook = new FacebookClient();
           dynamic result = facebook.Post("oauth/access_token",new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code= code,
            });
            var accesstoken = result.access_token;
            if(!string.IsNullOrEmpty(accesstoken))
			{
                dynamic me = facebook.Get("me?username,password");
                string username = me.username;
                string password = me.password;

                var rst = new AccountDao().InsertForFacebook(username, password);
                if(rst==1)
				{
                    var session = new LoginModels();
                    session.username = username;
                    session.password = password;
                    Session.Add(LoginConstants.LOGIN_SESSION, session);
                   
				}
				else
				{
                    ModelState.AddModelError("", "Đăng nhập thất bại");
				}
			}
            return RedirectToAction("Index", "Home");


        }
    }
}