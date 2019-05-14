using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DichVuCamDoVaThueXe.Areas.Admin.Controllers
{
	public class QLDatXeController : Controller
	{
		private readonly DichVuCamDoThueXeEntities db = new DichVuCamDoThueXeEntities();
		// GET: Admin/QLDatXe
		public ActionResult DSBookCar()
        {
			using (var context = new DichVuCamDoThueXeEntities())
			{
				ViewBag.listBookCar = context.BookCars.ToList();

			}
			return View();
        }
		public ActionResult Edit(int id)
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				var dt = context.BookCars.Where(x => x.ID == id).FirstOrDefault();
				ViewBag.listStatus = context.Status.ToList();
				ViewBag.listAcount = context.Accounts.Where(x => x.GroupID == 2).ToList();
				ViewBag.listDV = context.ChiTietDVs.Where(x => x.MaDV == 2).ToList();
				return View(dt);

			}
		}
		public ActionResult TenNguoiDat(int id)
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				String ten = context.Database.SqlQuery<string>("select Name from Account where ID ="+id).FirstOrDefault();
				return Json(ten, JsonRequestBehavior.AllowGet);
			}
		}
		public ActionResult XeDat(int id)
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				String ten = context.Database.SqlQuery<string>("select TenSP from ChiTietDV where MaSP =" + id).FirstOrDefault();
				return Json(ten, JsonRequestBehavior.AllowGet);
			}
		}
		public ActionResult Status(int id)
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				String ten = context.Database.SqlQuery<string>("select Name from Status where ID =" + id).FirstOrDefault();
				return Json(ten, JsonRequestBehavior.AllowGet);
			}
		}
		public ActionResult Delete(int id)
		{
			try
			{
				var d = db.BookCars.Find(id);
				db.BookCars.Remove(d);
				db.SaveChanges();
				return Json(true, JsonRequestBehavior.AllowGet);
			}
			catch
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}

		}
		public ActionResult Add(BookCar b)
		{
			try
			{
				b.NgayDat = DateTime.Now;
				db.BookCars.Add(b);
				db.SaveChanges();
				return Json(true, JsonRequestBehavior.AllowGet);

			}
			catch (Exception e)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}

		}
		public ActionResult doEdit(BookCar b)
		{
			try
			{
				using (var context = new DichVuCamDoThueXeEntities())
				{
					db.Entry(b).State = EntityState.Modified;
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