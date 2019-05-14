using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Data.Entity;

namespace DichVuCamDoVaThueXe.Controllers
{

    public class ChiTietDVController : Controller
    {
		private readonly DichVuCamDoThueXeEntities db = new DichVuCamDoThueXeEntities();
		// GET: ChiTietDV
		public ActionResult Index()
        {
            return View();
        }
		public ActionResult ChiTiet(int id)
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				ViewBag.listTT = context.ChiTietDVs.Where(x => x.MaSP == id).Take(1).ToList();
				int dv = context.Database.SqlQuery<int>("Select MaDV from ChiTietDV where MaSP=" + id).FirstOrDefault();
				ViewBag.Maybe = context.ChiTietDVs.Where(x => x.MaDV == dv).Where(x => x.MaSP != id).Take(5).ToList();

			}
			return View();
		}
		
	}
}