using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace DichVuCamDoVaThueXe.Controllers
{
    public class DatXeController : TestLoginController
	{
		private readonly DichVuCamDoThueXeEntities db = new DichVuCamDoThueXeEntities();
		// GET: DatXe
		public ActionResult Index()
        {
            return View();
        }
		public ActionResult DatXe(int id)
		{
			using (var context = new DichVuCamDoThueXeEntities())
			{
				ViewBag.listTT = context.ChiTietDVs.Where(x => x.MaSP == id).ToList();
				ViewBag.DVid = id;
			}
			return View();
		}
		public ActionResult addDatXe(BookCar dx)
		{
			try
			{
				using (var context = new DichVuCamDoThueXeEntities())
				{
					dx.NgayDat = DateTime.Now;
					dx.AccountID = Convert.ToInt32(Session["ID"]);
					db.BookCars.Add(dx);
					db.SaveChanges();
					string nameEmail = context.Database.SqlQuery<string>("select top 1 Email from ThongTinMail").FirstOrDefault();
					string PassEmail = context.Database.SqlQuery<string>("select top 1 Pass from ThongTinMail").FirstOrDefault();

					string email = context.Database.SqlQuery<string>("select Email from Account where ID = "+ Convert.ToInt32(Session["ID"])).FirstOrDefault();
					string xe = context.Database.SqlQuery<string>("select TenSP from ChiTietDV where MaSP=" + dx.IDChiTietDV).FirstOrDefault();
					int Price = context.Database.SqlQuery<int>("select DonGia from ChiTietDV where MaSP=" + dx.IDChiTietDV).FirstOrDefault();
					string mota = context.Database.SqlQuery<string>("select MoTaNgan from ChiTietDV where MaSP=" + dx.IDChiTietDV).FirstOrDefault();

					string noidung = "Bạn đã đặt thuê xe tại công ty Thành Chung.  \n  Thông tin xe đặt: Xe " + xe +" \n  Giá: "+Price+" VNĐ/Ngày  \n "+ mota.Replace("<br />","\n");
					noidung += "Bạn dự định đặt xe trong vòng " + dx.ThueBaoNhieuNgay + " ngày, người đứng ra đặt có số CMND là " + dx.CMND + " \n";
					noidung += " \n  \n Để nhận xe bạn hãy đến trung tâm công ty đặt cọt và hoàn thành thủ tục để nhận xe.  \n  \n Cảm ơn bạn đã quan tâm dịch vụ của công ty. \n ";
					MailMessage messege = new MailMessage(nameEmail, email , "Xác nhận đặt xe từ Công ty Thành Chung", noidung);
					SmtpClient Client = new SmtpClient("smtp.gmail.com", 587);
					Client.EnableSsl = true;

					Client.Credentials = new NetworkCredential(nameEmail, PassEmail);

					Client.Send(messege);

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