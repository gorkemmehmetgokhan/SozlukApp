using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SozlukApp.Common;
using SozlukApp.Entity;

namespace SozlukApp.Repository
{
    public class TopicRepository : DataRepository<Topic, int>
    {
        public static DB_DictionaryEntities db = Tools.GetConnection();
        public override int Delete(int id)
        {
            Topic deleted = db.Topics.SingleOrDefault(t => t.TopicId == id);
            db.Topics.Remove(deleted);
            return db.SaveChanges();
        }

        public override List<Topic> GetLatestObj(int Quantity)
        {
            return db.Topics.OrderByDescending(t => t.TopicId).Take(Quantity).ToList();
        }

        public override Topic GetObjById(int id)
        {
            Topic tp = db.Topics.SingleOrDefault(t => t.TopicId == id);
            return tp;
        }

        public override int Insert(Topic item)
        {
            db.Topics.Add(item);
            return db.SaveChanges();
        }

        public override List<Topic> List()
        {
            return db.Topics.ToList();
        }

        public override int Update(Topic item)
        {
            Topic updated = db.Topics.SingleOrDefault(t => t.TopicId == item.TopicId);
            updated.Name = item.Name;
            return db.SaveChanges();
        }
    }
}
