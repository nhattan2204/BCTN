using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DichVuCamDoVaThueXe.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult DangNhap()
		{
			return View();
		}
		public ActionResult login(string name, string pass)
		{
			try
			{
				using (var context = new DichVuCamDoThueXeEntities())
				{
					var dt = context.Accounts.Where(x => x.Name == name).Where(x => x.Pass == pass).ToList();
					if(dt != null && dt.Count > 0 )
					{
						Session["ID"] = context.Database.SqlQuery<int>("Select ID from Account where Name= '" + name+"'").FirstOrDefault();
						Session["Name"] = context.Database.SqlQuery<string>("Select Name from Account where Name= '" + name + "'").FirstOrDefault();
						Session["Group"] = context.Database.SqlQuery<int>("Select GroupID from Account where Name= '" + name + "'").FirstOrDefault();
						int t = context.Database.SqlQuery<int>("Select GroupID from Account where Name= '" + name + "'").FirstOrDefault();
						
						return Json(t, JsonRequestBehavior.AllowGet);
					}
					else
					{
						return Json(false, JsonRequestBehavior.AllowGet);
					}
				}
			}
			catch (Exception e)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}
		}
	}
}