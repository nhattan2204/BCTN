using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DichVuCamDoVaThueXe.Controllers
{
    public class TestLoginController : Controller
    {
		// GET: TestLogin
		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{

			if (Session["ID"] == null || Convert.ToInt32(Session["Group"]) != 2)
			{
				filterContext.Result = new RedirectToRouteResult(
			  new System.Web.Routing.RouteValueDictionary(new { controller = "DangNhap", action = "DangNhap" }));
			}

			base.OnActionExecuted(filterContext);
		}
	}
}