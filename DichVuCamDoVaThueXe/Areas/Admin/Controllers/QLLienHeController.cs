using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace DichVuCamDoVaThueXe.Areas.Admin.Controllers
{
    public class QLLienHeController : BaseController
	{
		private readonly DichVuCamDoThueXeEntities db = new DichVuCamDoThueXeEntities();
		// GET: Admin/QLLienHe
		public ActionResult DSLienHe()
        {
			using (var context = new DichVuCamDoThueXeEntities())
			{
				ViewBag.listLH = context.LienHes.ToList();

			}
			return View();
		}
		public ActionResult Delete(int id)
		{
			try
			{
				using (var context = new DichVuCamDoThueXeEntities())
				{
						var d = db.LienHes.Find(id);
						db.LienHes.Remove(d);
						db.SaveChanges();
						return Json(true, JsonRequestBehavior.AllowGet);
					
				}
			}
			catch (Exception)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}
		}
		public ActionResult Send(string title, string des,int to)
		{
			try
			{
				using (var context = new DichVuCamDoThueXeEntities())
				{
					string nameEmail = context.Database.SqlQuery<string>("select top 1 Email from ThongTinMail").FirstOrDefault();
					string PassEmail = context.Database.SqlQuery<string>("select top 1 Pass from ThongTinMail").FirstOrDefault();
					string mailto = context.Database.SqlQuery<string>("select Email from LienHe where ID="+to).FirstOrDefault();

					MailMessage messege = new MailMessage(nameEmail, mailto, title, des);
						SmtpClient Client = new SmtpClient("smtp.gmail.com", 587);
						Client.EnableSsl = true;

						Client.Credentials = new NetworkCredential(nameEmail, PassEmail);

						Client.Send(messege);
					var d = db.LienHes.Find(to);
					d.TrangThai = true;

					db.Entry(d).State = EntityState.Modified;
					db.SaveChanges();

					return Json(true, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception e)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}
		}
	}
}