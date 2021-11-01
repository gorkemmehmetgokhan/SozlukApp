using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Repository
{
    public abstract class DataRepository<T,M>
    {
        public abstract int Insert(T item);
        public abstract int Update(T item);
        public abstract int Delete(M id);
        public abstract List<T> List();
        public abstract T GetObjById(M id);
        public abstract List<T> GetLatestObj(int Quantity);
    }
}
