using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SozlukApp.Entity;
using SozlukApp.Common;

namespace SozlukApp.Repository
{
    public class UserRepository : DataRepository<User, int>
    {
        public static DB_DictionaryEntities db = Tools.GetConnection();
        public override int Delete(int id)
        {
            User deleted = db.Users.SingleOrDefault(t => t.UserId == id);
            db.Users.Remove(deleted);
            return db.SaveChanges();
        }

        public override List<User> GetLatestObj(int Quantity)
        {
            return db.Users.OrderByDescending(t => t.UserId).Take(Quantity).ToList();
        }

        public override User GetObjById(int id)
        {
            User u = db.Users.SingleOrDefault(t => t.UserId == id);
            return u;
        }

        public override int Insert(User item)
        {
            db.Users.Add(item);
            return db.SaveChanges();
        }

        public override List<User> List()
        {
            return db.Users.ToList();
        }

        public override int Update(User item)
        {
            User updated = db.Users.SingleOrDefault(t => t.UserId == item.UserId);
            updated.Username = item.Username;
            updated.Password = item.Password;
            updated.Birthday = item.Birthday;
            updated.Gender = item.Gender;
            updated.Email = item.Email;
            updated.Photo = item.Photo;
            updated.UserRoleId = item.UserRoleId;
            return db.SaveChanges();
        }
    }
}
