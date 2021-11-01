using SozlukApp.Entity;
using SozlukApp.Models.ViewModel;
using SozlukApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SozlukApp.Controllers
{
    public class ProfileController : Controller
    {
        DB_DictionaryEntities db = new DB_DictionaryEntities();
        EntryRepository er = new EntryRepository();
        HeaderRepository hr = new HeaderRepository();
        TopicRepository tr = new TopicRepository();
        // GET: Profile

        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                id = Convert.ToInt32(User.Identity.Name);
            }
            List<Entry> eList = er.List().Where(t => t.UserId == id).ToList();          
            return View(eList);
        }


        [HttpGet]
        public ActionResult IndexUser(int? id)
        {
            ViewBag.UserID = id;
            List<Entry> eList = er.List().Where(t => t.UserId == id).ToList();
            return View(eList);
        }
        public ActionResult myPage(User u)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            u.UserId = Convert.ToInt32(ticket.Name);
            var user = db.Users.FirstOrDefault(x => x.UserId == u.UserId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult userPage(int? id)
        {
            var user = db.Users.FirstOrDefault(x => x.UserId == id);
            return RedirectToAction("IndexUser", new { @id = id });
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
                return RedirectToAction("Index", "Profile");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            int success = er.Delete(id);
            if (success > 0)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View();
        }

        [HttpGet]
        public ActionResult AddHeader()
        {
            EntryViewModel ewm = new EntryViewModel();
            List<SelectListItem> topicList = new List<SelectListItem>();

            foreach (Topic item in tr.List())
            {
                topicList.Add(new SelectListItem { Value = item.TopicId.ToString(), Text = item.Name });
            }

            ewm.TopicList = topicList;

            return View(ewm);
        }

        [HttpPost]
        public ActionResult AddHeader(EntryViewModel model)
        {
            int success = hr.Insert(model.Header);

            if (success > 0)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AddEntry()
        {
            EntryViewModel ewm = new EntryViewModel();
            List<SelectListItem> headerList = new List<SelectListItem>();

            foreach (Header item in hr.List())
            {
                headerList.Add(new SelectListItem { Value = item.HeaderId.ToString(), Text = item.Name});
            }
            ewm.HeaderList = headerList;
            return View(ewm);
        }
        [HttpPost]
        public ActionResult AddEntry(EntryViewModel model)
        {           
            Entry entry = new Entry();
            entry.Text = model.Entry.Text;
            entry.Posttime = DateTime.Now;         
            entry.UserId = Convert.ToInt32(User.Identity.Name);
            entry.HeaderId = model.Entry.HeaderId;
            entry.LikeCount = model.Entry.LikeCount;
            entry.UnlikeCount = model.Entry.UnlikeCount;

            er.Insert(entry);

           return RedirectToAction("Index", "Profile");
           
        }
    }
}