using SozlukApp.Entity;
using SozlukApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SozlukApp.Areas.Admin.Controllers
{
    public class TopicController : Controller
    {
        // GET: Admin/Topic

        TopicRepository tr = new TopicRepository();
        public ActionResult List()
        {
            return View(tr.List());
        }

        public ActionResult Delete(int id)
        {
            int success = tr.Delete(id);
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
        public ActionResult Add(Topic model)
        {
            int success = tr.Insert(model);
            if (success > 0)
            {
                return RedirectToAction("List", model);
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(tr.GetObjById(id));
        }

        [HttpPost]
        public ActionResult Edit(Topic model)
        {
            int success = tr.Update(model);
            if (success > 0)
            {
                return RedirectToAction("List", model);
            }
            return View();
        }
    }
}