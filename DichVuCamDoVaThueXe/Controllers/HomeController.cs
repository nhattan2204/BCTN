using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Data.Entity;

namespace DichVuCamDoVaThueXe.Controllers
{
	public class HomeController : Controller
	{
		private readonly DichVuCamDoThueXeEntities db = new DichVuCamDoThueXeEntities();
		public ActionResult Index()
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				
			}
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		public ActionResult DichVu(int dv)
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				if(dv == 0)
				{
					var ma = context.Database.SqlQuery<int>("Select top 1 MaDV from DichVu").FirstOrDefault();
					ViewBag.Madv = ma;
					ViewBag.listTT = context.ChiTietDVs.Where(x => x.MaDV == ma).Take(3).ToList();
				}
				else
				{
					ViewBag.listTT = context.ChiTietDVs.Where(x => x.MaDV == dv).Take(3).ToList();
					ViewBag.Madv = dv;
				}
				ViewBag.listDV = context.DichVus.ToList();
			}

			return View();
		}
		public ActionResult Blog()
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				ViewBag.listTT = context.TinTucs.ToList();
				
			}

			return View();
		}
		[HttpPost]
		public ActionResult addContact(LienHe lh)
		{
			try
			{
				db.LienHes.Add(lh);
				db.SaveChanges();
				return Json(true, JsonRequestBehavior.AllowGet);
			}
			catch (Exception e)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}
		}
		public ActionResult Logout()
		{
			try
			{
				Session["ID"] = null;
				Session["Group"] = null;
				Session["Name"] = null;
				return Json(true, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}

		}
		public ActionResult GetMail(GetMail mail)
		{
			try
			{
				db.GetMails.Add(mail);
				db.SaveChanges();
				return Json(true, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}
		}

	}
}