using SozlukApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SozlukApp.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User

        UserRepository ur = new UserRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View(ur.List());
        }

        public ActionResult Delete(int id)
        {
            ur.Delete(id);
            return RedirectToAction("List");
        }
    }
}