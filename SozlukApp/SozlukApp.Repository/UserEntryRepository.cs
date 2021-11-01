using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SozlukApp.Entity;
using SozlukApp.Repository;
using SozlukApp.Common;

namespace SozlukApp.Repository
{
    public class UserEntryRepository : DataRepository<UserEntry, int>
    {
        public static DB_DictionaryEntities db = Tools.GetConnection();
        EntryRepository er = new EntryRepository();
        
        public override int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<UserEntry> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override UserEntry GetObjById(int id)
        {
            throw new NotImplementedException();
        }
        public UserEntry GetByUserIdEntryId(int UserId, int EntryId)
        {
            UserEntry ue = db.UserEntries.SingleOrDefault(t => t.UserId == UserId & t.EntryId == EntryId);
            return ue;
        }

        public override int Insert(UserEntry item)
        {
            db.UserEntries.Add(item);                       

            return db.SaveChanges();
        }

        public override List<UserEntry> List()
        {
            throw new NotImplementedException();
        }

        public override int Update(UserEntry item)
        {
            UserEntry updated = db.UserEntries.SingleOrDefault(t => t.UserEntryId == item.UserEntryId);
            updated.EntryId = item.EntryId;
            updated.LikeStatus = item.LikeStatus;
            updated.UserId = item.UserId;
            return db.SaveChanges();
        }
    }
}
