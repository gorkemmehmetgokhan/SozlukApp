using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SozlukApp.Common;
using SozlukApp.Entity;

namespace SozlukApp.Repository
{
    public class UserRoleRepository : DataRepository<UserRole, int>
    {
        public static DB_DictionaryEntities db = Tools.GetConnection();
        public override int Delete(int id)
        {
            UserRole deleted = db.UserRoles.SingleOrDefault(t => t.UserRoleId == id);
            db.UserRoles.Remove(deleted);
            return db.SaveChanges();
        }

        public override List<UserRole> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override UserRole GetObjById(int id)
        {
            UserRole ur = db.UserRoles.SingleOrDefault(t => t.UserRoleId == id);
            return ur;
        }

        public override int Insert(UserRole item)
        {
            db.UserRoles.Add(item);
            return db.SaveChanges();
        }

        public override List<UserRole> List()
        {
            return db.UserRoles.ToList();
        }

        public override int Update(UserRole item)
        {
            UserRole updated = db.UserRoles.SingleOrDefault(t => t.UserRoleId == item.UserRoleId);
            updated.Name = item.Name;
            return db.SaveChanges();
        }
    }
}
