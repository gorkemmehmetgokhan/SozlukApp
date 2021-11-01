using SozlukApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using SozlukApp.Repository;

namespace SozlukApp.Models.ViewModel
{

    public class EntryViewModel
    {
        public Entry Entry { get; set; }
        public Header Header { get; set; }
        public IEnumerable<SelectListItem> TopicList { get; set; }
        public IEnumerable<SelectListItem> HeaderList { get; set; }
    }
}