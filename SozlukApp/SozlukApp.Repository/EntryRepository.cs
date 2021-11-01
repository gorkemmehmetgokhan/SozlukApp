using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SozlukApp.Entity;
using SozlukApp.Common;

namespace SozlukApp.Repository
{
    public class EntryRepository : DataRepository<Entry, int>
    {
        public static DB_DictionaryEntities db = Tools.GetConnection();
        HeaderRepository hr = new HeaderRepository();
        public override int Delete(int id)
        {
            Entry deleted = db.Entries.SingleOrDefault(t => t.EntryId == id);
            Header header = hr.GetObjById(Convert.ToInt32(deleted.HeaderId));
            header.EntryCount--;
            hr.Update(header);

            db.Entries.Remove(deleted);

            return db.SaveChanges();
        }

        public override List<Entry> GetLatestObj(int Quantity)
        {
            return db.Entries.OrderByDescending(t => t.EntryId).Take(Quantity).ToList();
        }

        public override Entry GetObjById(int id)
        {
            Entry e = db.Entries.SingleOrDefault(t => t.EntryId == id);
            return e;
        }

        public override int Insert(Entry item)
        {
            item.LikeCount = 0;
            item.UnlikeCount = 0;
            item.Posttime = DateTime.Now;
            db.Entries.Add(item);
           
            Header header = hr.GetObjById(Convert.ToInt32(item.HeaderId));
            header.EntryCount++;
            hr.Update(header);

            return db.SaveChanges();
        }

        public override List<Entry> List()
        {
            return db.Entries.ToList();
        }

        public override int Update(Entry item)
        {
            Entry updated = db.Entries.SingleOrDefault(t => t.EntryId == item.EntryId);
            updated.HeaderId = item.HeaderId;
            updated.UserId = item.UserId;
            updated.LikeCount = item.LikeCount;
            updated.UnlikeCount = item.UnlikeCount;
            updated.Posttime = item.Posttime;
            updated.Text = item.Text;           
            return db.SaveChanges();
        }
    }
}
