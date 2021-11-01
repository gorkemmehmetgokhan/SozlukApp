using SozlukApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SozlukApp.Repository;
using PagedList;
using System.Web.Security;

namespace SozlukApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        HeaderRepository hr = new HeaderRepository();
        EntryRepository er = new EntryRepository();
        DB_DictionaryEntities db = new DB_DictionaryEntities();

        public ActionResult Index()
        {
            return View(hr.List().Where(t => t.TopicId == 21));
        }

        public ActionResult ListByTopic(int? id)
        {
            Session["TopicId"] = id.ToString();
            List<Header> hList = hr.List().Where(t => t.TopicId == id).ToList();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult ListByHeader(int id, int page = 1)
        {
            Session["HeaderId"] = id;
            ViewBag.header = id;

            List<Entry> eList = er.List().Where(t => t.HeaderId == id).ToList();
            PagedList<Entry> model = new PagedList<Entry>(eList, page, 5);
            return View(model);
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = User.Identity.Name;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            if (ModelState.IsValid)
            {
                using (DB_DictionaryEntities ctx = new DB_DictionaryEntities())
                {
                    var user = ctx.Users.FirstOrDefault(x => x.Username == u.Username && x.Password == u.Password);
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Username, true);
                        FormsAuthentication.SetAuthCookie(user.UserId.ToString(), true);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user, HttpPostedFileBase photo, string sampleradio)
        {
            UserRepository ur = new UserRepository();

            string PhotoName = "";
            if (photo != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photo.SaveAs(path);

                user.Photo = PhotoName;
            }

            else if (photo == null)
            {
                user.Photo = 0.ToString();
            }

            user.UserRoleId = 2;

            if (sampleradio == "Male")
            {
                user.Gender = "Erkek";
            }
            else
            {
                user.Gender = "Kadın";
            }

            int success = ur.Insert(user);
            if (success > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult EntryAdd(Entry e)
        {
            Entry entry = new Entry();

            if (User.Identity.IsAuthenticated)
            {
                entry.UserId = Convert.ToInt32(User.Identity.Name);
            }

            entry.Text = e.Text;
            entry.Posttime = DateTime.Now;
            entry.HeaderId = e.HeaderId;
            er.Insert(entry);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [HttpGet]
        public ActionResult Search(string searchText)
        {
            ViewBag.Username = db.Users.Where(t => t.Username.Contains(searchText)).ToList();
            ViewBag.Header = db.Headers.Where(t => t.Name.Contains(searchText)).ToList();
            ViewBag.Text = db.Entries.Where(t => t.Text.Contains(searchText)).ToList();

            return View();
        }

        [HttpGet]
        public ActionResult Like(int id)
        {
            UserEntryRepository uer = new UserEntryRepository();
            Entry e = new Entry();
            if (User.Identity.IsAuthenticated)
            {
                UserEntry ue = uer.GetByUserIdEntryId(Convert.ToInt32(User.Identity.Name), id);
                if (ue == null)
                {
                    UserEntry ue2 = new UserEntry();
                    ue2.UserId = Convert.ToInt32(User.Identity.Name);
                    ue2.EntryId = id;
                    ue2.LikeStatus = 1;
                    uer.Insert(ue2);

                    e = er.GetObjById(Convert.ToInt32(ue2.EntryId));
                    e.LikeCount++;
                    er.Update(e);
                }
                else if (ue.LikeStatus == 0)
                {
                    e = er.GetObjById(Convert.ToInt32(ue.EntryId));
                    e.LikeCount++;
                    er.Update(e);

                    ue.LikeStatus = 1;
                    uer.Update(ue);
                }
                else if (ue.LikeStatus == 1)
                {
                    e = er.GetObjById(Convert.ToInt32(ue.EntryId));
                    e.LikeCount--;
                    er.Update(e);

                    ue.LikeStatus = 0;
                    uer.Update(ue);
                }
                else if (ue.LikeStatus == 2)
                {
                    e = er.GetObjById(Convert.ToInt32(ue.EntryId));
                    e.LikeCount++;
                    e.UnlikeCount--;
                    er.Update(e);

                    ue.LikeStatus = 1;
                    uer.Update(ue);
                }
            }       
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [HttpGet]
        public ActionResult Dislike(int id)
        {
            UserEntryRepository uer = new UserEntryRepository();
            Entry e = new Entry();
            if (User.Identity.IsAuthenticated)
            {
                UserEntry ue = uer.GetByUserIdEntryId(Convert.ToInt32(User.Identity.Name), id);
                if (ue == null)
                {
                    UserEntry ue2 = new UserEntry();
                    ue2.UserId = Convert.ToInt32(User.Identity.Name);
                    ue2.EntryId = id;
                    ue2.LikeStatus = 2;
                    uer.Insert(ue2);

                    e = er.GetObjById(Convert.ToInt32(ue2.EntryId));
                    e.LikeCount++;
                    er.Update(e);
                }
                else if (ue.LikeStatus == 0)
                {
                    e = er.GetObjById(Convert.ToInt32(ue.EntryId));
                    e.UnlikeCount++;
                    er.Update(e);

                    ue.LikeStatus = 2;
                    uer.Update(ue);
                }
                else if (ue.LikeStatus == 2)
                {
                    e = er.GetObjById(Convert.ToInt32(ue.EntryId));
                    e.UnlikeCount--;
                    er.Update(e);

                    ue.LikeStatus = 0;
                    uer.Update(ue);
                }
                else if (ue.LikeStatus == 1)
                {
                    e = er.GetObjById(Convert.ToInt32(ue.EntryId));
                    e.LikeCount--;
                    e.UnlikeCount++;
                    er.Update(e);

                    ue.LikeStatus = 2;
                    uer.Update(ue);
                }
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}