using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SozlukApp.Common;
using SozlukApp.Entity;

namespace SozlukApp.Repository
{
    public class HeaderRepository : DataRepository<Header, int>
    {
        public static DB_DictionaryEntities db = Tools.GetConnection();
        public override int Delete(int id)
        {
            Header deleted = db.Headers.SingleOrDefault(t => t.HeaderId == id);
            db.Headers.Remove(deleted);
            return db.SaveChanges();
        }

        public override List<Header> GetLatestObj(int Quantity)
        {
            return db.Headers.OrderByDescending(t => t.HeaderId).Take(Quantity).ToList();
        }

        public override Header GetObjById(int id)
        {
            Header h = db.Headers.SingleOrDefault(t => t.HeaderId == id);
            return h;
        }

        public override int Insert(Header item)
        {
            item.EntryCount = 0;
            db.Headers.Add(item);
            return db.SaveChanges();
        }

        public override List<Header> List()
        {      
            return db.Headers.ToList();
        }

        public override int Update(Header item)
        {
            Header updated = db.Headers.SingleOrDefault(t => t.HeaderId == item.HeaderId);
            updated.Name = item.Name;
            return db.SaveChanges();
        }
    }
}
