using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewProject.Areas.Admin.Controllers
{
    public class AnnounceController : Controller
    {
        // GET: Admin/Announce
        public ActionResult Index()
        {
            ThongbaoDao thongbao = new ThongbaoDao();
            return View(thongbao.List());
        }
      
        public ActionResult Delete(int ID)
		{
            ThongbaoDao thongbao = new ThongbaoDao();
            thongbao.Delete(ID);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteList(List<int> ID)
		{
            foreach(int item in ID)
			{
                ThongbaoDao thongbao = new ThongbaoDao();
                thongbao.Delete(item);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Respond_mail(string usermail,string beforecontent, string content)
        {
            String content1 = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/RespondMail.html"));
            content1 = content1.Replace("{{beforemail}}", beforecontent);
            content1 = content1.Replace("{{respond}}", content);
            Common.MailHelpper.SendMail("LAVADO", " ", content1, usermail);
            return RedirectToAction("Index");
        }
    }
}