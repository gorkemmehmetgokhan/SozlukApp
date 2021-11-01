using SozlukApp.Entity;
using SozlukApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SozlukApp.Areas.Admin.Controllers
{
    public class HeaderController : Controller
    {
        // GET: Admin/Header

        HeaderRepository hr = new HeaderRepository();
        public ActionResult List()
        {
            return View(hr.List());
        }

        public ActionResult Delete(int id)
        {
            int success = hr.Delete(id);
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
        public ActionResult Add(Header model)
        {
            int success = hr.Insert(model);
            if (success > 0)
            {
                return RedirectToAction("List", model);
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(hr.GetObjById(id));
        }

        [HttpPost]
        public ActionResult Edit(Header model)
        {
            int success = hr.Update(model);
            if (success > 0)
            {
                return RedirectToAction("List", model);
            }
            return View();
        }
    }
}