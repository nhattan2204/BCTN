using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Data.Entity;
using System.IO;
using System.Data.Entity;

namespace DichVuCamDoVaThueXe.Areas.Admin.Controllers
{
	public class QLChiTietDVController : BaseController
	{
		private readonly DichVuCamDoThueXeEntities db = new DichVuCamDoThueXeEntities();
		// GET: Admin/QLChiTietDV
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult ChiTietDV()
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				ViewBag.listTT = context.ChiTietDVs.ToList();

			}
			return View();
		}
		public ActionResult ViewChiTietDV(int id)
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				var dt = context.Database.SqlQuery<ChiTietDV>("Select * from ChiTietDV where MaSP="+id).FirstOrDefault();
				ViewBag.listDV = context.DichVus.ToList();

				return View(dt);
			}
		}
		public ActionResult TenDichVu(int id)
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				var name = context.Database.SqlQuery<string>("select TenDV from DichVu where MaDV=" + id).FirstOrDefault();
				return Json(name, JsonRequestBehavior.AllowGet);
			}
		}
		public ActionResult Delete(int id)
		{
			try
			{
				using (var context = new DichVuCamDoThueXeEntities())
				{
					var dt = context.BookCars.Where(x => x.IDChiTietDV == id).Where(x => x.TrangThai == 2).ToList();
					if (dt != null && dt.Count > 0)
						return Json(false, JsonRequestBehavior.AllowGet);
					else
					{
						var d = db.ChiTietDVs.Find(id);
						db.ChiTietDVs.Remove(d);
						db.SaveChanges();
						return Json(true, JsonRequestBehavior.AllowGet);
					}
				}
			}
			catch (Exception)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Add(ChiTietDV dv)
		{
			try
			{
				string filename = Path.GetFileNameWithoutExtension(dv.ImageFile.FileName);
				string ex = Path.GetExtension(dv.ImageFile.FileName);
				filename = filename + DateTime.Now.ToString("ddMMyyyy") + ex;
				dv.Hinh = filename;
				filename = Path.Combine(Server.MapPath("~/images/"), filename);
				dv.ImageFile.SaveAs(filename);
				db.ChiTietDVs.Add(dv);
				db.SaveChanges();
				return Json(true, JsonRequestBehavior.AllowGet);

			}
			catch (Exception e)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}

		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(ChiTietDV dv)
		{
			try
			{
				using (var context = new DichVuCamDoThueXeEntities())
				{
					if (dv.ImageFile != null)
					{
						string filename = Path.GetFileNameWithoutExtension(dv.ImageFile.FileName);
						string ex = Path.GetExtension(dv.ImageFile.FileName);
						filename = filename + DateTime.Now.ToString("ddMMyyyy") + ex;
						dv.Hinh = filename;
						filename = Path.Combine(Server.MapPath("~/Images/News/"), filename);
						dv.ImageFile.SaveAs(filename);
					}
					else
					{
						dv.Hinh = context.Database.SqlQuery<string>("Select Hinh from ChiTietDV where MaSP="+dv.MaSP).FirstOrDefault();
					}


					db.Entry(dv).State = EntityState.Modified;
					db.SaveChanges();
					return Json(true, JsonRequestBehavior.AllowGet);
				}

			}
			catch (Exception ex)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}
		}
	}
}