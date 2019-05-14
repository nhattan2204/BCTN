using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DichVuCamDoVaThueXe.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
		// GET: Admin/Base
		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{

			if (Session["ID"] == null || Convert.ToInt32(Session["Group"]) != 1)
			{
				filterContext.Result = new RedirectToRouteResult(
			  new System.Web.Routing.RouteValueDictionary(new { controller = "../DangNhap", action = "DangNhap" }));
			}

			base.OnActionExecuted(filterContext);
		}
	}
}