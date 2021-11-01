using SozlukApp.Entity;
using SozlukApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SozlukApp.Areas.Admin.Controllers
{
    public class UserRoleController : Controller
    {
        // GET: Admin/UserRole
        UserRoleRepository ur = new UserRoleRepository();
        public ActionResult List()
        {
            return View(ur.List());
        }

        public ActionResult Delete(int id)
        {
            int success = ur.Delete(id);
            if (success > 0)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(UserRole model)
        {
            int success = ur.Insert(model);
            if (success > 0)
            {
                return RedirectToAction("List", model);
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(ur.GetObjById(id));
        }

        [HttpPost]
        public ActionResult Edit(UserRole model)
        {
            int success = ur.Update(model);
            if (success > 0)
            {
                return RedirectToAction("List", model);
            }
            return View();
        }

    }
}