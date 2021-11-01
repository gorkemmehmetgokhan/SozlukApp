using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SozlukApp.Entity;

namespace SozlukApp.Common
{
    public class Tools
    {
        public static DB_DictionaryEntities db = null;
        public static DB_DictionaryEntities GetConnection()
        {
            if (db == null)
            {
                db = new DB_DictionaryEntities();
            }
            return db;
        }
    }
}
