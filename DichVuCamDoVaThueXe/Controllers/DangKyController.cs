using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DichVuCamDoVaThueXe.Controllers
{
    public class DangKyController : Controller
	{
		private readonly DichVuCamDoThueXeEntities db = new DichVuCamDoThueXeEntities();
		// GET: DangKy
		public ActionResult Index()
        {
            return View();
        }
		public ActionResult DangKy()
		{
			return View();
		}
		[HttpPost]
		public ActionResult signup(Account acc)
		{
			try
			{
				db.Accounts.Add(acc);
				db.SaveChanges();
				return Json(true, JsonRequestBehavior.AllowGet);
			}
			catch (Exception e)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}
		}
	}
}