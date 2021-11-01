using SozlukApp.Entity;
using SozlukApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SozlukApp.Areas.Admin.Controllers
{
    public class EntryController : Controller
    {
        // GET: Admin/Entry
        EntryRepository er = new EntryRepository();
        public ActionResult List()
        {
            return View(er.List());
        }

        public ActionResult Delete(int id)
        {
            int success = er.Delete(id);
            if (success > 0)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(er.GetObjById(id));
        }

        [HttpPost]
        public ActionResult Edit(Entry model)
        {
            int success = er.Update(model);
            if (success > 0)
            {
                return RedirectToAction("List", model);
            }
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(Entry model)
        {
            int success = er.Insert(model);
            if (success > 0)
            {
                return RedirectToAction("List", model);
            }
            return View();
        }
    }
}