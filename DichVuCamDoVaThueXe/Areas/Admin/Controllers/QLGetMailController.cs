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
    public class QLGetMailController : Controller
	{
		private readonly DichVuCamDoThueXeEntities db = new DichVuCamDoThueXeEntities();
		// GET: Admin/QLGetMail
		public ActionResult DSMail()
        {
			using (var context = new DichVuCamDoThueXeEntities())
			{
				ViewBag.listMail = context.GetMails.ToList();

			}
			return View();
		}
		public ActionResult Delete(int id)
		{
			try
			{
				using (var context = new DichVuCamDoThueXeEntities())
				{
					var d = db.GetMails.Find(id);
					db.GetMails.Remove(d);
					db.SaveChanges();
					return Json(true, JsonRequestBehavior.AllowGet);

				}
			}
			catch (Exception)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}
		}
		public ActionResult SendMail()
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				var dt = context.ThongTinMails.Take(1).FirstOrDefault();
				return View(dt);
			}
		}
		
		public ActionResult Send(ThongTinMail m)
		{
			try
			{
				using (var context = new DichVuCamDoThueXeEntities())
				{
					var d = db.ThongTinMails.Find(1);
					if (d == null)
					{
						db.ThongTinMails.Add(m);
						db.SaveChanges();
					}
					else
					{
						d.Email = m.Email;
						d.Pass = m.Pass;
						d.NoiDung = m.NoiDung;
						d.ChuDe = m.ChuDe;
						db.Entry(d).State = EntityState.Modified;
						db.SaveChanges();
					}
					var email = context.GetMails.ToList();
					for (int i = 0; i < email.Count; i++)
					{
						MailMessage messege = new MailMessage(m.Email, email[i].Email, m.ChuDe, m.NoiDung);
						SmtpClient Client = new SmtpClient("smtp.gmail.com", 587);
						Client.EnableSsl = true;

						Client.Credentials = new NetworkCredential(m.Email, m.Pass);

						Client.Send(messege);
					}
					if (email == null || email.Count <1)
						return Json("NotSend", JsonRequestBehavior.AllowGet);
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