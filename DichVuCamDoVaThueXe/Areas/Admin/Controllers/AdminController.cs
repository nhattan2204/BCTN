using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DichVuCamDoVaThueXe.Areas.Admin.Controllers
{
    public class AdminController : BaseController
	{
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}